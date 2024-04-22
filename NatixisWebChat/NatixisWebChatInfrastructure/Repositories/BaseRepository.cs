namespace NatixisWebChatInfrastructure.Repositories
{
    using NatixisWebChatInfrastructure.Context;
    using NatixisWebChatInfrastructure.Repositories.Interfaces;

    /// <summary>Class BaseRepository</summary>
    public class BaseRepository : IBaseRepository
    {
        /// <summary>The context</summary>
        private readonly NatixisDbContext _context;

        /// <summary>The constructor</summary>
        /// <param name="context">The context</param>
        public BaseRepository(NatixisDbContext context)
        {
            _context = context;
        }

        /// <summary>Add method</summary>
        /// <typeparam name="T">The generic class.</typeparam>
        /// <param name="entity">The entity.</param>
        public void Add<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        /// <summary>Update method</summary>
        /// <typeparam name="T">The generic class.</typeparam>
        /// <param name="entity">The entity.</param>
        public void Update<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        /// <summary>Delete method</summary>
        /// <typeparam name="T">The generic class.</typeparam>
        /// <param name="entity">The entity.</param>
        public void Delete<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        /// <summary>Save changes method</summary>
        /// <returns>bool</returns>
        public bool SaveChanges()
        {
            _context.SaveChanges();
            return true;
        }     
    }
}
