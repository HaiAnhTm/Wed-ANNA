using DotNet_E_Commerce_Glasses_Web.Models;
using DotNet_E_Commerce_Glasses_Web.Utils;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DotNet_E_Commerce_Glasses_Web.Controllers.ForCommon
{
    public class RegisterAccountController : Controller
    {
        private readonly GlassesEntities db = new GlassesEntities();

        [HttpGet]
        public ActionResult Index() => View();

        [HttpPost]
        public async Task<ActionResult> Index([Bind(Include = "IdAccount,Username,Password")] Account account, [Bind(Include = "Name")] string Name)
        {
            if (ModelState.IsValid)
            {
                account.Password = PasswordSercurity.GetMD5(account.Password);
                db.Accounts.Add(account);
                await db.SaveChangesAsync();

                Consumer registerConsumer = new Consumer()
                {
                    Username = Name,
                    IdAccount = account.IdAccount
                };
                db.Consumers.Add(registerConsumer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "LoginAccount");
            }

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CheckExistUsername([Bind(Include = "Username")] string Username)
        {
            var accountList = await db.Accounts.ToListAsync();
            if (accountList.Exists(item => item.Username.Equals(Username)))
                return Json(new
                {
                    status = true,
                    message = "Tài khoản đã tồn tại!"
                });
            return Json(new
            {
                status = false,
                message = ""
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
