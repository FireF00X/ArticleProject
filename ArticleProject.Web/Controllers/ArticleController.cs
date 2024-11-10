using ArticleProject.Core.Entities;
using ArticleProject.Data.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace ArticleProject.Web.Controllers
{
    public class ArticleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
