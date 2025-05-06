using System.Text.Json;
using TRSAP09.Models;

namespace TRSAP09.Data
{
    public static class RestaurantRepository
    {
        public static void Insert(Restaurant restaurant)
        {
            RestaurantData.RestaurantsList.Add(restaurant);

        }

        private static void CreateJson()
        {
            var json = JsonSerializer.Serialize(RestaurantData.RestaurantsList);
            File.WriteAllText(RestaurantData.Path, json);
        }

        public static void Delete(int id)
        {
            RestaurantData.RestaurantsList.Remove(RestaurantData.RestaurantsList
                .Where(r => r.RestaurantId == id).FirstOrDefault());
            CreateJson();
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
