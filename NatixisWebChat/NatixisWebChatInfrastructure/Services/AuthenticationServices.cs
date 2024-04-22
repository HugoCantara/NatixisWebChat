namespace NatixisWebChatInfrastructure.Services
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Http;
    using NatixisWebChatDomain.AppEntities;
    using NatixisWebChatInfrastructure.Repositories.Interfaces;
    using System.Security.Claims;

    public class AuthenticationServices
    {
        /// <summary>The users repository</summary>
        private readonly IUsersRepository _usersRepository;

        /// <summary>The constructor</summary>
        /// <param name="usersRepository">The users repository.</param>
        public AuthenticationServices(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        /// <summary>Check if user with such username & password exists and return user</summary>
        /// <param name="user">The user.</param>
        /// <returns>UserEntity?</returns>
        public UserEntity? GetValidUser(UserEntity user)
        {
            return _usersRepository.GetUserCollection().FirstOrDefault(u => u.Username == user.Username && BCrypt.Net.BCrypt.Verify(user.Password, u.Password));
        }

        /// <summary>Method to get user that is authenticated by id stored in claims</summary>
        /// <param name="principal">The principal claims.</param>
        /// <returns>UserEntity?</returns>
        public UserEntity? GetUserByClaim(ClaimsPrincipal principal)
        {
            int userId;

            //get user claim(by id)
            var userClaim = principal.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();

            if (!Int32.TryParse(userClaim, out userId))
            {
                return null;
            }

            return _usersRepository.GetUserById(userId);
        }

        /// <summary>Method to sign in user, asign claims</summary>
        /// <param name="user">The user.</param>
        /// <param name="context">The Http context.</param>
        public static async Task CreateAuthentication(UserEntity user, HttpContext context)
        {
            var claimCollection = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),    
                new Claim(ClaimTypes.Name, user.Username)    
            };

            var claimsIdentity = new ClaimsIdentity(claimCollection, CookieAuthenticationDefaults.AuthenticationScheme);

            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties());
        }
    }
}
