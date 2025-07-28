using ADUSAPI.Services;
using ADUSAPICore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ADUSAPI.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly RefreshTokenStore _tokenStore;

        public AuthController(IConfiguration config, RefreshTokenStore tokenStore)
        {
            _config = config;
            _tokenStore = tokenStore;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            if (login.Username == "admin" && login.Password == "ChicoMara1120@!")
            {
                var tokens = GenerateTokens(login.Username);
                _tokenStore.SaveToken(login.Username, tokens.RefreshToken);
                return Ok(tokens);
            }

            return Unauthorized("Credenciais inválidas.");
        }

        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] RefreshRequest request)
        {
            if (!_tokenStore.ValidateToken(request.Username, request.RefreshToken))
                return Unauthorized("Refresh token inválido.");

            var tokens = GenerateTokens(request.Username);
            _tokenStore.SaveToken(request.Username, tokens.RefreshToken);
            return Ok(tokens);
        }

        private TokenResponse GenerateTokens(string username)
        {
            var secret = _config["JwtSettings:Secret"];
            var accessExp = int.Parse(_config["JwtSettings:AccessTokenExpirationMinutes"]);
            var refreshExp = int.Parse(_config["JwtSettings:RefreshTokenExpirationDays"]);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: new[] { new Claim(ClaimTypes.Name, username) },
                expires: DateTime.UtcNow.AddDays(accessExp),
                signingCredentials: creds);

            var refreshToken = Guid.NewGuid().ToString("N");

            return new TokenResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken
            };
        }

        public class LoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class RefreshRequest
        {
            public string Username { get; set; }
            public string RefreshToken { get; set; }
        }
    }
}