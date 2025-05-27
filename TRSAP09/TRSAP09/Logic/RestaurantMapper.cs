using TRSAP09.Models;
using TRSAP09.Models.Interfaces;
using TRSAP09.Viewmodels;

namespace TRSAP09.Logic
{
    public class RestaurantMapper : IRestaurantMapper
    {
        public RestaurantFormViewModel Map(Restaurant? restaurant)
        {
            if(restaurant == null)
                return new RestaurantFormViewModel();
            
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

        public Restaurant Map(RestaurantFormViewModel? viewmodel)
        {
            if (viewmodel == null)
                return new Restaurant();
            return new Restaurant
            {
                RestaurantId = viewmodel.RestaurantId.HasValue ? viewmodel.RestaurantId.Value: 0,
                Name = viewmodel.Name,
                PostalCode = viewmodel.PostalCode,
                City = viewmodel.City,
                StreetHouseNr = viewmodel.StreetHouseNr,
                Activ = viewmodel.Activ,
                Country = viewmodel.Country,
                ContactInfo = new ContactInfo
                {
                    PhoneNumber = viewmodel.PhoneNumber,
                    Email = viewmodel.Email,
                }
            };
        }

        public RestaurantListsListViewModel ToListListViewModel(Restaurant? restaurant)
        {
            if (restaurant == null)
                return new RestaurantListsListViewModel();

            return new RestaurantListsListViewModel
            {
                RestaurantId = restaurant.RestaurantId,
                Name = restaurant.Name,
                AddressSummary = $"{restaurant.StreetHouseNr}, {restaurant.PostalCode} {restaurant.City}, {restaurant.Country}",
                Email = restaurant.ContactInfo?.Email,
                PhoneNumber = restaurant.ContactInfo?.PhoneNumber,
                Activ = restaurant.Activ
            };
        }

        public RestaurantListViewModel ToListViewModels(IEnumerable<Restaurant>? restaurants)
        {
            var viewmodel = new RestaurantListViewModel();
            if (restaurants == null)
                return viewmodel;

            var list = new List<RestaurantListsListViewModel>();
            foreach (var item in restaurants)
            {
                list.Add(ToListListViewModel(item));
            }
            viewmodel.RestaurantsList = list;
            return viewmodel;
        }
    }
}
