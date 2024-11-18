using Microsoft.AspNetCore.Mvc;

namespace Forjob.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
