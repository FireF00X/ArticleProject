using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleProject.Core.Entities
{
    public class Author : BaseEntity<int>
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string? PictureUrl { get; set; }
        public string? Bio { get; set; }
        public string? FaceBookLink { get; set; }
        public string? InstegramLink { get; set; }
        public string? XSiteLink { get; set; }

        public List<AuthorPost> AuthorPosts { get; set; }
    }
}
