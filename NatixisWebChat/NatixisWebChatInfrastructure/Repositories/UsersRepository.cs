namespace NatixisWebChatInfrastructure.Repositories
{
    using NatixisWebChatDomain.AppEntities;
    using NatixisWebChatInfrastructure.Context;
    using NatixisWebChatInfrastructure.Repositories.Interfaces;
    using System.Collections.Generic;
    using System.Security.Claims;

    /// <summary>Class UsersRepository</summary>
    public class UsersRepository : BaseRepository, IUsersRepository
    {
        /// <summary>The context</summary>
        private readonly NatixisDbContext _context;

        /// <summary>The constructor</summary>
        /// <param name="context">The context.</param>
        public UsersRepository(NatixisDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="user">The user.</param>
        private void RegisterUser(UserEntity user)
        {
            _context.Users.Add(user);
            SaveChanges();
        }

        /// <summary>
        /// Update user password
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>UserEntity</returns>
        private UserEntity UpdateUserPassword(UserEntity user)
        {
            _context.Update(user);
            SaveChanges();
            return user;
        }

        /// <summary>Get all users</summary>
        /// <returns>List<UserEntity></returns>
        public List<UserEntity> GetUserCollection()
        {
            return _context.Users.ToList();
        }

        /// <summary>Get user by id</summary>
        /// <param name="userId">The user identification.</param>
        /// <returns>UserEntity?</returns>
        public UserEntity? GetUserById(int userId)
        {
            return this.GetUserCollection().FirstOrDefault(u => u.UserId == userId);
        }

        /// <summary>Get user by username</summary>
        /// <param name="username">The username.</param>
        /// <returns>UserEntity?</returns>
        public UserEntity? GetUserByUsername(string username)
        {
            return this.GetUserCollection().FirstOrDefault(u => u.Username == username);
        }

        /// <summary>Check if user with such username & password exists and return user</summary>
        /// <param name="user">The user.</param>
        /// <returns>UserEntity?</returns>
        public UserEntity? GetValidUser(UserEntity user)
        {
            return this.GetUserCollection().FirstOrDefault(u => u.Username == user.Username && BCrypt.Net.BCrypt.Verify(user.Password, u.Password));
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

        /// <summary>Add new registered user to database</summary>
        /// <param name="user">The user.</param>
        public void AddUser(UserEntity user)
        {
            this.RegisterUser(new UserEntity()
            {
                Username = user.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                Email = user.Email
            });
        }

        /// <summary>Update user password</summary>
        /// <param name="user">The user.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>UserEntity</returns>
        public UserEntity UpdateUserPassword(UserEntity user, string newPassword)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            return this.UpdateUserPassword(user);
        }

        /// <summary>Update user password by email</summary>
        /// <param name="user">The user.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>UserEntity?</returns>
        public UserEntity? UpdateUserPasswordByEmail(UserEntity user, string newPassword)
        {
            var databaseUser = this.GetUserCollection().FirstOrDefault(u => u.Username == user.Username && u.Email == user.Email);

            if (databaseUser == null)
            {
                return null;
            }

            return this.UpdateUserPassword(databaseUser, newPassword);
        }
    }
}
