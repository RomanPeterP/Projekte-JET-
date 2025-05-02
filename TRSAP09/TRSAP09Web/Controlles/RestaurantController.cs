using Microsoft.AspNetCore.Mvc;
using TRSAP09.Models.Interfaces;
using TRSAP09.Models;
using TRSAP09.Viewmodels;

namespace TRSAP09Web.Controlles
{
    public class RestaurantController : Controller
    {
        private readonly IRestaurantLogic _restaurantLogic;
        private readonly IRestaurantMapper _mapper;

        public RestaurantController(IRestaurantLogic restaurantLogic, IRestaurantMapper mapper)
        {
            _restaurantLogic = restaurantLogic;
            _mapper = mapper;
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
            var viewModel = _mapper.ToListViewModels(response.Data);
            viewModel.Message = response.Message;
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(RestaurantFormViewModel restaurant)
        {
            if (ModelState.IsValid)
            {
                var viewModel = _mapper.Map(restaurant);
                var response = _restaurantLogic.Register(viewModel);
                if (response.StatusCode == Enums.StatusCode.Success)
                    return RedirectToAction("List");
                restaurant.Message = response.Message;

            }
            return View(restaurant);
        }
    }
}
