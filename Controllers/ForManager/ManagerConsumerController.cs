using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DotNet_E_Commerce_Glasses_Web.Models;
using System.IO;
using DotNet_E_Commerce_Glasses_Web.Sessions;
using System.Data.Entity.Migrations;
using DotNet_E_Commerce_Glasses_Web.Utils;

namespace DotNet_E_Commerce_Glasses_Web.Controllers.ForManager
{
    public class ManagerConsumerController : Controller
    {
        private GlassesEntities db = new GlassesEntities();
    
        public async Task<ActionResult> Index()
        {
            var consumers = db.Consumers.Include(c => c.Account);
            return View(await consumers.ToListAsync());
        }

        public ActionResult UpdateConsumer()
        {
            if (int.TryParse(ConsumerSession.getConsumerSession(), out int id))
            {
                Consumer consumer = db.Consumers.Find(id);
                if (consumer == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Consumer = consumer;
                return View(consumer);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        [HttpPost]
        public async Task<ActionResult> UpdateConsumer([Bind(Include = "IdConsumer,IdAccount,Username,Address,DateOfBirth,NumberPhone,Image,ImageFile")] Consumer consumer)
        {
            if (ModelState.IsValid)
            {
                if (consumer.ImageFile != null)
                {
                    var fileSave = MoveImageToProject(consumer.ImageFile);
                    if (fileSave  != null)
                        consumer.Image = fileSave;
                }
                db.Consumers.AddOrUpdate(consumer);
                await db.SaveChangesAsync();
                return RedirectToAction("UpdateConsumer");
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


        [HttpPost]
        public async Task<JsonResult> UpdatePassword(int accountId, string oldPassword, string newPassword)
        {
            var account = db.Accounts.FirstOrDefault(item => item.IdAccount.Equals(accountId));
            if (account != null)
            {
                if (account.Password.Equals(PasswordSercurity.GetMD5(oldPassword))) 
                {
                    account.Password = PasswordSercurity.GetMD5(newPassword);
                    db.Accounts.AddOrUpdate(account);
                    await db.SaveChangesAsync();
                    return Json(new { 
                        status = true, 
                        message = "Cập nhật mật khẩu thành công" 
                    });
                }
                else
                    return Json(new { status = false, message = "Mật khẩu cũ không chính xác!" });
            }
            else
                return Json(new { status = false, message = "Lỗi cập nhật!" });
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
