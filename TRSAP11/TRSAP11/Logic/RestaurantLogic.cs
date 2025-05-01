using System.Text;
using TRSAP11.Models.Interfaces;
using TRSAP11.Models;
using TRSAP11.Data;

namespace TRSAP11.Logic
{
    public class RestaurantLogic : IRestaurantLogic
    {
        public Response Register(Restaurant restaurnat)
        {

            var response = new Response(Enums.StatusCode.Error);

            // Grundprüfung

            if (!IsBasicallyValid(restaurnat, out StringBuilder stringBuilder))
            {
                response.Message += stringBuilder.ToString();
                return response;
            }

            // Prüfung auf Duplikatte
            if (IsExists(restaurnat))
            {
                response.Message = "Es exitiert bereits ein Restaurant unter dieser Adresse. Anlage fehlgeschlagen.";
                return response;
            }

            RestaurantRepository.Insert(restaurnat);
            response.StatusCode = Enums.StatusCode.Success;
            return response;
        }

        public Response Data()
        {
            var response = new Response(Enums.StatusCode.Error);
            try
            {
                response.Data = RestaurantRepository.Select;
                response.StatusCode = Enums.StatusCode.Success;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        private bool IsExists(Restaurant restaurnat)
        {
            if (RestaurantRepository.Any)
            {
                var restaurantString = (restaurnat.Name + restaurnat.PostalCode + restaurnat.City
                    + restaurnat.StreetHouseNr + restaurnat.Country)
                    .Trim().ToUpper();

                foreach (var item in RestaurantRepository.Select)
                {
                    var restaurantStringCurrent = (item.Name + item.PostalCode + item.City + item.StreetHouseNr + item.Country)
                        .Trim().ToUpper();
                    if (restaurantString == restaurantStringCurrent)
                        return true;
                }
            }
            return false;
        }

        private bool IsBasicallyValid(Restaurant restaurnat, out StringBuilder stringBuilder)
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

            if (string.IsNullOrWhiteSpace(restaurnat.Country))
                stringBuilder.AppendLine($"{nameof(restaurnat.Country)} fehlt.");

            if (string.IsNullOrWhiteSpace(restaurnat.StreetHouseNr))
                stringBuilder.AppendLine($"{nameof(restaurnat.StreetHouseNr)} fehlt.");

            if (restaurnat.ContactInfo == null)
            {
                stringBuilder.AppendLine($"{nameof(restaurnat.ContactInfo)} fehlt.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(restaurnat.ContactInfo.Email))
                stringBuilder.AppendLine($"{nameof(restaurnat.ContactInfo.Email)} fehlt.");

            if (string.IsNullOrWhiteSpace(restaurnat.ContactInfo.PhoneNumber))
                stringBuilder.AppendLine($"{nameof(restaurnat.ContactInfo.PhoneNumber)} fehlt.");

            return (stringBuilder.Length == 0);
        }

    }
}
