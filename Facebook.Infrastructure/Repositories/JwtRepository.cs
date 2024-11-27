using Facebook.Domain.Const;
using Facebook.Domain.Entities.Auth;
using Facebook.Domain.Exceptions;
using Facebook.Domain.IRepositories;
using Facebook.Domain.IRepositories.IAuth;
using Facebook.Domain.IRepositories.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Infrastructure.Repositories
{
    public class JwtRepository : IJwtRepository
    {

        private readonly IConfiguration _configuration;
        private readonly Func<IAuthRepository> _authRepositoryFactory;
        public JwtRepository(IConfiguration configuration, Func<IAuthRepository> authRepositoryFactory)
        {
            _configuration = configuration;
            _authRepositoryFactory = authRepositoryFactory;
        }
        private IAuthRepository _authRepository => _authRepositoryFactory();

        #region JWT token
        public Token GenerateToken(Guid userId)
        {
            var result = GenerateJWTTokens(userId.ToString());
            return result;
        }

        public Token RefreshToken(Token token)
        {
            var principal = GetPrincipalFromExpiredToken(token.AccessToken);
            var userId = principal.Claims.First(e => e.Type == "id").Value;
            var oldToken = _authRepository.GetSavedRefreshTokens(Guid.Parse(userId), token.RefreshToken);

            if (oldToken.RefreshToken != token.RefreshToken)
            {
                throw new NotFoundException();
            }
            if (oldToken.ExpiredDate < DateTime.Now)
            {
                throw new UnauthorizationException($"Refresh token {token.RefreshToken} is expired", "Please login again");
            }

            var newAccessToken = GenerateAccessToken(userId);

            return new Token {AccessToken=newAccessToken, RefreshToken=token.RefreshToken };
        }


        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var Key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        #endregion

        #region Private Function
        private Token GenerateJWTTokens(string id)
        {
            try
            {
                var accessToken = GenerateAccessToken(id);
                var refreshToken = GenerateRefreshTokenEntity(id);

                return new Token { AccessToken = accessToken, RefreshToken = refreshToken.RefreshToken };
            }
            catch (Exception ex)
            {
                throw new UnauthorizationException("Generate JWT Tokens fail, please check");
            }
        }

        private string GenerateAccessToken(string id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(FbJwtClaimType.Id, id)
                }),
                Expires = DateTime.Now.AddMinutes(int.Parse(_configuration["JWT:ExpireMinutes"])),
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var result = tokenHandler.WriteToken(token);
            return result;
        }

        private UserRefreshTokenEntity GenerateRefreshTokenEntity(string id)
        {
            UserRefreshTokenEntity entity = new UserRefreshTokenEntity
            {
                RefreshToken = GenerateRefreshToken(),
                UserId = Guid.Parse(id),
                ExpiredDate = DateTime.Now.AddMinutes(int.Parse(_configuration["JWT:ExpireRefreshToken"]))
            };
            _authRepository.AddUserRefreshTokens(entity);

            return entity;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        #endregion

    }
}
