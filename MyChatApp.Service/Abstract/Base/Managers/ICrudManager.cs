using System.Linq.Expressions;

namespace MyChatApp.Service.Abstract.Base.Managers
{
    public interface ICrudManager<TEntity>
    {
        Task<TEntity> GetByIdAsync(string id);
        Task<IList<TEntity>> GetAllAsync();

        Task<TEntity> GetByFilterAsync(Expression<Func<TEntity, bool>> selector);
        Task<IList<TEntity>> GetListByFilterAsync(Expression<Func<TEntity, bool>> selector);

        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        void DeleteAsync(TEntity entity);

        void DeleteAsync(string Id);
    }
}
