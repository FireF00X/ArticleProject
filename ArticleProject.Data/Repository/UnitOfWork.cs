using ArticleProject.Core.Entities;
using ArticleProject.Data.IRepository;
using ArticleProject.Data.ProjectData.Contexts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleProject.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ArticleDbContext _context;
        private Hashtable _repos;

        public UnitOfWork(ArticleDbContext context)
        {
            _context = context;
        }
        public async Task CompleteAsync()
        => await _context.SaveChangesAsync();

        public IGenericRepository<T, TKey> CreateRepo<T, TKey>() where T : BaseEntity<TKey>
        {
            if(_repos is null) _repos = new Hashtable();

            var tableKey = typeof(T).Name;
            if(!_repos.ContainsKey(tableKey))
            {
                var tableValue = new GenericRepository<T, TKey>(_context);
                _repos.Add(tableKey, tableValue );
            }
            return _repos[tableKey] as IGenericRepository<T,TKey>;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
