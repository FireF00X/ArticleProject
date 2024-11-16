﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleProject.Core.Entities
{
    public class Category : BaseEntity<int>
    {
        public string Name { get; set; }
        public List<AuthorPost> AuthorPosts { get; set; }

    }
}
