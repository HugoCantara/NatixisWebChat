namespace NatixisWebChatInfrastructure.Services
{
    using NatixisWebChatDomain.AppEntities;
    using NatixisWebChatInfrastructure.Repositories.Interfaces;
    using System.Security.Claims;

    public class UserServices
    {
        /// <summary>The generic repository</summary>
        private readonly IBaseRepository<UserEntity> _userEntityRepository;

        /// <summary>The constructor</summary>
        /// <param name="userEntityRepository">The generic repository</param>
        public UserServices(IBaseRepository<UserEntity> userEntityRepository)
        {
            _userEntityRepository = userEntityRepository;
        }

        /// <summary>Get user by id</summary>
        /// <param name="userId">The user identification.</param>
        /// <returns>UserEntity?</returns>
        public UserEntity? GetUserById(int userId)
        {
            var userCollection = _userEntityRepository.GetAllAsync().Result.ToList();
            return userCollection.FirstOrDefault(u => u.UserId == userId);
        }

        /// <summary>Get user by username</summary>
        /// <param name="username">The username.</param>
        /// <returns>UserEntity?</returns>
        public UserEntity? GetUserByUsername(string username)
        {
            var userCollection = _userEntityRepository.GetAllAsync().Result.ToList();
            return userCollection.FirstOrDefault(u => u.Username == username);
        }

        /// <summary>Get user by username and email</summary>
        /// <param name="user">The user</param>
        /// <returns>UserEntity?</returns>
        public UserEntity? GetUserByUsernameAndEmail(UserEntity user)
        {
            var userCollection = _userEntityRepository.GetAllAsync().Result.ToList();
            return userCollection.FirstOrDefault(u => u.Username == user.Username && u.Email == user.Email);
        }

        /// <summary>Add new registered user to database</summary>
        /// <param name="user">The user.</param>
        public void AddUser(UserEntity user)
        {
            _userEntityRepository.AddAsync(new UserEntity()
            {
                Username = user.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                Email = user.Email
            });
        }

        /// <summary>Update user password</summary>
        /// <param name="user">The user.</param>
        /// <returns>UserEntity</returns>
        public UserEntity UpdateUserPassword(UserEntity user)
        {
            _userEntityRepository.UpdateAsync(user);
            return user;
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

            return this.GetUserById(userId);
        }
    }
}
