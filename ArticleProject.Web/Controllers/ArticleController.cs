using ArticleProject.Core.Entities;
using ArticleProject.Data.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArticleProject.Web.Controllers
{
    [Authorize]
    public class ArticleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
