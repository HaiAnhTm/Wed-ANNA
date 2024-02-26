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
using Microsoft.Ajax.Utilities;
using System.Data.Entity.Migrations;

namespace DotNet_E_Commerce_Glasses_Web.Controllers.ForManager
{
    public class ManagerTypeProductController : Controller
    {
        private GlassesEntities db = new GlassesEntities();

        // GET: ManagerTypeProduct
        public async Task<ActionResult> Index()
        {
            return View(await db.TypeProducts.ToListAsync());
        }


        [HttpPost]
        public async Task<ActionResult> AddTypeProduct([Bind(Include = "IdTypeProduct,TypeProductName")] TypeProduct typeProduct)
        {
            try
            {
                db.TypeProducts.Add(typeProduct);
                await db.SaveChangesAsync();
                return Json(new
                {
                    status = true,
                    message = "Thêm thành công."
                });
            }catch(Exception ex) { }
               
            return Json(new
            {
                status = false,
                message = "Thêm thất bại!"
            });
        }
        [HttpPost]
        public async Task<ActionResult> UpdateTypeProduct([Bind(Include = "IdTypeProduct,TypeProductName")] TypeProduct typeProduct)
        {
            try
            {
                var typeProductUpdate = db.TypeProducts.FirstOrDefault(l => l.IdTypeProduct == typeProduct.IdTypeProduct);
                if (typeProductUpdate != null)
                {
                    db.TypeProducts.AddOrUpdate(typeProductUpdate);
                    await db.SaveChangesAsync();
                    return Json(new
                    {
                        status = true,
                        message = "Cập nhật thành công."
                    });
                }
                else {
                    return Json(new
                    {
                        status = false,
                        message = "Cập nhật thất bại!"
                    });
                }
            }
            catch (Exception ex) { }

            return Json(new
            {
                status = false,
                message = "Cập nhật thất bại!"
            });
        }

        //// Chức năng cập nhật loại sản phẩm
        //public JsonResult CapNhatLoaiSanPham(string MaLoaiSanPham, string TenLoaiSanPham)
        //{
        //    bool result = false;
        //    if (ModelState.IsValid)
        //    {
        //        var loaiSanPham = db.LoaiSanPhams.FirstOrDefault(l => l.MaLoaiSanPham == MaLoaiSanPham);
        //        if (loaiSanPham == null)
        //            return Json(result);
        //        loaiSanPham.TenLoaiSanPham = TenLoaiSanPham;
        //        db.Entry(loaiSanPham).State = EntityState.Modified;
        //        db.SaveChanges();
        //        result = true;
        //    }
        //    return Json(result);
        //}
        //// Chức năng xóa loại sản phẩm
        //[HttpPost]
        //public JsonResult XoaLoaiSanPham(string MaLoaiSanPham)
        //{
        //    var loaiSanPham = db.LoaiSanPhams.FirstOrDefault(l => l.MaLoaiSanPham == MaLoaiSanPham);
        //    if (loaiSanPham != null)
        //    {
        //        try
        //        {
        //            db.LoaiSanPhams.Remove(loaiSanPham);
        //            db.SaveChanges();
        //            return Json(new { status = true, message = "" });
        //        }
        //        catch (Exception)
        //        {
        //            return Json(new { status = false, message = "Vui lòng thay đổi loại sản phẩm của sản phẩm trước khi xóa!" });
        //        }
        //    }
        //    return Json(new { status = false, message = "Xóa thất bại" });
        //}






        // GET: ManagerTypeProduct/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeProduct typeProduct = await db.TypeProducts.FindAsync(id);
            if (typeProduct == null)
            {
                return HttpNotFound();
            }
            return View(typeProduct);
        }

        // GET: ManagerTypeProduct/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManagerTypeProduct/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdTypeProduct,TypeProductName")] TypeProduct typeProduct)
        {
            if (ModelState.IsValid)
            {
                db.TypeProducts.Add(typeProduct);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(typeProduct);
        }

        // GET: ManagerTypeProduct/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeProduct typeProduct = await db.TypeProducts.FindAsync(id);
            if (typeProduct == null)
            {
                return HttpNotFound();
            }
            return View(typeProduct);
        }

        // POST: ManagerTypeProduct/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdTypeProduct,TypeProductName")] TypeProduct typeProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typeProduct).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(typeProduct);
        }

        // GET: ManagerTypeProduct/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeProduct typeProduct = await db.TypeProducts.FindAsync(id);
            if (typeProduct == null)
            {
                return HttpNotFound();
            }
            return View(typeProduct);
        }

        // POST: ManagerTypeProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TypeProduct typeProduct = await db.TypeProducts.FindAsync(id);
            db.TypeProducts.Remove(typeProduct);
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
