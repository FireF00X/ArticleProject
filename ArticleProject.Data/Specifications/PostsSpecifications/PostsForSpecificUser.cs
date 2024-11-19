using ArticleProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleProject.Data.Specifications.PostsSpecifications
{
    public class PostsForSpecificUser : Specifications<AuthorPost,int>
    {
        public PostsForSpecificUser(string userId):base(p=>p.UserId == userId)
        {
            
        }
    }
}
