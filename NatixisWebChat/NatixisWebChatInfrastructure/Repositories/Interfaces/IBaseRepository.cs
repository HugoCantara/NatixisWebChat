namespace NatixisWebChatInfrastructure.Repositories.Interfaces
{
    /// <summary>Interface IBaseRepository</summary>
    public interface IBaseRepository
    {
        /// <summary>Add method</summary>
        /// <typeparam name="T">The generic class.</typeparam>
        /// <param name="entity">The entity.</param>
        public void Add<T>(T entity) where T : class;

        /// <summary>Update method</summary>
        /// <typeparam name="T">The generic class.</typeparam>
        /// <param name="entity">The entity.</param>
        public void Update<T>(T entity) where T : class;

        /// <summary>Delete method</summary>
        /// <typeparam name="T">The generic class.</typeparam>
        /// <param name="entity">The entity.</param>
        public void Delete<T>(T entity) where T : class;

        /// <summary>Save changes method</summary>
        /// <returns>bool</returns>
        bool SaveChanges();
    }
}
