using Microsoft.AspNetCore.Mvc;
using TableReservationSystem.Logic;
using TableReservationSystem.Models;
using TableReservationSystem.Models.Interfaces;
using TableReservationSystem.Viewmodels;

namespace TableReservationSystemWeb.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly IRestaurantLogic _logic;
        private readonly IRestaurantMapper _mapper;

        public RestaurantController(IRestaurantLogic logic, IRestaurantMapper mapper)
        {

            _logic = logic;
            _mapper = mapper;
        }

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
        public IActionResult Register(RestaurantFormViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                var logicResponse = _logic.Register(_mapper.Map(viewmodel));
                if(logicResponse.StatusCode == Enums.StatusCode.Success)
                    return RedirectToAction("List");
                viewmodel.Message = logicResponse.Message;
            }
            return View(viewmodel);
        }
    }
}
