using ArticleProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleProject.Data.Specifications.AuthorSpecifications
{
    public class AuthorSearchingItem : Specifications<Author,int>
    {
        public AuthorSearchingItem(string item):
            base(a=>a.UserName.Trim().ToLower().Contains(item.Trim().ToLower())||
                 a.FullName.Trim().ToLower().Contains(item.Trim().ToLower())||
                 a.Id.ToString() == item||
                 a.UserId == item)
        {
            
        }
    }
}
