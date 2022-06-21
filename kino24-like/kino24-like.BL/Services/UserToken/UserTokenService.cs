using kino24_like.BL.Interfaces;
using kino24_like.BL.Models;
using System.IdentityModel.Tokens.Jwt;

namespace kino24_like.BL.Services
{
    public class UserTokenService : IUserTokenService
    {
        public UserTokenService() { }

        public User GetUserFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtclaims = handler.ReadJwtToken(token).Claims; 

            return new User()
            {
                Id = jwtclaims.First(claim => claim.Type == "nameid").Value,
                FirstName = jwtclaims.First(claim => claim.Type == "name").Value,
                LastName = jwtclaims.First(claim => claim.Type == "family_name").Value
            };
        }
    }
}
