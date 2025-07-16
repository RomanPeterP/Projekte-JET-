using System.Text.Json;
using TRSAP11.Models;

namespace TRSAP11.Data
{
    public static class RestaurantRepository
    {
        public static void Insert(Restaurant restaurant)
        {
            RestaurantData.RestaurantsList.Add(restaurant);
            File.WriteAllText(RestaurantData.Filename, JsonSerializer.Serialize(RestaurantData.RestaurantsList));
        }

        public static void Delete(int id)
        {
            var restaurant = RestaurantData.RestaurantsList.Where(r => r.RestaurantId == id).FirstOrDefault();
            if(restaurant == null)
                return;
            RestaurantData.RestaurantsList.Remove(restaurant);
            File.WriteAllText(RestaurantData.Filename, JsonSerializer.Serialize(RestaurantData.RestaurantsList));
        }

        public static void Update(Restaurant restaurantInput)
        {
            var restaurant = RestaurantData.RestaurantsList
                .Where(r => r.RestaurantId == restaurantInput.RestaurantId).FirstOrDefault();
            if (restaurant == null)
                return;

            restaurant = restaurantInput;
            File.WriteAllText(RestaurantData.Filename, JsonSerializer.Serialize(RestaurantData.RestaurantsList));
        }

        public static bool Any
        {
            get { return RestaurantData.RestaurantsList.Any(); }
        }

        public static List<Restaurant> Select
        {
            get { return RestaurantData.RestaurantsList; }
        }
    }
}
