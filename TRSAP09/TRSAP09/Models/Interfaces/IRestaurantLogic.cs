namespace TRSAP09.Models.Interfaces
{
    public interface IRestaurantLogic
    {
        public Response Register(Restaurant restaurnat);
        public Response Data();

        public Response Delete(int id);
    }
}
