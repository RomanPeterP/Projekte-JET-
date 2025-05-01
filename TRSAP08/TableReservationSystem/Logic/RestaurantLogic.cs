using System.Text;
using TableReservationSystem.Models;
using TableReservationSystem.Models.Interfaces;

namespace TableReservationSystem.Logic
{
    public class RestaurantLogic : IRestaurantLogic
    {
        private readonly IResponse _response;
        private readonly IRestaurantRepository _repository;

        public RestaurantLogic(IRestaurantRepository repository, IResponse response)
        {
            _response = response;
            _repository = repository;
        }

        public IResponse Register(Restaurant restaurnat)
        {
            try
            {
                // Grundprüfung
                if (!IsBasicallyValid(restaurnat, out StringBuilder stringBuilder))
                {
                    _response.Message += stringBuilder.ToString();
                    return _response;
                }

                // Prüfung auf Duplikatte
                if (IsExists(restaurnat))
                {
                    _response.Message = "Es exitiert bereits ein Restaurant unter dieser Adresse. Anlage fehlgeschlagen.";
                    return _response;
                }
                _repository.Insert(restaurnat);
                _response.StatusCode = Enums.StatusCode.Success;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
            }
            return _response;
        }

        public IResponse Data()
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

        private bool IsExists(IRestaurant restaurnat)
        {
            if (_repository.Any)
            {
                var restaurantString = (restaurnat.Name + restaurnat.PostalCode + restaurnat.City
                    + restaurnat.StreetHouseNr + restaurnat.CountryCode)
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

        private bool IsBasicallyValid(IRestaurant restaurnat, out StringBuilder stringBuilder)
        {
            stringBuilder = new StringBuilder();

            if (restaurnat == null)
            {
                stringBuilder.AppendLine($"{restaurnat}-Objekt ist nicht gesetzt");
                return false;
            }

            if (string.IsNullOrWhiteSpace(restaurnat.Name))
                stringBuilder.AppendLine($"{nameof(restaurnat.Name)} fehlt.");

            if (string.IsNullOrWhiteSpace(restaurnat.PostalCode))
                stringBuilder.AppendLine($"{nameof(restaurnat.PostalCode)} fehlt.");

            if (string.IsNullOrWhiteSpace(restaurnat.City))
                stringBuilder.AppendLine($"{nameof(restaurnat.City)} fehlt.");

            if (string.IsNullOrWhiteSpace(restaurnat.CountryCode))
                stringBuilder.AppendLine($"{nameof(restaurnat.CountryCode)} fehlt.");

            if (string.IsNullOrWhiteSpace(restaurnat.StreetHouseNr))
                stringBuilder.AppendLine($"{nameof(restaurnat.StreetHouseNr)} fehlt.");

            if (restaurnat.ContactInfo == null)
            {
                stringBuilder.AppendLine($"{nameof(restaurnat.ContactInfo)} fehlt.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(restaurnat.ContactInfo.Email))
                stringBuilder.AppendLine("Email fehlt.");

            if (string.IsNullOrWhiteSpace(restaurnat.ContactInfo.PhoneNumber))
                stringBuilder.AppendLine("PhoneNumber fehlt.");

            return (stringBuilder.Length == 0);
        }

    }
}
