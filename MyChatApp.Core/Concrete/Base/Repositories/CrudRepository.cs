using MongoDB.Driver;
using MyChatApp.Core.Abstract.Base.MongoDB;
using MyChatApp.Core.Abstract.Base.Repositories;
using MyChatApp.Domain.Abstract.Base.Entities;
using System.Linq.Expressions;

namespace MyChatApp.Core.Concrete.Base.Repositories
{
    public class CrudRepository<TEntity> : ICrudRepository<TEntity>
        where TEntity : IAudit
    {

        protected readonly IMongoCollection<TEntity> _mongoCl;

        public CrudRepository(IMongoStoreDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName) ?? throw new ArgumentNullException(nameof(settings));
            _mongoCl = database.GetCollection<TEntity>(typeof(TEntity).Name.ToLower()) ?? throw new ArgumentNullException(nameof(mongoClient));
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _mongoCl.InsertOneAsync(entity);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await _mongoCl.ReplaceOneAsync(q => q.Id == entity.Id, entity);
            return entity;
        }

        public async void DeleteAsync(TEntity entity)
        {
            await _mongoCl.DeleteOneAsync(q => q.Id == entity.Id);
        }

        public async void DeleteAsync(string Id)
        {
            await _mongoCl.DeleteOneAsync(q => q.Id == Id);
        }

        public async Task<IList<TEntity>> GetAllAsync()
        {
            var result = await _mongoCl.FindAsync(q => true);
            return result.ToList();
        }

        public async Task<TEntity> GetByFilterAsync(Expression<Func<TEntity, bool>> selector)
        {
            var result = await _mongoCl.FindAsync(selector);
            return result.FirstOrDefault();
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            var result = await _mongoCl.FindAsync(q => q.Id == id);
            return result.FirstOrDefault();
        }

        public async Task<IList<TEntity>> GetListByFilterAsync(Expression<Func<TEntity, bool>> selector)
        {
            var result = await _mongoCl.FindAsync(selector);
            return result.ToList();
        }
    }
}
