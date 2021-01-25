using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using music.Domain;
using music.Domain.Common;
using music.Domain.Entities;
using music.Domain.Services.Internal;
using music.Services.Common;

namespace music.Services.services
{
    public class GradeService : BaseService, IGradeService
    {
        private readonly IUnitOfWork uow;
        private readonly IErrorMessages ErrorMEssages;
        public GradeService(IUnitOfWork uow, IHttpContextAccessor accessor, IErrorMessages ErrorMEssages) : base(accessor)
        {
            this.ErrorMEssages = ErrorMEssages;
            this.uow = uow;

        }

        public async Task<Error> AddAlbumGrade(string albumId, double newScore)
        {
            var firstCheck = await CheckUserHasGivenScoreAsync(albumId, GradeTypeEnum.ALBUM);
            if (firstCheck.scored)
                return Error.ToError(ErrorMEssages.ScoreAlreadyExist) ; 

            var grade = firstCheck.grade ; 
            grade.Value = CalculateNewScore(grade.Value,grade.Count,newScore) ; 
            grade.Count+=1 ; 
            var newVote = new VotingRelationship {VoterId =UserId() ,Value=newScore} ;  
            grade.Votes.Add(newVote) ; 
             uow.GradeRepo.Update(grade) ;
            var transactionResult = await uow.SaveChangesAsync() ; 
            if(!transactionResult)
                return Error.ToError(ErrorMEssages.TransactionFail) ; 
            
            return Error.WithoutError() ; 
        }

        public async Task<Error> AddMusicGrade(string musicId, double newScore)
        {
            var firstCheck = await CheckUserHasGivenScoreAsync(musicId , GradeTypeEnum.MUSIC) ; 
            if(firstCheck.scored)
                return Error.ToError(ErrorMEssages.ScoreAlreadyExist) ; 

            var grade = firstCheck.grade ; 
            grade.Value = CalculateNewScore(grade.Value ,grade.Count,newScore) ;
            grade.Count+=1;  
            var newVote = new VotingRelationship {VoterId =UserId() ,Value = newScore } ; 
            grade.Votes.Add(newVote); 
            uow.GradeRepo.Update(grade) ; 
            var transactionResult = await uow.SaveChangesAsync() ; 
            if(!transactionResult)
                return Error.ToError(ErrorMEssages.TransactionFail) ; 
            return Error.WithoutError() ; 
        }

        public async Task<Error> AddSignerGrade(string signerId, double newScore)
        {
            var firstCheck = await CheckUserHasGivenScoreAsync(signerId , GradeTypeEnum.SIGNER) ; 
            if(firstCheck.scored)
                return Error.ToError(ErrorMEssages.ScoreAlreadyExist) ; 
            if(isSelfScore(signerId))
                return Error.ToError(ErrorMEssages.SelfScoreIsNotValid) ; 

            var grade = firstCheck.grade ; 
            grade.Value = CalculateNewScore(grade.Value ,grade.Count ,newScore) ; 
            grade.Count+=1 ;
            var newVote = new VotingRelationship {VoterId=UserId() , Value=newScore} ; 
            grade.Votes.Add(newVote) ; 
            uow.GradeRepo.Update(grade) ; 
            var transactionResult = await uow.SaveChangesAsync() ; 
            if(!transactionResult)
                return Error.ToError(ErrorMEssages.TransactionFail) ; 

            return Error.WithoutError() ; 
        }
        // itemId ? itemId could be musicId,albumId,userId 
        private async Task<(bool scored, Grade grade)> CheckUserHasGivenScoreAsync(string itemId, GradeTypeEnum gradeType)
        {
            if (gradeType == GradeTypeEnum.ALBUM)
            {
                bool scored = uow.GradeRepo.FindByExpression(g => g.AlbumId == itemId && g.Votes.Where(v => v.VoterId == UserId()).Any()).Any();
                var grade = await uow.GradeRepo.GetByAlbumIdAsync(itemId);
                return (scored, grade);
            }
            else if (gradeType == GradeTypeEnum.MUSIC)
            {
                bool scored = uow.GradeRepo.FindByExpression(g => g.MusicId == itemId && g.Votes.Where(v => v.VoterId == UserId()).Any()).Any();
                var grade = await uow.GradeRepo.GetByMusicIdAsync(itemId);
                return (scored, grade);
            }
            else
            {
                bool scored = uow.GradeRepo.FindByExpression(g => g.SignerId == itemId && g.Votes.Where(v => v.VoterId == UserId()).Any()).Any();
                var grade = await uow.GradeRepo.GetBySignerIdAsync(itemId);
                return (scored, grade);
            }
        }
        private bool isSelfScore(string userId)
        {
            return userId==UserId() ; 
        }
        private double CalculateNewScore( double gradeScoreValue ,int gradeScoresCount , double newScoreValue )
        {
            return (double) gradeScoreValue + newScoreValue/(gradeScoresCount+1) ; 
        }
    }
}