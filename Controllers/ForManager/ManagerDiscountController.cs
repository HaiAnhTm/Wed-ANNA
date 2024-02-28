using DotNet_E_Commerce_Glasses_Web.Models;
using DotNet_E_Commerce_Glasses_Web.Utils;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var discounts = db.Discounts.Include(d => d.StatusDiscount);
            return View(await discounts.ToListAsync());
        }

        [HttpGet]
        public ActionResult CreateDiscount()
        {
            ViewBag.IdStatus = new SelectList(db.StatusDiscounts, "IdStatus", "Status");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateDiscount([Bind(Include = "IdDiscount,IdStatus,TitleDiscount,DateOfStart,DateOfEnd,Quantity,Percent,CodeDiscount,ImageFile,Image")] Discount discount)
        {
            if (ModelState.IsValid)
            {
                if (discount.ImageFile != null)
                    discount.Image = MoveImageToProject(discount.ImageFile);
                if (discount.DateOfStart > discount.DateOfEnd)
                    discount.DateOfEnd = discount.DateOfStart;
                if (discount.DateOfEnd < DateTime.Now)
                    discount.StatusDiscount = db.StatusDiscounts.FirstOrDefault(item => item.Status.Equals("Hết hạn"));
                db.Discounts.Add(discount);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdStatus = new SelectList(db.StatusDiscounts, "IdStatus", "Status", discount.IdStatus);
            return View(discount);
        }

        [HttpGet]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateDiscount([Bind(Include = "IdDiscount,IdStatus,TitleDiscount,DateOfStart,DateOfEnd,Quantity,Percent,ImageFile,CodeDiscount,Image")] Discount discount)
        {
            if (ModelState.IsValid)
            {
                if (discount.ImageFile != null)
                    discount.Image = MoveImageToProject(discount.ImageFile);
                if (discount.DateOfStart > discount.DateOfEnd)
                    discount.DateOfEnd = discount.DateOfStart;
                if (discount.DateOfEnd < DateTime.Now)
                    discount.StatusDiscount = db.StatusDiscounts.FirstOrDefault(item => item.Status.Equals("Hết hạn"));
                db.Discounts.AddOrUpdate(discount);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdStatus = new SelectList(db.StatusDiscounts, "IdStatus", "Status", discount.IdStatus);
            return View(discount);
        }

        public string MoveImageToProject(HttpPostedFileBase file)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    string extension = Path.GetExtension(file.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                    string folderPath = @"~\Assets\images\images_Discount\";

                    string fullPath = Path.Combine(Server.MapPath(folderPath), fileName);

                    file.SaveAs(fullPath);

                    return Path.Combine(folderPath, fileName).Replace("~", "");
                }
                else
                {
                    return null; // Handle the case where no file is provided
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, log or rethrow as needed
                // Example: Log.Error("Error saving file", ex);
                return null;
            }
        }

        [HttpPost]
        public JsonResult GetCodeDiscount()
        {
            do
            {
                string code = RenderCode.Code();
                if (!db.Discounts.ToList().Exists(item => item.CodeDiscount.Equals(code)))
                    return Json(new
                    {
                        status = true,
                        message = "",
                        code = code
                    });
            } while (true);
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
