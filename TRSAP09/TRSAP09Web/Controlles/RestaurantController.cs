using Microsoft.AspNetCore.Mvc;
using TRSAP09.Models.Interfaces;
using TRSAP09.Models;

namespace TRSAP09Web.Controlles
{
    public class RestaurantController : Controller
    {
        private readonly IRestaurantLogic _restaurantLogic;


        public RestaurantController(IRestaurantLogic restaurantLogic)
        {
            _restaurantLogic = restaurantLogic;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult List()
        {
            var response = _restaurantLogic.Data();
            if (response == null
                || response.StatusCode == Enums.StatusCode.Error || response.Data == null)
                return NoContent();

            return View(response.Data);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(Restaurant restaurant)
        {
            if (!ModelState.IsValid)
            {
                var response = _restaurantLogic.Register(restaurant);
                if (response != null
                        && response.StatusCode == Enums.StatusCode.Success)
                    return RedirectToAction("List");
            }
            return View();
        }
    }
}
