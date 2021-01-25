using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using music.Domain.Entities;
using music.Domain.Services;
using music.Infrastructure.Common.Common;

namespace music.Infrastructure.Common.Services
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly UserManager<User> userManager;
        private readonly AuthConfigurations config;
        public TokenGenerator(UserManager<User> userManager, AuthConfigurations config)
        {
            this.config = config;
            this.userManager = userManager;

        }
        public async Task<Token> GenerateTokenAsync(User user)
        {
            
            List<Claim> claims = await GetClaims(user) ; 
            var accessToken =  GenerateTokenByClaims(claims) ; 
            var refreshToken = RefreshTokenGeneretor() ; 
            return new Token {AccessToken = accessToken , RefreshToken = refreshToken} ; 
        }
        private string RefreshTokenGeneretor()
        {
            var randNumber = new byte[32] ; 
            using(var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randNumber) ; 
                return Convert.ToBase64String(randNumber) ; 
            }
        }
        public string GetUserIdFromExpiredToken(string token)
        {
            var tokenHnadler = new JwtSecurityTokenHandler() ;
            var tokenValidatorParams = new TokenValidationParameters
            {
                ValidateIssuer = true  , 
                ValidIssuer= config.Issuer , 
                ValidateAudience = false ,
                ValidateIssuerSigningKey = true ,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Key)) , 
                ValidateLifetime = false
            } ; 
            SecurityToken securityToken ; 
            var princ = tokenHnadler.ValidateToken(token , tokenValidatorParams ,out securityToken) ;  
            var jwtSecurityToken = securityToken as JwtSecurityToken ;
            if(jwtSecurityToken==null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256 , StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("توکن معتبر نمیباشد") ; 
            }
            return princ.FindFirst(ClaimTypes.NameIdentifier).Value ;
        }


        private async Task<List<Claim>> GetClaims(User user) {
            IEnumerable<string> roles = await userManager.GetRolesAsync(user);
            List<Claim> claims = new List<Claim>()  ;
            foreach(var role in roles) {
                var claim = new Claim(ClaimTypes.Role , role) ; 
                claims.Add(claim)  ;
            }
            var idClaim = new Claim(ClaimTypes.NameIdentifier  , user.Id ) ; 
            claims.Add(idClaim) ;
            return claims ;  
        }
        private string GenerateTokenByClaims(List<Claim> claims) {
            var tokenHandler = new JwtSecurityTokenHandler() ;
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config.Key))  ;  
            var tokenDescription = new SecurityTokenDescriptor {
                Issuer = config.Issuer , 
                
                Subject = new ClaimsIdentity(claims) , 
                Expires = DateTime.UtcNow.AddMinutes(config.ExpirationTime) ,
                 SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            } ; 
            var token = tokenHandler.CreateToken(tokenDescription) ;
           return tokenHandler.WriteToken(token) ; 
        }  
    }
}