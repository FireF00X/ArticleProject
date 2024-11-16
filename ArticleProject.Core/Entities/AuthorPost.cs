using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleProject.Core.Entities
{
    public class AuthorPost : BaseEntity<int>
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string? PostImageUrl { get; set; }
        public string PostCategory { get; set; }
        public string PostTitle { get; set; }
        [DataType(DataType.MultilineText)]
        public string PostDescription { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Author? Author { get; set; }
        public int AuthorId { get; set; }

        public Category? Category { get; set; }
        public int CategoryId { get; set; }
    }
}
