using System.Reflection;

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
        public static string GetEnumDisplayName(this Enum value)
        {
            FieldInfo? fi = value.GetType().GetField(value.ToString());
            if (fi != null)
            {
                DisplayAttribute[] attributes = (DisplayAttribute[])fi.GetCustomAttributes(typeof(DisplayAttribute), false);

                if (attributes != null && attributes.Length > 0)
                    return attributes[0].Name;
                else
                    return value.ToString();
            }
            else
            {
                return value.ToString();
            }
;
        }
    }
}
