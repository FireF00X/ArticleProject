using ArticleProject.Core.Entities;
using ArticleProject.Data.IRepository;
using ArticleProject.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArticleProject.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _repo;

        public AdminController(IUnitOfWork repo)
        {
            _repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            var allPosts = await _repo.CreateRepo<AuthorPost, int>().GetAllAsync();

            var allPostsCount = allPosts.Count;
            var postsLastMonthCount = allPosts.Count(p => p.CreatedAt >= DateTime.Now.AddMonths(-1));
            var postsLastYearCount = allPosts.Count(p => p.CreatedAt >= DateTime.Now.AddYears(-1));

            var model = new AdminDashboardViewModel
            {
                AllPosts = allPostsCount,
                PostsLastMonth = postsLastMonthCount,
                PostsLastYear = postsLastYearCount
            };
            return View(model);            
        } 
    }
    
}
