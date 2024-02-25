using System.Web;
using System.Web.Mvc;

namespace DotNet_E_Commerce_Glasses_Web.App_Start
{
    public class ConsumerAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            var customerSession = HttpContext.Current.Session["ConsumerSession"];
            if (customerSession != null)
                return;
            else
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(
                        new
                        {
                            controller = "LogIn",
                            action = "Index"
                        }));
        }
    }
}