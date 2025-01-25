namespace PruebaServidor.Repositories
{
    public interface IRepository<TEntity, TId> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(TId id); 
    }
}
