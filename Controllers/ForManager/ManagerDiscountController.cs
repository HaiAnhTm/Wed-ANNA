using DotNet_E_Commerce_Glasses_Web.App_Start;
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
using System.Web.UI.WebControls;

namespace DotNet_E_Commerce_Glasses_Web.Controllers.ForManager
{
    [ManagerAuthorize]
    public class ManagerDiscountController : Controller
    {
        private readonly GlassesEntities db = new GlassesEntities();

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
        public async Task<ActionResult> Index(string sort)
        {
            if (!int.TryParse(sort, out int indexSort))
            {
                indexSort = -1;
            }
            var discounts = await db.Discounts.Include(d => d.StatusDiscount).ToListAsync();
            switch (indexSort)
            {
                case 1:
                    discounts.Sort((first, second) => first.TitleDiscount.CompareTo(second.TitleDiscount));
                    break;
                case 2:
                    discounts.Sort((first, second) => first.Quantity.Value.CompareTo(second.Quantity));
                    break;
                case 3:
                    discounts.Sort((first, second) => first.PercentValue.Value.CompareTo(second.PercentValue));
                    break;
                default:
                    break;
            }

            return View(discounts);
        }

        [HttpGet]
        public ActionResult CreateDiscount()
        {
            var statusDiscounts = db.StatusDiscounts.ToList();
            var itemToRemove = statusDiscounts.FirstOrDefault(s => s.Status.Equals("Hết hạn"));
            if (itemToRemove != null)
                statusDiscounts.Remove(itemToRemove);

            ViewBag.IdStatus = new SelectList(statusDiscounts, "IdStatus", "Status");

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

            var statusDiscounts = db.StatusDiscounts.ToList();
            var itemToRemove = statusDiscounts.FirstOrDefault(s => s.Status.Equals("Hết hạn"));
            if (itemToRemove != null)
                statusDiscounts.Remove(itemToRemove);

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

            var statusDiscounts = db.StatusDiscounts.ToList();
            var itemToRemove = statusDiscounts.FirstOrDefault(s => s.Status.Equals("Hết hạn"));
            if (itemToRemove != null)
                statusDiscounts.Remove(itemToRemove);

            ViewBag.IdStatus = new SelectList(statusDiscounts, "IdStatus", "Status");

            return View(discount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateDiscount([Bind(Include = "IdDiscount,IdStatus,TitleDiscount,DateOfStart,DateOfEnd,Quantity,Percent,ImageFile,CodeDiscount,Image")] Discount discount)
        {
            if (ModelState.IsValid)
            {
                if (discount.ImageFile != null)
                {
                    var pathSave = MoveImageToProject(discount.ImageFile);
                        if(pathSave != null)
                            discount.Image = pathSave;
                }
                if (discount.DateOfStart > discount.DateOfEnd)
                    discount.DateOfEnd = discount.DateOfStart;
                if (discount.DateOfEnd < DateTime.Now)
                    discount.StatusDiscount = db.StatusDiscounts.FirstOrDefault(item => item.Status.Equals("Hết hạn"));
                db.Discounts.AddOrUpdate(discount);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            var statusDiscounts = db.StatusDiscounts.ToList();
            var itemToRemove = statusDiscounts.FirstOrDefault(s => s.Status.Equals("Hết hạn"));
            if (itemToRemove != null)
                statusDiscounts.Remove(itemToRemove);

            ViewBag.IdStatus = new SelectList(statusDiscounts, "IdStatus", "Status", discount.IdStatus);

            return View(discount);
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

                    string folderPath = @"~\Assets\images\images_Discount\";

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
        public JsonResult GetCodeDiscount()
        {
            string code;
            do
            {
                code = RenderCode.Code();
            } while (db.Discounts.Any(item => item.CodeDiscount.Equals(code)));
            return Json(new
            {
                status = true,
                message = "",
                code = code
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
