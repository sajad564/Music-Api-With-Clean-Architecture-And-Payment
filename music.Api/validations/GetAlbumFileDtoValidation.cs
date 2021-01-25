using FluentValidation;
using music.Api.Dtos;

namespace music.Api.validations
{
    public class GetAlbumFileDtoValidation : AbstractValidator<GetAlbumFileDto>
    {
        public GetAlbumFileDtoValidation()
        {
            RuleFor(gaFile =>gaFile.Albumid).Must(albumId => !string.IsNullOrEmpty(albumId)).WithMessage("درخواست نامعتبر");
        }
    }
}