using TRSAP09.Models;
using TRSAP09.Models.Interfaces;
using TRSAP09.Viewmodels;

namespace TRSAP09.Logic
{
    public class RestaurantMapper : IRestaurantMapper
    {
        public RestaurantFormViewModel Map(Restaurant restaurant)
        {
            return new RestaurantFormViewModel
            {
                RestaurantId = restaurant.RestaurantId,
                Name = restaurant.Name,
                PostalCode = restaurant.PostalCode,
                City = restaurant.City,
                StreetHouseNr = restaurant.StreetHouseNr,
                Activ = restaurant.Activ,
                Country = restaurant.Country,
                PhoneNumber = restaurant.ContactInfo?.PhoneNumber,
                Email = restaurant.ContactInfo?.Email
            };
        }

        public Restaurant Map(RestaurantFormViewModel vm)
        {
            return new Restaurant
            {
                RestaurantId = vm.RestaurantId,
                Name = vm.Name,
                PostalCode = vm.PostalCode,
                City = vm.City,
                StreetHouseNr = vm.StreetHouseNr,
                Activ = vm.Activ,
                Country = vm.Country,
                ContactInfo = new ContactInfo
                {
                    PhoneNumber = vm.PhoneNumber,
                    Email = vm.Email,
                }
            };
        }

        public RestaurantListViewModel ToListViewModel(Restaurant restaurant)
        {
            return new RestaurantListViewModel
            {
                RestaurantId = restaurant.RestaurantId,
                Name = restaurant.Name,
                AddressSummary = $"{restaurant.StreetHouseNr}, {restaurant.PostalCode} {restaurant.City}, {restaurant.Country}",
                Email = restaurant.ContactInfo?.Email,
                PhoneNumber = restaurant.ContactInfo?.PhoneNumber,
                Activ = restaurant.Activ
            };
        }

        public List<RestaurantListViewModel> ToListViewModels(IEnumerable<Restaurant> restaurants)
        {
            return restaurants.Select(ToListViewModel).ToList();
        }
    }
}
