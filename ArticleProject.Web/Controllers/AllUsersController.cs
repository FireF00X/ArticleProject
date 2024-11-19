using ArticleProject.Core.Entities;
using ArticleProject.Data.IRepository;
using ArticleProject.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace ArticleProject.Web.Controllers
{
    public class AllUsersController : Controller
    {
        private readonly IUnitOfWork _repo;

        public AllUsersController(IUnitOfWork repo)
        {
            _repo = repo;
        }
        public async Task<IActionResult> Index(int? pageIndex, int pageSize = 3)
        {
            var authors = await _repo.CreateRepo<Author,int>().GetAllAsync();
            var mappedAuthors = authors.Select(a => new AuthorViewModel
            {
                Id = a.Id,
                UserId = a.UserId,
                UserName = a.UserName,
                FullName = a.FullName,
                PictureUrl = a.PictureUrl,
                Bio = a.Bio,
                FaceBookLink = a.FaceBookLink,
                InstegramLink = a.InstegramLink,
                XSiteLink = a.XSiteLink
            });
            return View(mappedAuthors.ToPagedList(pageIndex??1,pageSize));
        }
    }
}
