using ArticleProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ArticleProject.Data.Specifications
{
    public class Specifications<T, TKey> : ISpecifications<T, TKey> where T : BaseEntity<TKey>
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public int Skip { get ; set ; }
        public int Take { get ; set ; }
        public bool IsPaginated { get ; set ; } =false;

        public Specifications(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public void AddPagination (int skip, int take)
        {
            IsPaginated = true;
            Skip = skip;
            Take = take;
        }
    }
}
