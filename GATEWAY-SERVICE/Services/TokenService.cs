using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GATEWAY_SERVICE.Modals;

namespace GATEWAY_SERVICE.Services
{
    public class JwtTokenService : ITokenService
    {
        private readonly string _tokenSecret;
        private readonly string _refreshTokenSecret;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly TimeSpan _tokenLifetime;
        private readonly TimeSpan _refreshTokenLifetime;

        public JwtTokenService(IOptions<JwtSettings> jwtSettings)
        {
            var settings = jwtSettings.Value;

            _tokenLifetime = TimeSpan.FromHours(8);
            _refreshTokenLifetime = TimeSpan.FromDays(29);

            _tokenSecret = settings.Key;
            _refreshTokenSecret = settings.RefreshTokenKey;
            _issuer = settings.Issuer;
            _audience = settings.Audience;

            // Check if values are missing or empty
            ValidateJwtSettings();
        }

        private void ValidateJwtSettings()
        {
            if (string.IsNullOrEmpty(_tokenSecret) || string.IsNullOrEmpty(_issuer) || string.IsNullOrEmpty(_audience))
            {
                throw new InvalidOperationException("Token settings are missing or incomplete.");
            }
        }

        public string GenerateToken(UserViewModel request)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_tokenSecret);

            // Liste des revendications de base
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, request.Email),
                new Claim(JwtRegisteredClaimNames.Email, request.Email),
                new Claim("userId", request.Id.ToString())
            };

            // Configurer le descripteur du token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_tokenLifetime),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            // Créer et écrire le token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            return jwt;
        }

        public string GenerateRefreshToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_refreshTokenSecret);

            var refreshTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.Add(_refreshTokenLifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var refreshToken = tokenHandler.CreateToken(refreshTokenDescriptor);
            var jwtRefreshToken = tokenHandler.WriteToken(refreshToken);

            return jwtRefreshToken;
        }
    }
}
