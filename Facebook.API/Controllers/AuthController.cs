﻿using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Facebook.Application.IServices.IAuth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Facebook.Domain.Entities.Auth;
using Facebook.Application.Dtos.Auth;
using Facebook.Application.Dtos.Users;

namespace Facebook.API.Controllers
{
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        [HttpPost("sign-in")]
        public async Task<UserDto> SignIn([FromBody] SignInDto body)
        {
            var result = await _authService.SignInAsync(body);

            return result;
        }

        [HttpPost("sign-up")]
        public async Task<UserDto> SignUp([FromBody] SignUpDto body)
        {
            var result = await _authService.SignUpAsync(body);

            return result;
        }

        [HttpGet("sign-in-google")]
        public IActionResult SignInGoogle()
        {
            var redirectUri = Url.Action("GoogleCallback", "Auth", null, Request.Scheme);
            var clientId = _configuration["Authentication:Google:ClientId"];
            var authorizationUrl = $"https://accounts.google.com/o/oauth2/v2/auth?client_id={clientId}&response_type=code&scope=openid%20profile%20email&redirect_uri={redirectUri}";
            return Redirect(authorizationUrl);
        }

        [HttpGet("google-callback")]
        public async Task<IActionResult> GoogleCallback(string code)
        {
            var clientId = _configuration["Authentication:Google:ClientId"];
            var clientSecret = _configuration["Authentication:Google:ClientSecret"];
            var redirectUri = _configuration["Authentication:Google:RedirectUri"];

            var tokenRequest = new HttpClient();
            var tokenResponse = await tokenRequest.PostAsync("https://oauth2.googleapis.com/token", new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "code", code },
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "redirect_uri", redirectUri },
                { "grant_type", "authorization_code" }
            }));

            // Register JWT Token by google id Token
            var tokenResult = await tokenResponse.Content.ReadFromJsonAsync<GoogleTokenResponse>();
            var googleIdToken = tokenResult.IdToken;

            var jwtToken = GenerateJwtToken(googleIdToken);
            var script = $@"
                <script>
                    window.opener.postMessage({{ token: '{jwtToken}' }}, '*');
                    window.close();
                </script>";
            return Content(script, "text/html");
        }

        private string GenerateJwtToken(string idToken)
        {
            var key = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var expireMinutes = int.Parse(_configuration["Jwt:ExpireMinutes"]);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, idToken),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var keyBytes = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(keyBytes, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(expireMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
