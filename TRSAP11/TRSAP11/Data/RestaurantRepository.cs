using System.Text.Json;
using TRSAP11.Models;

namespace TRSAP11.Data
{
    public static class RestaurantRepository
    {
        public static void Insert(Restaurant restaurant)
        {
            try
            {
                RestaurantData.RestaurantsList.Add(restaurant);
                File.WriteAllText(RestaurantData.Filename, JsonSerializer.Serialize(RestaurantData.RestaurantsList));
            }
            catch
            {
                Console.WriteLine("Restaurant adden nicht erfolgreich");
                throw;
            }
        }

        public static void Delete(int id)
        {
            try
            {
                var restaurant = RestaurantData.RestaurantsList.Where(r => r.RestaurantId == id).FirstOrDefault();
                if (restaurant == null)
                    return;
                RestaurantData.RestaurantsList.Remove(restaurant);
                File.WriteAllText(RestaurantData.Filename, JsonSerializer.Serialize(RestaurantData.RestaurantsList));
            }
            catch
            {
                Console.WriteLine("Restaurant löschen nicht erfolgreich");
                throw;
            }
        }

        public static void Update(Restaurant restaurantInput)
        {
            try
            {
                var restaurant = RestaurantData.RestaurantsList
                    .Where(r => r.RestaurantId == restaurantInput.RestaurantId).FirstOrDefault();
                if (restaurant == null)
                    return;

                restaurant = restaurantInput;
                File.WriteAllText(RestaurantData.Filename, JsonSerializer.Serialize(RestaurantData.RestaurantsList));
            }
            catch
            {
                Console.WriteLine("Restaurant update nicht möglich");
                throw;
            }
        }

        public static bool Any
        {
            get
            {
                try
                {
                    return RestaurantData.RestaurantsList.Any();
                }
                catch
                {
                    Console.WriteLine("get restaurant list nicht erfolgreich");
                    throw;
                }
            }
        }

        public static List<Restaurant> Select
        {
            get
            {
                try
                {
                    return RestaurantData.RestaurantsList;
                }
                catch
                {
                    Console.WriteLine("select restaurant list nicht erfolgreich");
                    throw;
                }
            }
        }
    }
}
