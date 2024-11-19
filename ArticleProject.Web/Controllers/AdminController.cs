using ArticleProject.Core.Entities;
using ArticleProject.Data.IRepository;
using ArticleProject.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ArticleProject.Data.Specifications.PostsSpecifications;
using ArticleProject.Data.Specifications.AuthorSpecifications;

namespace ArticleProject.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _repo;
        private readonly IAuthorizationService _auth;

        public AdminController(IUnitOfWork repo,IAuthorizationService auth)
        {
            _repo = repo;
            _auth = auth;
        }
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).ToString().Split(":")[2].Trim();
            var allPosts = await _repo.CreateRepo<AuthorPost, int>().GetAllAsyncWithSpecification(new PostsForSpecificUser(userId));
            var allPostsCount = allPosts.Count;
            var postsLastMonthCount = allPosts.Count(p => p.CreatedAt >= DateTime.Now.AddMonths(-1));
            var postsLastYearCount = allPosts.Count(p => p.CreatedAt >= DateTime.Now.AddYears(-1));
            var specificUser = await _repo.CreateRepo<Author, int>().GetAllAsyncWithSpecification(new AuthorByUserId(userId));
            var userName = specificUser.FirstOrDefault().FullName;
            var userImage = specificUser.FirstOrDefault().PictureUrl;

            var model = new AdminDashboardViewModel
            {
                AllPosts = allPostsCount,
                PostsLastMonth = postsLastMonthCount,
                PostsLastYear = postsLastYearCount,
                fullName = userName,
                imgUrl = userImage
            };
            return View(model);            
        } 
    }
    
}
