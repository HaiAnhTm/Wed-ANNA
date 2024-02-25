using System.Web;

namespace DotNet_E_Commerce_Glasses_Web.Sessions
{
    public class ManagerSession
    {
        private static string key = "ManagerSession";

        public static void setManagerSession(int value)
        {
            HttpContext.Current.Session[key] = value.ToString();
        }

        public static string getManagerSession()
        {
            return HttpContext.Current.Session[key] as string ?? string.Empty;
        }

    }
}