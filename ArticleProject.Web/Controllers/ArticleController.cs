using ArticleProject.Core.Entities;
using ArticleProject.Data.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace ArticleProject.Web.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IUnitOfWork _repo;

        public ArticleController(IUnitOfWork repo)
        {
            _repo = repo;
        }
        public IActionResult Index(int id)
        {
            var article =  _repo.CreateRepo<AuthorPost,int>().GetById(id);
            var mappedPost =  new AuthorPostViewModel()
            {
                Id = article.Id,
                UserId = article.UserId,
                UserName = article.UserName,
                FullName = article.FullName,
                AuthorId = article.AuthorId,
                CategoryId = article.CategoryId,
                CreatedAt = article.CreatedAt,
                PostCategory = article.PostCategory,
                PostDescription = article.PostDescription,
                PostImageName = article.PostImageUrl,
                PostTitle = article.PostTitle
            };
            return View(mappedPost);
        }
    }
}
