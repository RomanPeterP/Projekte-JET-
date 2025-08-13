using TRSAP11.Logic;
using TRSAP11.Models;
namespace TRSAP11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Uebungen.AddRestaurant();
                Uebungen.DeleteRestaurant();
                Uebungen.UpdateRestaurant();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            // Uebungen.Polymorphie();

            //Uebungen.Generics();
        }
    }
}

