using ArticleProject.Core.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ArticleProject.Data.Specifications.CategorySpecifications
{
    public class CategorySearchingItem : Specifications<Category, int>
    {
        public CategorySearchingItem(string item, int? pageIndex, int pageSize) :
            base(c => item.IsNullOrEmpty() || c.Name.Trim().ToLower().Contains(item.Trim().ToLower()) ||
                 c.Id.ToString() == item)
        {
        }
    }
}
