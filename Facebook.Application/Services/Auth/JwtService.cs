using Facebook.Application.Dtos.Users;
using Facebook.Application.IServices.IAuth;
using Facebook.Domain.Entities;
using Facebook.Domain.Entities.Auth;
using Facebook.Domain.IRepositories;
using Facebook.Domain.IRepositories.IAuth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Application.Services.Jwt
{
    public class JwtService : IJwtService
    {
        private readonly IJwtRepository _JwtRepository;

        public JwtService(IJwtRepository jwtRepository)
        {
            _JwtRepository = jwtRepository;
        }

        public Token GenerateRefreshToken(Guid userId)
        {
            var result = _JwtRepository.GenerateRefreshToken(userId);
            return result;
        }

        public Token GenerateToken(Guid userId)
        {
            var result = _JwtRepository.GenerateToken(userId);
            return result;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var result = _JwtRepository.GetPrincipalFromExpiredToken(token);
            return result;
        }
    }
}
