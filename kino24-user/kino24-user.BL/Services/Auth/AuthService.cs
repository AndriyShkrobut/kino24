using kino24_user.BL.DTO.UserAuthentication;
using kino24_user.BL.Interfaces.Auth;
using kino24_user.core.Entities.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace kino24_user.BL.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AuthService(UserManager<User> userManager,
                           SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        ///<inheritdoc/>
        public async Task<string> AddTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return code;
        }

        ///<inheritdoc/>
        public async Task<IdentityResult> CreateUserAsync(RegisterDto registerDto)
        {
            var user = new User()
            {
                Email = registerDto.Email,
                UserName = registerDto.Email,
                LastName = registerDto.Surname,
                FirstName = registerDto.Name,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            return result;
        }

        ///<inheritdoc/>
        public async Task<User> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        ///<inheritdoc/>
        public async Task<User> FindByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        ///<inheritdoc/>
        public async Task<string> GenerateConfToken(User user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        ///<inheritdoc/>
        public AuthenticationProperties GetAuthProperties(string provider, string returnUrl)
        {
            AuthenticationProperties properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, returnUrl);
            return properties;
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<AuthenticationScheme>> GetAuthSchemesAsync()
        {
            var externalLogins = await _signInManager.GetExternalAuthenticationSchemesAsync();
            return externalLogins;
        }

        ///<inheritdoc/>
        public async Task<string> GetIdForUserAsync(User user)
        {
            return await _userManager.GetUserIdAsync(user);
        }

        ///<inheritdoc/>
        public async Task<ExternalLoginInfo> GetInfoAsync()
        {
            return await _signInManager.GetExternalLoginInfoAsync();
        }

        ///<inheritdoc/>
        public async Task<SignInResult> GetSignInResultAsync(ExternalLoginInfo externalLoginInfo)
        {
            SignInResult signInResult = await _signInManager.ExternalLoginSignInAsync(externalLoginInfo.LoginProvider,
                    externalLoginInfo.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            return signInResult;
        }

        ///<inheritdoc/>
        public async Task<bool> RefreshSignInAsync(User user)
        {
            try
            {
                await _signInManager.RefreshSignInAsync(user);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        ///<inheritdoc/>
        public async Task<SignInResult> SignInAsync(LoginDto loginDto)
        {
            var user = _userManager.FindByEmailAsync(loginDto.Email);
            var result = await _signInManager.PasswordSignInAsync(user.Result, loginDto.Password, loginDto.RememberMe, true);
            return result;
        }

        ///<inheritdoc/>
        public async void SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
