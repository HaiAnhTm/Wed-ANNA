using DotNet_E_Commerce_Glasses_Web.Models;
using DotNet_E_Commerce_Glasses_Web.Sessions;
using DotNet_E_Commerce_Glasses_Web.Utils;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DotNet_E_Commerce_Glasses_Web.Controllers.ForCommon
{
    public class LoginAccountController : Controller
    {
        private GlassesEntities db = new GlassesEntities();

        public ActionResult Index()
        {
            Session.RemoveAll();
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Index([Bind(Include = "Username")] string Username, [Bind(Include = "Password")] string Password)
        {
            var convertPasswordToMD5 = PasswordSercurity.GetMD5(Password);
            var accountLogin = await db
                .Accounts
                .FirstOrDefaultAsync(item =>
                    item.Username.Equals(Username) &&
                    item.Password.Equals(convertPasswordToMD5));
            if (accountLogin != null)
            {
                if (accountLogin.Managers.Count != 0)
                {
                    ManagerSession.setManagerSession(accountLogin.Managers.First().IdManager);
                    return Json(new
                    {
                        status = true,
                        message = "Đăng nhập thành công.",
                        url = "/Manage",
                    });
                }
                if (accountLogin.Consumers.Count != 0)
                {
                    ConsumerSession.setConsumerSession(accountLogin.Consumers.First().IdConsumer);
                    return Json(new
                    {
                        status = true,
                        message = "Đăng nhập thành công.",
                        url = "/Home/",
                    });
                }
            }
            return Json(new
            {
                status = false,
                message = "Tài khoản và mật khẩu không đúng!",
                url = "",
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
