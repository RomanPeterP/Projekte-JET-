using System.Text;
using TableReservationSystem.Models;
using TableReservationSystem.Models.Interfaces;

namespace TableReservationSystem.Logic
{
    public class RestaurantLogic : IRestaurantLogic
    {
        private readonly IResponse<IRestaurant> _response;
        private readonly IRestaurantRepository _repository;

        public RestaurantLogic(IRestaurantRepository repository, IResponse<IRestaurant> response)
        {
            _response = response;
            _repository = repository;
        }

        public IResponse<IRestaurant> Register(Restaurant restaurant)
        {
            try
            {
                // Grundprüfung
                if (!IsBasicallyValid(restaurant, out StringBuilder stringBuilder))
                {
                    _response.Message += stringBuilder.ToString();
                    return _response;
                }

                // Prüfung auf Duplikatte
                if (IsExists(restaurant))
                {
                    _response.Message = "Es existiert bereits ein Restaurant unter dieser Adresse. Anlage fehlgeschlagen.";
                    return _response;
                }
                _repository.Insert(restaurant);
                _response.StatusCode = Enums.StatusCode.Success;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
            }
            return _response;
        }

        public IResponse<IRestaurant> Data()
        {
            try
            {
                _response.Data = _repository.Select;
                _response.StatusCode = Enums.StatusCode.Success;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
            }
            return _response;
        }


        public IResponse<IRestaurant> Delete(Restaurant restaurant)
        {
            try
            {
                _repository.Delete(restaurant.RestaurantId);
                _response.StatusCode = Enums.StatusCode.Success;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
            }
            return _response;
        }

        private bool IsExists(IRestaurant restaurant)
        {
            if (_repository.Any)
            {
                var restaurantString = (restaurant.Name + restaurant.PostalCode + restaurant.City
                    + restaurant.StreetHouseNr + restaurant.CountryCode)
                    .Trim().ToUpper();

                foreach (var item in _repository.Select)
                {
                    var restaurantStringCurrent = (item.Name + item.PostalCode + item.City + item.StreetHouseNr + item.CountryCode)
                        .Trim().ToUpper();
                    if (restaurantString == restaurantStringCurrent)
                        return true;
                }
            }
            return false;
        }

        private bool IsBasicallyValid(IRestaurant restaurant, out StringBuilder stringBuilder)
        {
            stringBuilder = new StringBuilder();

            if (restaurant == null)
            {
                stringBuilder.AppendLine($"{restaurant}-Objekt ist nicht gesetzt");
                return false;
            }

            if (string.IsNullOrWhiteSpace(restaurant.Name))
                stringBuilder.AppendLine($"{nameof(restaurant.Name)} fehlt.");

            if (string.IsNullOrWhiteSpace(restaurant.PostalCode))
                stringBuilder.AppendLine($"{nameof(restaurant.PostalCode)} fehlt.");

            if (string.IsNullOrWhiteSpace(restaurant.City))
                stringBuilder.AppendLine($"{nameof(restaurant.City)} fehlt.");

            if (string.IsNullOrWhiteSpace(restaurant.CountryCode))
                stringBuilder.AppendLine($"{nameof(restaurant.CountryCode)} fehlt.");

            if (string.IsNullOrWhiteSpace(restaurant.StreetHouseNr))
                stringBuilder.AppendLine($"{nameof(restaurant.StreetHouseNr)} fehlt.");

            if (restaurant.ContactInfo == null)
            {
                stringBuilder.AppendLine($"{nameof(restaurant.ContactInfo)} fehlt.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(restaurant.ContactInfo.Email))
                stringBuilder.AppendLine($"{nameof(restaurant.ContactInfo.Email)} fehlt.");

            if (string.IsNullOrWhiteSpace(restaurant.ContactInfo.PhoneNumber))
                stringBuilder.AppendLine($"{nameof(restaurant.ContactInfo.PhoneNumber)} fehlt.");


            return (stringBuilder.Length == 0);
        }
    }
}
