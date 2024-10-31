using System.Linq.Expressions;

namespace EventManagement.Data.Interface
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllPagedAsync(
            Func<IQueryable<TEntity>, IQueryable<TEntity>>? func = null,
            int pageIndex = 1, int pageSize = int.MaxValue);
        IQueryable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> GetById(long id);
        Task Create(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(long id);
    }
}
