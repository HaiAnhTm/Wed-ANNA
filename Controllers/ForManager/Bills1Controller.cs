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
    public class Bills1Controller : Controller
    {
        private GlassesEntities db = new GlassesEntities();

        // GET: Bills1
        public async Task<ActionResult> Index()
        {
            var bills = db.Bills.Include(b => b.Consumer).Include(b => b.DetailBill).Include(b => b.Discount).Include(b => b.StatusDelivery);
            return View(await bills.ToListAsync());
        }

        // GET: Bills1/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = await db.Bills.FindAsync(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        // GET: Bills1/Create
        public ActionResult Create()
        {
            ViewBag.IdConsumer = new SelectList(db.Consumers, "IdConsumer", "Username");
            ViewBag.IdDetailBill = new SelectList(db.DetailBills, "IdDetailBill", "IdDetailBill");
            ViewBag.IdDetailDiscount = new SelectList(db.Discounts, "IdDiscount", "TitleDiscount");
            ViewBag.IdDetailDelivery = new SelectList(db.StatusDeliveries, "IdStatus", "Status");
            return View();
        }

        // POST: Bills1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdBild,IdConsumer,PercentDiscount,IdDetailDiscount,DateOfPurchase,TotalBill,TotalPay,IdDetailDelivery,IdDetailBill")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                db.Bills.Add(bill);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdConsumer = new SelectList(db.Consumers, "IdConsumer", "Username", bill.IdConsumer);
            ViewBag.IdDetailBill = new SelectList(db.DetailBills, "IdDetailBill", "IdDetailBill", bill.IdDetailBill);
            ViewBag.IdDetailDiscount = new SelectList(db.Discounts, "IdDiscount", "TitleDiscount", bill.IdDetailDiscount);
            ViewBag.IdDetailDelivery = new SelectList(db.StatusDeliveries, "IdStatus", "Status", bill.IdDetailDelivery);
            return View(bill);
        }

        // GET: Bills1/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = await db.Bills.FindAsync(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdConsumer = new SelectList(db.Consumers, "IdConsumer", "Username", bill.IdConsumer);
            ViewBag.IdDetailBill = new SelectList(db.DetailBills, "IdDetailBill", "IdDetailBill", bill.IdDetailBill);
            ViewBag.IdDetailDiscount = new SelectList(db.Discounts, "IdDiscount", "TitleDiscount", bill.IdDetailDiscount);
            ViewBag.IdDetailDelivery = new SelectList(db.StatusDeliveries, "IdStatus", "Status", bill.IdDetailDelivery);
            return View(bill);
        }

        // POST: Bills1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdBild,IdConsumer,PercentDiscount,IdDetailDiscount,DateOfPurchase,TotalBill,TotalPay,IdDetailDelivery,IdDetailBill")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bill).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdConsumer = new SelectList(db.Consumers, "IdConsumer", "Username", bill.IdConsumer);
            ViewBag.IdDetailBill = new SelectList(db.DetailBills, "IdDetailBill", "IdDetailBill", bill.IdDetailBill);
            ViewBag.IdDetailDiscount = new SelectList(db.Discounts, "IdDiscount", "TitleDiscount", bill.IdDetailDiscount);
            ViewBag.IdDetailDelivery = new SelectList(db.StatusDeliveries, "IdStatus", "Status", bill.IdDetailDelivery);
            return View(bill);
        }

        // GET: Bills1/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = await db.Bills.FindAsync(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        // POST: Bills1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Bill bill = await db.Bills.FindAsync(id);
            db.Bills.Remove(bill);
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
