using ArticleProject.Core.Entities;
using System.Linq.Expressions;

namespace ArticleProject.Data.Specifications
{
    public interface ISpecifications<T, TKey> where T : BaseEntity<TKey>
    {
        Expression<Func<T,bool>> Criteria { get; set; }
        int Skip { get; set; }
        int Take { get; set; }
        bool IsPaginated { get; set; }
    }
}
