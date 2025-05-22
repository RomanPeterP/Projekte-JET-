using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TableReservationSystem.Models;
using TableReservationSystem.Models.Interfaces;
using TableReservationSystem.Viewmodels;

namespace TableReservationSystemWeb.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly IMiscLogic _misclogic;
        private readonly IRestaurantLogic _restaurantlogic;
        private readonly IRestaurantMapper _mapper;

        public RestaurantController(IMiscLogic miscLogic, IRestaurantLogic restaurantlogic, IRestaurantMapper mapper)
        {
            _misclogic = miscLogic;
            _restaurantlogic = restaurantlogic;
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
            return View(GetRestaurantFormViewModel(null));
        }


        [HttpGet]
        public IActionResult List()
        {
            var listResponse = _restaurantlogic.Data();
            var viewmodel = _mapper.Map(listResponse.Data, listResponse.Message); 
            return View(viewmodel);
        }


        [HttpPost]
        public IActionResult Register(RestaurantFormViewModel viewmodel)
        {
            viewmodel = GetRestaurantFormViewModel(viewmodel);  
            if (ModelState.IsValid)
            {
                var logicResponse = _restaurantlogic.Register(_mapper.Map(viewmodel));
                if(logicResponse.StatusCode == Enums.StatusCode.Success)
                    return RedirectToAction("List");
                viewmodel.Message = logicResponse.Message;
            }
            return View(viewmodel);
        }

        private RestaurantFormViewModel GetRestaurantFormViewModel(RestaurantFormViewModel? viewmodel)
        {
            if(viewmodel == null)
                viewmodel = new RestaurantFormViewModel();
            var countryList = _misclogic.CountriesData().Data
                .Select(k => new SelectListItem
                {
                    Value = k.CountryCode,
                    Text = k.Name
                }).ToList();
            countryList.Insert(0, new SelectListItem() { Value = "", Text = "" });
            viewmodel.CountryList = countryList;
            return viewmodel;
        }
    }
}
