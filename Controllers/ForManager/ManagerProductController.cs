using DotNet_E_Commerce_Glasses_Web.App_Start;
using DotNet_E_Commerce_Glasses_Web.Models;
using DotNet_E_Commerce_Glasses_Web.Sessions;
using DotNet_E_Commerce_Glasses_Web.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DotNet_E_Commerce_Glasses_Web.Controllers.ForManager
{
    [ManagerAuthorize]
    public class ManagerProductController : Controller
    {
        private readonly GlassesEntities db = new GlassesEntities();
        private readonly Manager manager;

        public ManagerProductController()
        {
            string session = ManagerSession.getManagerSession();
            if (session != null && int.TryParse(session, out int managerId))
            {
                manager = db.Managers.FirstOrDefault(item => item.IdManager.Equals(managerId));
            }
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> QueryProduct(string sort, string statusProduct)
        {
            if(manager == null)
                return Json(new {
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

            List<Product> products = await db.Products.Include(p => p.TypeProductSale).Include(p => p.TypeProduct).ToListAsync();

            switch (indexStatus)
            {
                case 1:
                    {
                        var typeSale =  await db.TypeProductSales.Where(item => item.StatusProduct.Equals("Bán hàng")).FirstOrDefaultAsync();
                        if (typeSale != null)
                            products = products
                                .Where(item => item.IdTypeSale.Equals(typeSale.IdTypeSale))
                                .ToList();
                        break;
                    }
                case 2:
                    {
                        products = products
                               .Where(item => item.Quantity <= 0)
                               .ToList();
                        break;
                    }
                case 3:
                    {
                        var typeSale = await db.TypeProductSales.Where(item => item.StatusProduct.Equals("Không bán")).FirstOrDefaultAsync();
                        if (typeSale != null)
                            products = products
                                .Where(item => item.IdTypeSale.Equals(typeSale.IdTypeSale))
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
                    products.Sort((first, second) => first.NameProduct.CompareTo(second.NameProduct));
                    break;
                case 2:
                    products.Sort((first, second) => first.Quantity.Value.CompareTo(second.Quantity));
                    break;
                case 3:
                    products.Sort((first, second) => first.Price.Value.CompareTo(second.Price));
                    break;
                default:
                    break;
            }

            var jsonResult = new List<object>();
            foreach (var item in products)
            {
                if (item == null)
                {
                    continue; // Skip invalid items
                }

                var jsonData = new
                {
                    id_product = item.IdProduct,
                    image_url = item.Image,
                    name_product = item.NameProduct,
                    description = item.Description,
                    price = item.Price,
                    discount_product = item.Discount,
                    quantity = item.Quantity,
                    type_product = item.TypeProduct?.TypeProductName, 
                    status_product = item.TypeProductSale?.StatusProduct, 
                };
                jsonResult.Add(jsonData);
            }

            return Json(new { status = true, data = jsonResult }, JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [ManagerAuthorize]
        public ActionResult CreateProduct()
        {
            ViewBag.IdTypeProduct = new SelectList(db.TypeProducts, "IdTypeProduct", "TypeProductName");
            ViewBag.IdTypeSale = new SelectList(db.TypeProductSales, "IdTypeSale", "StatusProduct");
            return View();
        }
        [ManagerAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateProduct([Bind(Include = "IdProduct,IdTypeProduct,NameProduct,Price,Description,Image,ImageFile,Discount,Quantity,IdTypeSale")] Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageFile != null)
                {
                    var fileSave = MoveImageToProject(product.ImageFile);
                    if (fileSave != null)
                        product.Image = fileSave;
                }

                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdTypeProduct = new SelectList(db.TypeProducts, "IdTypeProduct", "TypeProductName", product.IdTypeProduct);
            ViewBag.IdTypeSale = new SelectList(db.TypeProductSales, "IdTypeSale", "StatusProduct", product.IdTypeSale);
            return View(product);
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

                    string folderPath = @"~\Assets\images\images_Product\";

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
        [ManagerAuthorize]
        public ActionResult UpdateProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdTypeProduct = new SelectList(db.TypeProducts, "IdTypeProduct", "TypeProductName", product.IdTypeProduct);
            ViewBag.IdTypeSale = new SelectList(db.TypeProductSales, "IdTypeSale", "StatusProduct", product.IdTypeSale);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ManagerAuthorize]
        public async Task<ActionResult> UpdateProduct([Bind(Include = "IdProduct,IdTypeProduct,NameProduct,Price,Description,Image,ImageFile,Discount,Quantity,IdTypeSale")] Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageFile != null)
                {
                    var fileSave = MoveImageToProject(product.ImageFile);
                    if (fileSave != null)
                        product.Image = fileSave;
                }
                db.Entry(product).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdTypeProduct = new SelectList(db.TypeProducts, "IdTypeProduct", "TypeProductName", product.IdTypeProduct);
            ViewBag.IdTypeSale = new SelectList(db.TypeProductSales, "IdTypeSale", "StatusProduct", product.IdTypeSale);
            return View(product);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteProduct(int IdProduct)
        {
            if (ModelState.IsValid)
            {
                var product = db.Products.FirstOrDefault(item => item.IdProduct.Equals(IdProduct));
                if (product == null)
                    return Json(false);
                db.Products.Remove(product);
                await db.SaveChangesAsync();
                return Json(true);
            }
            return Json(false);
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
