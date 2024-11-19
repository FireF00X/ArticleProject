using ArticleProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleProject.Data.Specifications.PostsSpecifications
{
    public class PostsForHomeSpecs : Specifications<AuthorPost,int>
    {
        public PostsForHomeSpecs(string? catName,string? searchingItem):
            base(p=>(string.IsNullOrEmpty(catName)||
            p.PostCategory == catName)&&
            (string.IsNullOrEmpty(searchingItem) ||
            p.PostTitle.Trim().ToLower().Contains(searchingItem.Trim().ToLower())))
        {
            
        }
    }
}
