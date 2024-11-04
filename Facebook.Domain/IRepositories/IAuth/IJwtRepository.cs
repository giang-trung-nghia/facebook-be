using Facebook.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Domain.IRepositories.IAuth
{
    public interface IJwtRepository
    {
        Token GenerateToken(string userName);
        Token GenerateRefreshToken(string userName);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
