using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using TableReservationSystem.Models;
using TableReservationSystem.Models.Interfaces;
using TableReservationSystem.Viewmodels;

namespace TableReservationSystemWeb.Controllers
{
    [Authorize(Roles = "Admin")]
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
        public IActionResult List([FromQuery] RestaurantListViewModel? inViewModel)
        {
            var searchCriteria = _mapper.Map(inViewModel?.RestaurantSearchCriteriaViewModel);
            var listResponse = _restaurantlogic.Data(searchCriteria);
            var outViewmodel = _mapper.Map(listResponse.Data, listResponse.Message, inViewModel);
            return View(outViewmodel);
        }


        [HttpPost]
        public IActionResult Register(RestaurantFormViewModel viewmodel)
        {
            viewmodel = GetRestaurantFormViewModel(viewmodel);
            if (ModelState.IsValid)
            {
                var logicResponse = _restaurantlogic.Register(_mapper.Map(viewmodel));
                if (logicResponse.StatusCode == Enums.StatusCode.Success)
                    return RedirectToAction("List");
                viewmodel.Message = logicResponse.Message;
            }
            return View(viewmodel);
        }

        private RestaurantFormViewModel GetRestaurantFormViewModel(RestaurantFormViewModel? viewmodel)
        {
            if (viewmodel == null)
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


        public IActionResult Details([FromRoute] int id)
        {
            //var geholtesRestaurant = _restaurantlogic.GetRestaurant(id);

            //if (geholtesRestaurant == null) return NotFound();

            //var outViewmodel = _mapper.Map(geholtesRestaurant.Data, geholtesRestaurant.Message);
            var vm = new RestaurantViewModel()
            {
                AddressSummary = "Hauptstraße 11, AT-1140 Wien",
                Email = "aaa@bbb.cc",
                Name = "Restaurant Musil",
                PhoneNumber = "1234567890",
                RestaurantId = id
            };
            return View(vm);
        }
    }
}
