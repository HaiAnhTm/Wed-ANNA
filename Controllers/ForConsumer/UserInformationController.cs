using DotNet_E_Commerce_Glasses_Web.Models;
using DotNet_E_Commerce_Glasses_Web.Sessions;
using DotNet_E_Commerce_Glasses_Web.Utils;
using System;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DotNet_E_Commerce_Glasses_Web.Controllers.ForConsumer
{
    public class UserInformationController : Controller
    {
        private readonly GlassesEntities db = new GlassesEntities();
        private readonly Consumer consumer;

        public UserInformationController()
        {
            string session = ConsumerSession.getConsumerSession();
            if (session != null && int.TryParse(session, out int consumerID))
            {
                consumer = db.Consumers.FirstOrDefault(item => item.IdConsumer.Equals(consumerID));
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (consumer == null)
                return RedirectToAction("Index", "LoginAccount");

            ViewBag.Consumer = consumer;
            return View(consumer);
        }

        [HttpPost]
        public async Task<ActionResult> Index([Bind(Include = "IdConsumer,IdAccount,Username,Address,DateOfBirth,NumberPhone,Image,ImageFile")] Consumer updateConsumer)
        {
            if (consumer == null)
                return RedirectToAction("Index", "LoginAccount");

            if (ModelState.IsValid)
            {
                if (updateConsumer.ImageFile != null)
                {
                    var fileSave = MoveImageToProject(updateConsumer.ImageFile);
                    if (fileSave != null)
                        consumer.Image = fileSave;
                }
                consumer.Username = updateConsumer.Username;
                consumer.Address = updateConsumer.Address;
                consumer.DateOfBirth = updateConsumer.DateOfBirth;
                consumer.NumberPhone = updateConsumer.NumberPhone;

                db.Consumers.AddOrUpdate(consumer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Consumer = consumer;
            return View(consumer);
        }

        private string MoveImageToProject(HttpPostedFileBase file)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    string extension = Path.GetExtension(file.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                    string folderPath = @"~\Assets\images\images_Consumer\";

                    string fullPath = Path.Combine(Server.MapPath(folderPath), fileName);

                    file.SaveAs(fullPath);

                    return Path.Combine(folderPath, fileName).Replace("~", "");
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet]
        public ActionResult UpdatePassword()
        {
            if (consumer == null)
                return RedirectToAction("Index", "LoginAccount");
            return View(consumer);
        }

        [HttpPost]
        public async Task<JsonResult> CheckPassword(string IdAccount, string OldPassword, string NewPassword)
        {
            if (consumer == null)
            {
                return Json(new
                {
                    status = false,
                    message = "Yêu cầu đăng nhập trước khi thay đổi mật khẩu!",
                    url = "/LoginAccount/Index"
                });
            }

            if (consumer.Account.IdAccount.ToString().Equals(IdAccount))
            {
                Account account = consumer.Account;

                if (account.Password.Equals(PasswordSercurity.GetMD5(OldPassword)))
                {
                    account.Password = PasswordSercurity.GetMD5(NewPassword);
                    db.Accounts.AddOrUpdate(account);
                    await db.SaveChangesAsync();

                    return Json(new
                    {
                        status = true,
                        message = "Cập nhật thành công!",
                        url = "/LoginAccount/Index"
                    });
                }
                else
                {
                    return Json(new
                    {
                        status = false,
                        message = "Yêu cầu nhập đúng mật khẩu!",
                        url = ""
                    });
                }
            }

            return Json(new
            {
                status = false,
                message = "Yêu cầu đăng nhập trước khi thay đổi mật khẩu!",
                url = "/LoginAccount/Index"
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