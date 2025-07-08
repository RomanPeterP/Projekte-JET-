using Microsoft.AspNetCore.Mvc;

namespace TRSAP09V2Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
