using DotNet_E_Commerce_Glasses_Web.Sessions;
using DotNet_E_Commerce_Glasses_Web.Models;
using System.Linq;
using System.Web.Mvc;
using System.IO;
using System.Web;
using System;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using DotNet_E_Commerce_Glasses_Web.App_Start;

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
            if (ModelState.IsValid)
            {
                if (updateConsumer.ImageFile != null)
                {
                    var fileSave = MoveImageToProject(updateConsumer.ImageFile);
                    if (fileSave != null)
                        updateConsumer.Image = fileSave;
                }
                db.Consumers.AddOrUpdate(updateConsumer);
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

        //[HttpGet]
        //public ActionResult UserInfor()
        //{
        //    var id = KhachHangSession.getKhachHangSession();
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Models.KhachHang khachHang = db.KhachHangs.Find(id);
        //    if (khachHang == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.NguoiDung = khachHang;
        //    return View(khachHang);
        //}
        //[HttpPost]
        //public ActionResult CapNhatKhachHang([Bind(Include = "MaKhachHang,MaTaiKhoan,TenKhachHang,DiaChi,NamSinh,SoDienThoai,HinhAnh,ImageFile")] Models.KhachHang khachHang)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (khachHang.ImageFile != null)
        //            khachHang.HinhAnh = MoveImageToProject(khachHang.ImageFile);
        //        db.KhachHangs.AddOrUpdate(khachHang);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.NguoiDung = khachHang;
        //    return View(khachHang);
        //}
        //public string MoveImageToProject(HttpPostedFileBase directory)
        //{
        //    string fileName = Path.GetFileNameWithoutExtension(directory.FileName);
        //    string extension = Path.GetExtension(directory.FileName);
        //    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
        //    string path = "/Asset/Images/" + fileName;

        //    fileName = Path.Combine(Server.MapPath("~/Asset/Images/"), fileName);
        //    directory.SaveAs(fileName);
        //    return path;
        //}
        //// Chức năng cập nhật mật khẩu cho khách hàng
        //[KhachHangAuthorize]
        //[HttpPost]
        //public JsonResult   ChangePasword(string matKhauCu, string maKhauMoi, string maTaiKhoan)
        //{
        //    var taiKhoan = db.DangNhaps.FirstOrDefault(item => item.MaTaiKhoan.Equals(maTaiKhoan));
        //    if (taiKhoan != null)
        //    {
        //        if (taiKhoan.MatKhau.Equals(matKhauCu))
        //        {
        //            taiKhoan.MatKhau = maKhauMoi;
        //            db.DangNhaps.AddOrUpdate(taiKhoan);
        //            db.SaveChanges();
        //            return Json(new { status = true, message = "Cập nhật mật khẩu thành công" });
        //        }
        //        else
        //            return Json(new { status = false, message = "Mật khẩu cũ không chính xác!" });
        //    }
        //    else
        //        return Json(new { status = false, message = "Lỗi cập nhật!" });
        //}
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