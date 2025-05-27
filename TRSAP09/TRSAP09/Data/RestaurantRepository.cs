using Microsoft.Data.SqlClient;
using TRSAP09.Models;

namespace TRSAP09.Data
{
    public static class RestaurantRepository
    {
        static string connectionString = "Data Source=DESKTOP-KCGE85K\\SQLEXPRESS;Initial Catalog=TRSAP09;Integrated Security=True;Encrypt=False";

        public static void InsertUpdate(Restaurant restaurant)
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
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string query = @"
                    DECLARE @ContactInfoId INT = (SELECT [ContactInfoId] FROM [dbo].[Restaurant] WHERE [RestaurantId] = @RestaurantId)
                    DELETE [dbo].[Restaurant] WHERE [RestaurantId] = @RestaurantId
                    DELETE [dbo].[ContactInfo]
                    WHERE [ContactInfoId] = @ContactInfoId";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@RestaurantId", id);
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }

        public static List<Restaurant> Select
        {
            get
            {
                var restaurantList = new List<Restaurant>();
                using var sqlConnection = new SqlConnection(connectionString);

                string query = @"SELECT [RestaurantId]
                              ,r.[Name]
                              ,r.[PostalCode]
                              ,r.[City]
                              ,r.[StreetHouseNr]
                              ,r.[Activ]
                              ,r.[Country]
	                          ,ci.[Email]
	                          ,ci.[PhoneNumber]
                              ,ci.[ContactInfoId]
                        FROM [dbo].[Restaurant] r 
                        LEFT JOIN [dbo].[ContactInfo] ci ON r.ContactInfoId = ci.ContactInfoId";
                var sqlCommand = new SqlCommand(query, sqlConnection);

                sqlConnection.Open();
                var sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        var restaurant = new Restaurant()
                        {
                            RestaurantId = sqlDataReader.GetInt32(0),
                            Name = sqlDataReader.GetString(1),
                            PostalCode = sqlDataReader.GetString(2),
                            City = sqlDataReader.GetString(3),
                            StreetHouseNr = sqlDataReader.GetString(4),
                            Activ = sqlDataReader.GetBoolean(5),
                            Country = sqlDataReader.GetString(6)
                        };
                        restaurant.ContactInfo = new ContactInfo
                        {
                            Email = sqlDataReader.GetString(7),
                            PhoneNumber = sqlDataReader.GetString(8),
                            Id = sqlDataReader.GetInt32(9)
                        };
                        restaurantList.Add(restaurant);
                    }
                }
                return restaurantList;
            }
        }

        public static List<Restaurant> SelectDetails(int id)
        {
            var restaurantList = new List<Restaurant>();
            using var sqlConnection = new SqlConnection(connectionString);

            string query = @"SELECT [RestaurantId]
                              ,r.[Name]
                              ,r.[PostalCode]
                              ,r.[City]
                              ,r.[StreetHouseNr]
                              ,r.[Activ]
                              ,r.[Country]
	                          ,ci.[Email]
	                          ,ci.[PhoneNumber]
                              ,ci.[ContactInfoId]
                        FROM [dbo].[Restaurant] r 
                        LEFT JOIN [dbo].[ContactInfo] ci ON r.ContactInfoId = ci.ContactInfoId
                        WHERE RestaurantId = @RestaurantId";
            var sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@RestaurantId", id);
            
            sqlConnection.Open();
            var sqlDataReader = sqlCommand.ExecuteReader();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    var restaurant = new Restaurant()
                    {
                        RestaurantId = sqlDataReader.GetInt32(0),
                        Name = sqlDataReader.GetString(1),
                        PostalCode = sqlDataReader.GetString(2),
                        City = sqlDataReader.GetString(3),
                        StreetHouseNr = sqlDataReader.GetString(4),
                        Activ = sqlDataReader.GetBoolean(5),
                        Country = sqlDataReader.GetString(6)
                    };
                    restaurant.ContactInfo = new ContactInfo
                    {
                        Email = sqlDataReader.GetString(7),
                        PhoneNumber = sqlDataReader.GetString(8),
                        Id = sqlDataReader.GetInt32(9)
                    };
                    restaurantList.Add(restaurant);
                }
            }
            return restaurantList;
        }
    }
}
