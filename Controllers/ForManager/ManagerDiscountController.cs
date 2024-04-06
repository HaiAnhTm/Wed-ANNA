using DotNet_E_Commerce_Glasses_Web.Models;
using DotNet_E_Commerce_Glasses_Web.Sessions;
using DotNet_E_Commerce_Glasses_Web.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
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
    //[ManagerAuthorize]
    public class ManagerDiscountController : Controller
    {
        private readonly GlassesEntities db = new GlassesEntities();
        private readonly Manager manager;

        public ManagerDiscountController()
        {
            string session = ManagerSession.getManagerSession();
            if (session != null && int.TryParse(session, out int managerId))
            {
                manager = db.Managers.FirstOrDefault(item => item.IdManager.Equals(managerId));
            }

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

        [HttpPost]
        public async Task<JsonResult> QueryDiscount(string sort, string statusProduct, string search)
        {
            if (manager == null)
                return Json(new
                {
                    status = false,
                    message = "Yêu cầu đăng nhập!",
                    url = "/LoginAccount"
                });
            if (!int.TryParse(sort, out int indexSort))
            {
                indexSort = -1;
            }
            if (!int.TryParse(statusProduct, out int indexStatus))
            {
                indexStatus = -1;
            }

            List<Discount> discounts;

            if (!string.IsNullOrWhiteSpace(search))
            {
                discounts = await db.Discounts
                    .Where(item => item.TitleDiscount.ToLower().Contains(search.ToLower()))
                    .Include(d => d.StatusDiscount)
                    .ToListAsync();
            }else
            {
                discounts = await db.Discounts
                    .Include(d => d.StatusDiscount)
                    .ToListAsync();
            }

            switch (indexStatus)
            {
                case 1:
                    {
                        discounts = discounts
                               .Where(item => item.DateOfStart.Value <= DateTime.Now && item.DateOfEnd.Value >= DateTime.Now)
                               .ToList();
                        break;
                    }
                case 2:
                    {

                        discounts = discounts
                             .Where(item => item.DateOfStart.Value.Subtract(DateTime.Now).TotalDays >= 7)
                             .ToList();
                        break;
                    }
                case 3:
                    {

                        discounts = discounts
                             .Where(item => item.DateOfEnd.Value < DateTime.Now)
                             .ToList();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

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

            var jsonResult = new List<object>();
            foreach (var item in discounts)
            {
                if (item == null)
                {
                    continue; 
                }

                var jsonData = new
                {
                    id_discount = item.IdDiscount,
                    code_discount = item.CodeDiscount,
                    name_discount = item.TitleDiscount,
                    percent = item.PercentValue,
                    image_url = item.Image,
                    quantity = item.Quantity,
                    status = item.StatusDiscount.Status,
                    duration = item.DateOfStart.Value.ToString("dd/MM/yyyy") + " - " + item.DateOfEnd.Value.ToString("dd/MM/yyyy")
                };
                jsonResult.Add(jsonData);
            }

            return Json(new {
                status = true, 
                data = jsonResult
            },
            JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
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
        public async Task<ActionResult> CreateDiscount([Bind(Include = "IdDiscount,IdStatus,TitleDiscount,DateOfStart,DateOfEnd,Quantity,PercentValue,CodeDiscount,ImageFile,Image")] Discount discount)
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
        public async Task<ActionResult> UpdateDiscount([Bind(Include = "IdDiscount,IdStatus,TitleDiscount,DateOfStart,DateOfEnd,Quantity,PercentValue,ImageFile,CodeDiscount,Image")] Discount discount)
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
        [HttpDelete]
        public async Task<JsonResult> DeleteDiscount(string id_discount)
        {
            if (manager == null)
                return Json(new
                {
                    status = false,
                    message = "Yêu cầu đăng nhập!",
                    url = "/LoginAccount"
                });
            if (!int.TryParse(id_discount, out int idDiscount))
            {
                idDiscount = -1;
            }


            var discountDelete = db.Discounts.FirstOrDefault(item => item.IdDiscount.Equals(idDiscount));

            if (discountDelete != null)
            {
                db.Discounts.Remove(discountDelete);
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
