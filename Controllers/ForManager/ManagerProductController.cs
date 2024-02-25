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

namespace DotNet_E_Commerce_Glasses_Web.Controllers.ForManager
{
    public class ManagerProductController : Controller
    {
        private GlassesEntities db = new GlassesEntities();

        // GET: ManagerProduct
        public async Task<ActionResult> Index()
        {
            var products = db.Products.Include(p => p.TypeProductSale).Include(p => p.TypeProduct);
            return View(await products.ToListAsync());
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
        public ActionResult Create()
        {
            ViewBag.IdTypeSale = new SelectList(db.TypeProductSales, "IdTypeSale", "IdTypeSale");
            ViewBag.IdTypeProduct = new SelectList(db.TypeProducts, "IdTypeProduct", "TypeProductName");
            return View();
        }

        // POST: ManagerProduct/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdProduct,IdTypeProduct,NameProduct,Price,Description,Image,Discount,Quantity,IdTypeSale")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdTypeSale = new SelectList(db.TypeProductSales, "IdTypeSale", "IdTypeSale", product.IdTypeSale);
            ViewBag.IdTypeProduct = new SelectList(db.TypeProducts, "IdTypeProduct", "TypeProductName", product.IdTypeProduct);
            return View(product);
        }

        // GET: ManagerProduct/Edit/5
        public async Task<ActionResult> Edit(int? id)
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
            ViewBag.IdTypeSale = new SelectList(db.TypeProductSales, "IdTypeSale", "IdTypeSale", product.IdTypeSale);
            ViewBag.IdTypeProduct = new SelectList(db.TypeProducts, "IdTypeProduct", "TypeProductName", product.IdTypeProduct);
            return View(product);
        }

        // POST: ManagerProduct/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdProduct,IdTypeProduct,NameProduct,Price,Description,Image,Discount,Quantity,IdTypeSale")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdTypeSale = new SelectList(db.TypeProductSales, "IdTypeSale", "IdTypeSale", product.IdTypeSale);
            ViewBag.IdTypeProduct = new SelectList(db.TypeProducts, "IdTypeProduct", "TypeProductName", product.IdTypeProduct);
            return View(product);
        }

        // GET: ManagerProduct/Delete/5
        public async Task<ActionResult> Delete(int? id)
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

        // POST: ManagerProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product product = await db.Products.FindAsync(id);
            db.Products.Remove(product);
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
