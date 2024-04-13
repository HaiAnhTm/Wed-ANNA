using DotNet_E_Commerce_Glasses_Web.App_Start;
using DotNet_E_Commerce_Glasses_Web.Sessions;
using DotNet_E_Commerce_Glasses_Web.Models;
using System.Linq;
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
            ViewBag.Consumer = consumer;    
            return View(consumer);
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