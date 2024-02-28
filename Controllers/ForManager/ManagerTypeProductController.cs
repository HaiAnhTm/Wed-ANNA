using DotNet_E_Commerce_Glasses_Web.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DotNet_E_Commerce_Glasses_Web.Controllers.ForManager
{
    public class ManagerTypeProductController : Controller
    {
        private GlassesEntities db = new GlassesEntities();

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(await db.TypeProducts.ToListAsync());
        }

        [HttpPost]
        public async Task<JsonResult> AddTypeProduct([Bind(Include = "IdTypeProduct,TypeProductName")] TypeProduct typeProduct)
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
            }
            catch (Exception) { }

            return Json(new
            {
                status = false,
                message = "Thêm thất bại!"
            });
        }

        [HttpPost]
        public async Task<JsonResult> UpdateTypeProduct([Bind(Include = "IdTypeProduct,TypeProductName")] TypeProduct typeProduct)
        {
            var typeProductUpdate = db.TypeProducts.FirstOrDefault(l => l.IdTypeProduct == typeProduct.IdTypeProduct);
            if (typeProductUpdate != null)
            {
                typeProductUpdate.TypeProductName = typeProduct.TypeProductName;
                db.TypeProducts.AddOrUpdate(typeProductUpdate);
                await db.SaveChangesAsync();
                return Json(new
                {
                    status = true,
                    message = "Cập nhật thành công."
                });
            }
            return Json(new
            {
                status = false,
                message = "Cập nhật thất bại!"
            });
        }

        [HttpPost]
        public async Task<JsonResult> DeleteTypeProduct([Bind(Include = "IdTypeProduct")] string idTypeProduct)
        {
            var idTypeProductKey = int.Parse(idTypeProduct);

            var typeProductDelete = db.TypeProducts.FirstOrDefault(l => l.IdTypeProduct == idTypeProductKey);
            if (typeProductDelete != null)
            {
                db.TypeProducts.Remove(typeProductDelete);
                await db.SaveChangesAsync();
                return Json(new
                {
                    status = true,
                    message = "Xóa thành công."
                });
            }
            return Json(new
            {
                status = false,
                message = "Xóa thất bại!"
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
