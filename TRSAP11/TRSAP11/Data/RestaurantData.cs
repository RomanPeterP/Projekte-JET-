using System.Text.Json;
using TRSAP11.Models;

namespace TRSAP11.Data
{
    public static class RestaurantData
    {
        public static readonly string Filename = "RestaurantsList.json";
        static RestaurantData()
        {
            RestaurantsList = [];
            if (File.Exists(Filename))
            {
                string jsonString = File.ReadAllText(Filename);
                RestaurantsList = JsonSerializer.Deserialize<List<Restaurant>>(jsonString);
            }
        }
        public static List<Restaurant> RestaurantsList { get; set; }
    }
}
