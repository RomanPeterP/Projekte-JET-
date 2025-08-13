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
                try
                {
                    RestaurantsList = JsonSerializer.Deserialize<List<Restaurant>>(jsonString);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Fehler aufgetretten in Data " + ex.Message);
                    throw;
                }
                
            }
        }
        public static List<Restaurant> RestaurantsList { get; set; }
    }
}
