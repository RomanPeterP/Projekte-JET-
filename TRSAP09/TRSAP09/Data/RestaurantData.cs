using System.Text.Json;
using TRSAP09.Models;

namespace TRSAP09.Data
{
    public static class RestaurantData
    {
        public const string Path = "C:\\Users\\roman\\OneDrive\\BBRZ\\Programmieren Unterricht\\xModul 3\\Woche 8-10\\Woche 8\\Vers2\\TRSAP09\\bin\\Debug\\net8.0\\Restaurants.json";
        static RestaurantData()
        {
            if (File.Exists(Path))
            {
                string json = File.ReadAllText(Path);
                RestaurantsList = JsonSerializer.Deserialize<List<Restaurant>>(json);
            }
            else
                RestaurantsList = new List<Restaurant>();
        }
        public static List<Restaurant> RestaurantsList { get; set; }
    }
}
