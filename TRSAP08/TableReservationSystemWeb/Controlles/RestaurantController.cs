using Microsoft.AspNetCore.Mvc;
using TableReservationSystem.Models;
using TableReservationSystem.Viewmodels;

namespace TableReservationSystemWeb.Controlles
{
    public class RestaurantController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RestaurantFormViewModel restaurant)
        {
             // TODO
            return View(restaurant);
        }
    }
}
