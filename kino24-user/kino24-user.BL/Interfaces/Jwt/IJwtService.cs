using kino24_user.core.Entities.User;

namespace kino24_user.BL.Interfaces.Jwt
{
    public interface IJwtService
    {
        /// <summary>
        /// Generting JWT token
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns>Returns generated JWT token</returns>
        Task<string> GenerateJWTTokenAsync(User user);
    }
}
