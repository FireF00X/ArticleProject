using ArticleProject.Core.Entities;
using ArticleProject.Data.IRepository;
using ArticleProject.Data.ProjectData.Contexts;
using ArticleProject.Data.Specifications;
using Microsoft.EntityFrameworkCore;

namespace ArticleProject.Data.Repository
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        internal readonly ArticleDbContext _context;

        public GenericRepository(ArticleDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddAsync(TEntity entity)
        {
            if(_context.Database.CanConnect())
            {
               await _context.AddAsync(entity);
                return 1;
            }
            return 0;
        }

        public int Delete(TKey id)
        {
            if (_context.Database.CanConnect())
            {
                var entity = _context.Set<TEntity>().Find(id);
                _context.Remove(entity);
                return 1;
            }
            return 0;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<List<TEntity>> GetAllAsyncWithSpecification(ISpecifications<TEntity, TKey> specs)
        {
            return await SpecificationEvaluator<TEntity, TKey>.GetQuery(_context.Set<TEntity>(),specs).ToListAsync();
        }

        public async Task<int> GetAllEntityCountAsync(ISpecifications<TEntity, TKey> specs)
        {
            return await SpecificationEvaluator<TEntity,TKey>.GetQuery(_context.Set<TEntity>(),specs).CountAsync();
        }

        public TEntity GetById(TKey id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public int Update(TKey id, TEntity entity)
        {
            if (_context.Database.CanConnect())
            {
                _context.Update(entity);
                return 1;
            }
            return 0;
        }
    }
}
