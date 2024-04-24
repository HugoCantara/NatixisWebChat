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
        /// <summary>The generic repository</summary>
        private readonly IBaseRepository<UserEntity> _userEntityRepository;

        /// <summary>The constructor</summary>
        /// <param name="userEntityRepository">The generic repository</param>
        public AuthenticationServices(IBaseRepository<UserEntity> userEntityRepository)
        {
            _userEntityRepository = userEntityRepository;
        }

        /// <summary>Check if user with such username & password exists and return user</summary>
        /// <param name="user">The user.</param>
        /// <returns>UserEntity?</returns>
        public UserEntity? GetValidUser(UserEntity user)
        {
            var userCollection = _userEntityRepository.GetAllAsync().Result.ToList();
            return userCollection.FirstOrDefault(u => u.Username == user.Username && BCrypt.Net.BCrypt.Verify(user.Password, u.Password));
        }

        /// <summary>Method to sign in user, asign claims</summary>
        /// <param name="user">The user.</param>
        /// <param name="context">The Http context.</param>
        public async Task CreateAuthentication(UserEntity user, HttpContext context)
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
