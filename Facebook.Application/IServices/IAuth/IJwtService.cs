using Facebook.Application.Dtos.Auth;
using Facebook.Application.Dtos.Users;
using Facebook.Domain.Entities.Auth;
using Facebook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Facebook.Application.IServices.IAuth
{
    public interface IJwtService
    {
        Token GenerateToken(Guid userId);
        Token RefreshToken(Token token);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
