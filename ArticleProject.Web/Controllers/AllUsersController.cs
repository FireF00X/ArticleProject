using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArticleProject.Web.Controllers
{
    [Authorize("Admin")]
    public class AllUsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
