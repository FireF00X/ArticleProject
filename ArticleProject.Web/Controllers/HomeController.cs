using ArticleProject.Core.Entities;
using ArticleProject.Data.IRepository;
using ArticleProject.Data.Specifications.PostsSpecifications;
using ArticleProject.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using X.PagedList.Extensions;

namespace ArticleProject.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _repo;

        public HomeController(ILogger<HomeController> logger,IUnitOfWork repo)
        {
            _logger = logger;
            _repo = repo;
        }

        public async Task<IActionResult> Index(string? searchingItme, string catName, int pageSize = 6, int? pageIndex = 1)
        {
            var posts = await _repo.CreateRepo<AuthorPost, int>().GetAllAsyncWithSpecification(new PostsForHomeSpecs(catName, searchingItme));
            var mappedPost = posts.Select(p => new AuthorPostViewModel()
            {
                Id = p.Id,
                UserId = p.UserId,
                UserName = p.UserName,
                FullName = p.FullName,
                AuthorId = p.AuthorId,
                CategoryId = p.CategoryId,
                CreatedAt = p.CreatedAt,
                PostCategory = p.PostCategory,
                PostDescription = p.PostDescription,
                PostImageName = p.PostImageUrl,
                PostTitle = p.PostTitle
            });
            ViewBag.currentName = catName;
            return View(mappedPost.ToPagedList(pageIndex ?? 1, pageSize));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
