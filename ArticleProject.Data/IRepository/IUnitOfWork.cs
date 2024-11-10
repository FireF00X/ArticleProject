using ArticleProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleProject.Data.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T, TKey> CreateRepo<T, TKey>() where T : BaseEntity<TKey>;
        Task CompleteAsync();
    }
}
