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
    public class ManagerDiscountController : Controller
    {
        private GlassesEntities db = new GlassesEntities();

        // GET: ManagerDiscount
        public async Task<ActionResult> Index()
        {
            var discounts = db.Discounts.Include(d => d.StatusDiscount);
            return View(await discounts.ToListAsync());
        }

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

        // GET: ManagerDiscount/Create
        public ActionResult Create()
        {
            ViewBag.IdStatus = new SelectList(db.StatusDiscounts, "IdStatus", "Status");
            return View();
        }

        // POST: ManagerDiscount/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdDiscount,IdStatus,TitleDiscount,DateOfStart,DateOfEnd,Quantity,Percent,Image")] Discount discount)
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
        public async Task<ActionResult> Edit(int? id)
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
        public async Task<ActionResult> Edit([Bind(Include = "IdDiscount,IdStatus,TitleDiscount,DateOfStart,DateOfEnd,Quantity,Percent,Image")] Discount discount)
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
