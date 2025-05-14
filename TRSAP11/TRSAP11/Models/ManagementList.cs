namespace TRSAP11.Models
{
    public class ManagementList<T>
    {
        private List<T> items = new List<T>();

        public void Add(T item)
        {
            items.Add(item);
        }

        public void Remove(T item)
        {
            items.Remove(item);
        }

        public void Display()
        {
            Console.WriteLine($"Liste von {typeof(T).Name}");
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }
    }
}
