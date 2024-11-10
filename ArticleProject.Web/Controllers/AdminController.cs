using Microsoft.AspNetCore.Mvc;

namespace ArticleProject.Web.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
