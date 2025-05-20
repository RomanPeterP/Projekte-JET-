using Microsoft.Data.SqlClient;
using System.Text.Json;
using TRSAP09.Models;

namespace TRSAP09.Data
{
    public static class RestaurantRepository
    {
        static string connectionString = "Data Source=DESKTOP-KCGE85K\\SQLEXPRESS;Initial Catalog=TRSAP09;Integrated Security=True;Encrypt=False";

        public static void Insert(Restaurant restaurant)
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string query = "EXEC[dbo].[InsertUpdateRestaurantWithContactInfo] \r\n   @RestaurantId\r\n  ,@Name\r\n  ,@PostalCode\r\n  ,@City\r\n  ,@StreetHouseNr\r\n  ,@Activ\r\n  ,@Country\r\n  ,@Email\r\n  ,@PhoneNumber";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlCommand.Parameters.AddWithValue("@RestaurantId", restaurant.RestaurantId);
                sqlCommand.Parameters.AddWithValue("@Name", restaurant.Name);
                sqlCommand.Parameters.AddWithValue("@PostalCode", restaurant.PostalCode);
                sqlCommand.Parameters.AddWithValue("@City", restaurant.City);
                sqlCommand.Parameters.AddWithValue("@StreetHouseNr", restaurant.StreetHouseNr);
                sqlCommand.Parameters.AddWithValue("@Activ", restaurant.Activ);
                sqlCommand.Parameters.AddWithValue("@Country", restaurant.Country);
                sqlCommand.Parameters.AddWithValue("@Email", restaurant.ContactInfo.Email);
                sqlCommand.Parameters.AddWithValue("@PhoneNumber", restaurant.ContactInfo.PhoneNumber);

                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }


        public static void Delete(int id)
        {

        }

        public static bool Any
        {
            get { return false; }
        }

        public static List<Restaurant> Select
        {
            get { return new List<Restaurant>(); }
        }
    }
}
