using ArticleProject.Core.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleProject.Data.Specifications.PostsSpecifications
{
    public class PostsSearchingItem : Specifications<AuthorPost, int>
    {
        public PostsSearchingItem( string userId,string? item) :
            base(a => a.UserId == userId &&
                 (item.IsNullOrEmpty()||
                 a.UserName.Trim().ToLower().Contains(item.Trim().ToLower())))
        {

        }
        public PostsSearchingItem(string? item) :
            base(a=>
                 item.IsNullOrEmpty()||
                 a.UserName.Trim().ToLower().Contains(item.Trim().ToLower()))
        {

        }
    }
}
