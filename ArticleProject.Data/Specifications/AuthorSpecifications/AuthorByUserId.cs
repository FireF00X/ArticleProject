using ArticleProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleProject.Data.Specifications.AuthorSpecifications
{
    public class AuthorByUserId : Specifications<Author,int>
    {
        public AuthorByUserId(string userId): base(a=>a.UserId == userId)
        {
            
        }
    }
}
