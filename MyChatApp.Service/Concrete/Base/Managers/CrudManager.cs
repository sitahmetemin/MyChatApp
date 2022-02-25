using MyChatApp.Core.Abstract.Base.Repositories;
using MyChatApp.Domain.Abstract.Base.Entities;
using MyChatApp.Service.Abstract.Base.Managers;
using System.Linq.Expressions;

namespace MyChatApp.Service.Concrete.Base.Managers
{
    public class CrudManager<TEntity> : ICrudManager<TEntity>
        where TEntity : IAudit
    {
        protected readonly ICrudRepository<TEntity> _crudRepository;

        public CrudManager(ICrudRepository<TEntity> crudRepository) => _crudRepository = crudRepository;

        public async Task<TEntity> AddAsync(TEntity entity) => await _crudRepository.AddAsync(entity);

        public void DeleteAsync(TEntity entity) => _crudRepository.DeleteAsync(entity);

        public void DeleteAsync(string Id) => _crudRepository.DeleteAsync(Id);

        public async Task<IList<TEntity>> GetAllAsync() => await _crudRepository.GetAllAsync();

        public async Task<TEntity> GetByFilterAsync(Expression<Func<TEntity, bool>> selector) => await _crudRepository.GetByFilterAsync(selector);

        public async Task<TEntity> GetByIdAsync(string id) => await _crudRepository.GetByIdAsync(id);

        public async Task<IList<TEntity>> GetListByFilterAsync(Expression<Func<TEntity, bool>> selector) => await _crudRepository.GetListByFilterAsync(selector);

        public async Task<TEntity> UpdateAsync(TEntity entity) => await _crudRepository.UpdateAsync(entity);
    }
}
