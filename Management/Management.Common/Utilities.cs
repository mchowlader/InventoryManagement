namespace Management.Common
{
    public static class Utilities
    {
        public static string GeerateRandomCodeStringByBiteSize(int length)
        {
            Random random= new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GetRequestResponseTime()
        {
            return DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
        public static DateTime GetDate() 
        {
            return DateTime.Now; 
        }
    }
}