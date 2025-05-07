using System.Text.Json;
using TRSAP11.Data;
using TRSAP11.Models;

namespace TRSAP11.Data
{
    public static class RestaurantRepository
    {
        public static void Insert(Restaurant restaurant)
        {
            RestaurantData.RestaurantsList.Add(restaurant);
        }

        public static void Delete(int id)
        {
            // Hier klassisch (noch ohne LINQ) lösen
            
            //RestaurantData.RestaurantsList.Remove(RestaurantData.RestaurantsList
            //    .Where(r => r.RestaurantId == id).FirstOrDefault());


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
