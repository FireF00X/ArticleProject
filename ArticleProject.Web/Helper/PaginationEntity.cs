using ArticleProject.Core.Entities;
using ArticleProject.Data.Specifications;
using System.Linq.Expressions;

namespace ArticleProject.Web.Helper
{
    public class PaginationEntity<T>
    {
        public PaginationEntity(int pageSize, int pageNumber, int totalCount, IReadOnlyList<T> data)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
            TotalCount = totalCount;
            Data = data;
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalCount { get; set; }
        public IReadOnlyList<T> Data { get; set; }

    }
}
