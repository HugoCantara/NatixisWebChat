namespace NatixisWebChatInfrastructure.Repositories
{
    using NatixisWebChatDomain.AppEntities;
    using NatixisWebChatInfrastructure.Context;
    using NatixisWebChatInfrastructure.Repositories.Interfaces;
    using System.Collections.Generic;

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

        /// <summary>
        /// Update user password
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>UserEntity</returns>
        public UserEntity UpdateUserPassword(UserEntity user)
        {
            _context.Update(user);
            SaveChanges();
            return user;
        }
    }
}
