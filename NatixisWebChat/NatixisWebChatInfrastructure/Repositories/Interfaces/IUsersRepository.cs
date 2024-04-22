namespace NatixisWebChatInfrastructure.Repositories.Interfaces
{
    using NatixisWebChatDomain.AppEntities;

    /// <summary>Interface IUsersRepository</summary>
    public interface IUsersRepository : IBaseRepository
    {
        /// <summary>Get all users</summary>
        /// <returns>List<UserEntity></returns>
        public List<UserEntity> GetUserCollection();

        /// <summary>Get user by id</summary>
        /// <param name="userId">The user identification.</param>
        /// <returns>UserEntity?</returns>
        public UserEntity? GetUserById(int userId);

        /// <summary>Get user by username</summary>
        /// <param name="username">The username.</param>
        /// <returns>UserEntity?</returns>
        public UserEntity? GetUserByUsername(string username);

        /// <summary>Add new registered user to database</summary>
        /// <param name="user">The user.</param>
        public void AddUser(UserEntity user);

        /// <summary>
        /// Update user password
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>UserEntity</returns>
        public UserEntity UpdateUserPassword(UserEntity user);
    }
}
