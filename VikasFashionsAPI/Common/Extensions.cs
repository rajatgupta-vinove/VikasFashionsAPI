namespace VikasFashionsAPI.Common
{
    public static class Extensions
    {
        public static DateTime CurrentDateTime(this DateTime dateTime)
        {
            return dateTime.ToUniversalTime();
        }
        public static DateTime ConvertToTimeZone(this DateTime dateTime, string toTimeZone)
        {
            return dateTime.ToUniversalTime();
        }
    }
}
