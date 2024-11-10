using ArticleProject.Core.Entities;
using ArticleProject.Data.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleProject.Data.IRepository
{
    public interface IGenericRepository<T,TKey> where T : BaseEntity<TKey>
    {
        // READ
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsyncWithSpecification(ISpecifications<T, TKey> spec);
        Task<int> GetAllEntityCountAsync(ISpecifications<T, TKey> specs);
        T GetById(TKey id);
        // WRITE
        Task<int> AddAsync(T entity);
        int Update(TKey id,T entity);
        int Delete(TKey id);
    }
}
