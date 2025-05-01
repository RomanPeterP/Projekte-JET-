namespace TRSAP11.Logic
{
    public class Tools
    {
        private static int counter = 0;
        public static int GetNextUniqueInteger
        {
            get
            {
                return Interlocked.Increment(ref counter);
            }
        }
    }
}
