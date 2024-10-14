namespace RealEstateDLL.Helpers
{
    public static class IDGenerator
    {
        // Static field to store the current ID, starting from 1
        private static int currentID = 1;

        // Method to get the next unique ID
        public static int GetNextID()
        {
            // Return the current ID, then increment it for the next request
            return currentID++;
        }

        public static string GetUniqueId()
        {
            return Guid.NewGuid().ToString("D");
        }
    }
}
