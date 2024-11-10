using Microsoft.AspNetCore.Mvc;

namespace ArticleProject.Web.Controllers
{
    public class AllUsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
