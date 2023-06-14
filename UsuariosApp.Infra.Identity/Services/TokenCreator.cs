using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Application.Interfaces.Identities;
using UsuariosApp.Infra.Identity.Settings;

namespace UsuariosApp.Infra.Identity.Services
{
    public class TokenCreator : ITokenCreator
    {
        private readonly IdentitySettings? _identitySettings;

        public TokenCreator(IOptions<IdentitySettings>? identitySettings)
        {
            _identitySettings = identitySettings?.Value;
        }

        public string Create(string userName, string userRole)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, userRole),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_identitySettings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _identitySettings.Issuer,
                audience: _identitySettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            var tokenHanlder = new JwtSecurityTokenHandler();
            return tokenHanlder.WriteToken(token);
        }
    }
}
