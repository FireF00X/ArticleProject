using ArticleProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleProject.Data.IRepository
{
    public interface IAuthorRepository
    {
        Task<List<Author>> SearchByItem(string item);
    }
}
