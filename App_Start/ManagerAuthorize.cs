using System.Web;
using System.Web.Mvc;

namespace DotNet_E_Commerce_Glasses_Web.App_Start
{
    public class ManagerAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var managerSession = HttpContext.Current.Session["ManagerSession"];
            if (managerSession == null)
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(
                        new
                        {
                            controller = "DangNhap",
                            action = "Index"
                        }));
        }
    }
}