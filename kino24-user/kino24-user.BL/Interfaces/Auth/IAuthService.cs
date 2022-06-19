using kino24_user.BL.DTO.UserAuthentication;
using kino24_user.core.Entities.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace kino24_user.BL.Interfaces.Auth
{
    public interface IAuthService
    {
        /// <summary>
        /// Add token for user after registration
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Result of adding token</returns>
        Task<string> AddTokenAsync(string email);

        /// <summary>
        /// Creating user in database
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns>Result of creating user in system</returns>
        Task<IdentityResult> CreateUserAsync(RegisterDto registerDto);

        /// <summary>
        /// Finding by email in database
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Returns user from database</returns>
        Task<User> FindByEmailAsync(string email);

        /// <summary>
        /// Finding by id in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns user from database</returns>
        Task<User> FindByIdAsync(string id);

        /// <summary>
        /// Get authentication properties for user
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="returnUrl"></param>
        /// <returns>Authentication properties for user</returns>
        AuthenticationProperties GetAuthProperties(string provider, string returnUrl);

        /// <summary>
        /// Get authentication scheme for user
        /// </summary>
        /// <returns>Returns authentication scheme</returns>
        Task<IEnumerable<AuthenticationScheme>> GetAuthSchemesAsync();

        /// <summary>
        /// Get id for user from database
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Returns id of user</returns>
        Task<string> GetIdForUserAsync(User user);

        /// <summary>
        /// Get information about external login
        /// </summary>
        /// <returns>External login information</returns>
        Task<ExternalLoginInfo> GetInfoAsync();

        /// <summary>
        /// Get sign in result
        /// </summary>
        /// <param name="externalLoginInfo"></param>
        /// <returns>Sign in result</returns>
        Task<SignInResult> GetSignInResultAsync(ExternalLoginInfo externalLoginInfo);

        /// <summary>
        /// Refresh signin credentials
        /// </summary>
        /// <param name="userDto"></param>
        Task<bool> RefreshSignInAsync(User user);

        /// <summary>
        /// Login in system
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns>Result of logining in system</returns>
        Task<SignInResult> SignInAsync(LoginDto loginDto);

        /// <summary>
        /// Logout in system
        /// </summary>
        void SignOutAsync();
    }
}
