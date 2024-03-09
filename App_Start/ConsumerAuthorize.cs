using DotNet_E_Commerce_Glasses_Web.Sessions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DotNet_E_Commerce_Glasses_Web.App_Start
{
    public class ConsumerAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            var customerSession = ConsumerSession.getConsumerSession();
            if (customerSession == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                     new RouteValueDictionary(
                         new
                         {
                             controller = "LogInAccount",
                             action = "Index"
                         }));
            }
        }
    }
}