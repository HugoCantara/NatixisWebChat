namespace NatixisWebChatInfrastructure.Repositories.Interfaces
{
    /// <summary>Interface IBaseRepository</summary>
    /// <typeparam name="T">Generic class</typeparam>
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}
