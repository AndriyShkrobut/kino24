using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using kino24_user.BL.Interfaces;
using kino24_user.BL.Interfaces.Jwt;
using kino24_user.BL.Interfaces.UniqueId;
using kino24_user.core.Entities.User;

namespace kino24_user.BL.Services.Jwt
{
    public class JwtService : IJwtService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly IUniqueIdService _uniqueId;

        public JwtService(IOptions<JwtOptions> jwtOptions, IUniqueIdService uniqueId)
        {
            _jwtOptions = jwtOptions.Value;
            _uniqueId = uniqueId;
        }

        ///<inheritdoc/>
        public async Task<string> GenerateJWTTokenAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                new Claim(JwtRegisteredClaimNames.Name, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.Jti, _uniqueId.GetUniqueId().ToString())
            };
            var tmp = _jwtOptions.Key;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              issuer: _jwtOptions.Issuer,
              audience: _jwtOptions.Audience,
              claims: claims,
              expires: DateTime.Now.AddMinutes(_jwtOptions.Time),
              signingCredentials: creds);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
