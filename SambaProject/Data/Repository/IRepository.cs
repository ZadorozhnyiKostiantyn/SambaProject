using SambaProject.Data.Models;

namespace SambaProject.Data.Repository
{
    public interface IRepository<TEntity> where TEntity: class, IEntity
    {
        public Task AddAsync(TEntity entity);
        public Task DeleteAsync(int id);
        public Task Update(TEntity newData);
        public Task<TEntity?> GetByIdAsync(int id);
        public Task<List<TEntity>> GetAllAsync();
        public TEntity? GetById(int id);
        public List<TEntity> GetAll();

    }
}
