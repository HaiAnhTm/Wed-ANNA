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

namespace DotNet_E_Commerce_Glasses_Web.Controllers.ForManager
{
    public class ManagerProductController : Controller
    {
        private GlassesEntities db = new GlassesEntities();

        // GET: ManagerProduct
        public async Task<ActionResult> Index(string sort)
        {
            int indexSort;
            if (!int.TryParse(sort, out indexSort))
            {
                indexSort = -1;
            }
            var products = await db.Products.Include(p => p.TypeProductSale).Include(p => p.TypeProduct).ToListAsync();
            switch (indexSort)
            {
                case 1:
                    products.Sort((first, second) => first.NameProduct.CompareTo(second.NameProduct));
                    break;
                case 2:
                    products.Sort((first, second) => first.Quantity.CompareTo(second.Quantity));
                    break;
                case 3:
                    products.Sort((first, second) => first.Price.CompareTo(second.Price));
                    break;
                default:
                    break;
            }

            return View(products);
        }

        // GET: ManagerProduct/Details/5
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

        // GET: ManagerProduct/Create
        public ActionResult CreateProduct()
        {
            ViewBag.IdTypeProduct = new SelectList(db.TypeProducts, "IdTypeProduct", "TypeProductName");
            ViewBag.IdTypeSale = new SelectList(db.TypeProductSales, "IdTypeSale", "StatusProduct");
            return View();
        }

        // POST: ManagerProduct/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateProduct([Bind(Include = "IdProduct,IdTypeProduct,NameProduct,Price,Description,Image,ImageFile,Discount,Quantity,IdTypeSale")] Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageFile != null)
                    product.Image = MoveImageToProject(product.ImageFile);
                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdTypeProduct = new SelectList(db.TypeProducts, "IdTypeProduct", "TypeProductName", product.IdTypeProduct);
            ViewBag.IdTypeSale = new SelectList(db.TypeProductSales, "IdTypeSale", "StatusProduct", product.IdTypeSale);
            return View(product);
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

                    string folderPath = @"~\Assets\images\images_Product\";

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


        // GET: ManagerProduct/Edit/5
        public async Task<ActionResult> UpdateProduct(int? id)
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
            ViewBag.IdTypeProduct = new SelectList(db.TypeProducts, "IdTypeProduct", "TypeProductName", product.IdTypeProduct);
            ViewBag.IdTypeSale = new SelectList(db.TypeProductSales, "IdTypeSale", "StatusProduct", product.IdTypeSale);
            return View(product);
        }

        // POST: ManagerProduct/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateProduct([Bind(Include = "IdProduct,IdTypeProduct,NameProduct,Price,Description,Image,ImageFile,Discount,Quantity,IdTypeSale")] Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageFile != null)
                {
                    product.Image = MoveImageToProject(product.ImageFile);
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
        public JsonResult DeleteProduct(int IdProduct)
        {
            if (ModelState.IsValid)
            {
                var product = db.Products.FirstOrDefault(item => item.IdProduct.Equals(IdProduct));
                if (product == null)
                    return Json(false);
                db.Products.Remove(product);
                db.SaveChanges();
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
