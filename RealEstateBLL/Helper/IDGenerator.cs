namespace RealEstateDLL.Helpers
{
    public static class IDGenerator
    {

        public static string GetUniqueId()
        {
            return Guid.NewGuid().ToString("D");
        }
    }
}
