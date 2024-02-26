using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DotNet_E_Commerce_Glasses_Web.Models;
using System.IO;
using System.Data.Entity.Migrations;

namespace DotNet_E_Commerce_Glasses_Web.Controllers.ForManager
{
    public class ManagerDiscountController : Controller
    {
        private GlassesEntities db = new GlassesEntities();

        public ManagerDiscountController()
        {
            db.Discounts.ToList().ForEach(item =>
            {
                if (item.DateOfEnd < DateTime.Now)
                {
                    item.StatusDiscount = db.StatusDiscounts.FirstOrDefault(item2 => item2.Status.Equals("Hết hạn"));
                    db.Discounts.AddOrUpdate(item);
                    db.SaveChanges();
                }
            });
        }

        public async Task<ActionResult> Index()
        {
            var discounts = db.Discounts.Include(d => d.StatusDiscount);
            return View(await discounts.ToListAsync());
        }



        // GET: ManagerDiscount/Create
        public ActionResult AddDiscount()
        {
            ViewBag.IdStatus = new SelectList(db.StatusDiscounts, "IdStatus", "Status");
            return View();
        }

        // POST: ManagerDiscount/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddDiscount([Bind(Include = "IdDiscount,IdStatus,TitleDiscount,DateOfStart,DateOfEnd,Quantity,Percent,CodeDiscount")] Discount discount)
        {
            if (ModelState.IsValid)
            {
                db.Discounts.Add(discount);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdStatus = new SelectList(db.StatusDiscounts, "IdStatus", "Status", discount.IdStatus);
            return View(discount);
        }

        // GET: ManagerDiscount/Edit/5
        public async Task<ActionResult> UpdateDiscount(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discount discount = await db.Discounts.FindAsync(id);
            if (discount == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdStatus = new SelectList(db.StatusDiscounts, "IdStatus", "Status", discount.IdStatus);
            return View(discount);
        }

        // POST: ManagerDiscount/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateDiscount([Bind(Include = "IdDiscount,IdStatus,TitleDiscount,DateOfStart,DateOfEnd,Quantity,Percent,Image")] Discount discount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(discount).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdStatus = new SelectList(db.StatusDiscounts, "IdStatus", "Status", discount.IdStatus);
            return View(discount);
        }



        //public ActionResult ThemMaKhuyenMai()
        //{
        //    ViewBag.MaTrangThai = new SelectList(db.TrangThais.Where(item => !item.MaTrangThai.Equals("3")).ToList(), "MaTrangThai", "TenTrangThai");
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult ThemMaKhuyenMai([Bind(Include = "TenChuongTrinh,NgayBatDau,NgayKetThuc,SoLuong,TiLe,ImageFile,MaTrangThai")] KhuyenMai khuyenMai)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string maKhuyenMai;
        //        do
        //        {
        //            maKhuyenMai = Others.TaoMa();
        //            if (db.KhuyenMais.Any(item => item.MaKhuyenMai.Equals(maKhuyenMai)))
        //                maKhuyenMai = Others.TaoMa();
        //            else break;
        //        } while (true);
        //        khuyenMai.MaKhuyenMai = maKhuyenMai;

        //        if (khuyenMai.ImageFile != null)
        //            khuyenMai.HinhAnh = MoveImageToProject(khuyenMai.ImageFile);
        //        if (khuyenMai.NgayBatDau > khuyenMai.NgayKetThuc)
        //        {
        //            khuyenMai.NgayKetThuc = khuyenMai.NgayBatDau;
        //        }
        //        if (khuyenMai.NgayKetThuc < DateTime.Now)
        //        {
        //            khuyenMai.TrangThai = db.TrangThais.FirstOrDefault(item => item.TenTrangThai.Equals("Hết hạn"));
        //        }
        //        db.KhuyenMais.Add(khuyenMai);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.MaTrangThai = new SelectList(db.TrangThais, "MaTrangThai", "TenTrangThai", khuyenMai.MaTrangThai);
        //    return View(khuyenMai);
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
        //// Chức năng cập nhật mã khuyến mãi
        //public ActionResult CapNhatMaKhuyenMai(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    KhuyenMai khuyenMai = db.KhuyenMais.Find(id);
        //    if (khuyenMai == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.MaTrangThai = new SelectList(db.TrangThais.Where(item => !item.MaTrangThai.Equals("3")).ToList(), "MaTrangThai", "TenTrangThai");
        //    return View(khuyenMai);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CapNhatMaKhuyenMai([Bind(Include = "MaKhuyenMai,TenChuongTrinh,NgayBatDau,NgayKetThuc,SoLuong,TiLe,MaTrangThai,HinhAnh, ImageFile")] KhuyenMai khuyenMai)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (khuyenMai.ImageFile != null)
        //            khuyenMai.HinhAnh = MoveImageToProject(khuyenMai.ImageFile);
        //        if (khuyenMai.NgayBatDau > khuyenMai.NgayKetThuc)
        //            khuyenMai.NgayKetThuc = khuyenMai.NgayBatDau;
        //        if (khuyenMai.NgayKetThuc < DateTime.Now)
        //            khuyenMai.TrangThai = db.TrangThais.FirstOrDefault(item => item.TenTrangThai.Equals("Hết hạn"));
        //        db.KhuyenMais.AddOrUpdate(khuyenMai);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.MaTrangThai = new SelectList(db.TrangThais, "MaTrangThai", "TenTrangThai", khuyenMai.MaTrangThai);
        //    return View(khuyenMai);
        //}
        //// Chức năng xóa mã khuyến mãi
        //[HttpPost]
        //public JsonResult Delete(string maKhuyenMai)
        //{
        //    KhuyenMai khuyenMai = db.KhuyenMais.Find(maKhuyenMai);
        //    if (khuyenMai == null)
        //        return Json(new { status = false, message = "Mã khuyến mãi không tồn tại!" });
        //    db.KhuyenMais.Remove(khuyenMai);
        //    db.SaveChanges();
        //    return Json(new { status = true, message = "" });
        //}






        // GET: ManagerDiscount/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discount discount = await db.Discounts.FindAsync(id);
            if (discount == null)
            {
                return HttpNotFound();
            }
            return View(discount);
        }




        // GET: ManagerDiscount/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discount discount = await db.Discounts.FindAsync(id);
            if (discount == null)
            {
                return HttpNotFound();
            }
            return View(discount);
        }

        // POST: ManagerDiscount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Discount discount = await db.Discounts.FindAsync(id);
            db.Discounts.Remove(discount);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
