namespace NatixisWebChatInfrastructure.Repositories.Interfaces
{
    using NatixisWebChatDomain.AppEntities;
    using System.Security.Claims;

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

        /// <summary>Method to get user that is authenticated by id stored in claims</summary>
        /// <param name="principal">The principal claims.</param>
        /// <returns>UserEntity?</returns>
        public UserEntity? GetUserByClaim(ClaimsPrincipal principal);

        /// <summary>Check if user with such username & password exists and return user</summary>
        /// <param name="user">The user.</param>
        /// <returns>UserEntity?</returns>
        public UserEntity? GetValidUser(UserEntity user);

        /// <summary>Add new registered user to database</summary>
        /// <param name="user">The user.</param>
        public void AddUser(UserEntity user);

        /// <summary>Update user password</summary>
        /// <param name="user">The user.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>UserEntity</returns>
        public UserEntity UpdateUserPassword(UserEntity user, string newPassword);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public UserEntity? UpdateUserPasswordByEmail(UserEntity user, string newPassword);
    
    }
}
