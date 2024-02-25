using System.Web;

namespace DotNet_E_Commerce_Glasses_Web.Sessions
{
    public class ConsumerSession
    {
        private static string key = "ConsumerSession";

        public static void setConsumerSession(int value)
        {
            HttpContext.Current.Session[key] = value.ToString();
        }

        public static string getConsumerSession()
        {
            return HttpContext.Current.Session[key] as string ?? string.Empty;
        }

    }
}