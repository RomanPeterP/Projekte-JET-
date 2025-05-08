using TableReservationSystem.Models;
using TableReservationSystem.Models.Interfaces;
using TableReservationSystem.Viewmodels;

namespace TableReservationSystem.Logic
{
    public class RestaurantMapper : IRestaurantMapper
    {
        public RestaurantFormViewModel Map(Restaurant? model, string? message)
        {
            {
                if (model == null)
                    return new RestaurantFormViewModel();

                return new RestaurantFormViewModel
                {
                    RestaurantId = model.RestaurantId,
                    Name = model.Name,
                    PostalCode = model.PostalCode,
                    City = model.City,
                    StreetHouseNr = model.StreetHouseNr,
                    Activ = model.Activ,
                    PhoneNumber = model.ContactInfo?.PhoneNumber,
                    Email = model.ContactInfo?.Email,
                    Country = model.CountryCode,
                    Message = message
                };
            }
        }

        public Restaurant Map(RestaurantFormViewModel? viewmodel)
        {
            if (viewmodel == null)
                return new Restaurant();
            
            return new Restaurant
            {
                RestaurantId = viewmodel.RestaurantId,
                Name = viewmodel.Name,
                PostalCode = viewmodel.PostalCode,
                City = viewmodel.City,
                StreetHouseNr = viewmodel.StreetHouseNr,
                Activ = viewmodel.Activ,
                CountryCode = viewmodel.Country,
                ContactInfo = new ContactInfo
                {
                    PhoneNumber = viewmodel.PhoneNumber ?? string.Empty,
                    Email = viewmodel.Email ?? string.Empty,
                }
            };
        }
    }
}
