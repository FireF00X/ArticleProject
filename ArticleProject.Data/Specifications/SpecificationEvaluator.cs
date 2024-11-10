using ArticleProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleProject.Data.Specifications
{
    public static class SpecificationEvaluator<T, TKey> where T : BaseEntity<TKey>
    {
        public static IQueryable<T> GetQuery(IQueryable<T> sequence, ISpecifications<T, TKey> specs)
        {
            var query = sequence;
            if(specs.Criteria is not null)            
                query = query.Where(specs.Criteria);

            if(specs.IsPaginated)
                query = query.Skip(specs.Skip).Take(specs.Take);
            
            return query;
        }
    }
}
