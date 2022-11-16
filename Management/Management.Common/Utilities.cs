namespace Management.Common
{
    public static class Utilities
    {
        public static string GetRequestResponseTime()
        {
            return DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
    }
}