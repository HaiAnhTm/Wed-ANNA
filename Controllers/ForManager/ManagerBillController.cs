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
using Newtonsoft.Json;

namespace DotNet_E_Commerce_Glasses_Web.Controllers.ForManager
{
    public class ManagerBillController : Controller
    {
        private GlassesEntities db = new GlassesEntities();

        // GET: ManagerBill
        public async Task<ActionResult> Index()
        {
            var bills = db.Bills.Include(b => b.Consumer).Include(b => b.DetailBill).Include(b => b.Discount).Include(b => b.StatusDelivery);
            return View(await bills.ToListAsync());
        }

        [HttpPost]
        public async Task<JsonResult> DetailBill(int billId)
        {
            var bill = await db.Bills.FindAsync(billId);
            if(bill == null)
                return Json(new { 
                    status = false, 
                    data = new
                    {

                    },
                    message = ""
                });
            Dictionary<int, ProductSale> productSaleDic = bill.DetailBill
             .listProductDetail()
             .ToDictionary(
                 item => item.Key,
                 item => new ProductSale(item.Value, db.Products.FirstOrDefault(sanPham => sanPham.IdProduct.Equals(item.Key)) ?? new ProductSale(item.Value))
             );

            List<ItemListBill> listBill = new List<ItemListBill>();
            productSaleDic.Values.ToList().ForEach(item => listBill.Add(new ItemListBill(item.NameProduct, item.QuanitySale)));
            return Json(new { 
                status = true, 
                data =  new {
                    ConsumerName = bill.Consumer.Username,
                    ConsumerImage = bill.Consumer.Image,
                    TotalBill = bill.currencyTotalBill(),
                    TotalPay = bill.currencyTotalPay(),
                    PercentDiscount = bill.valuePercent(),
                    DetailListBill = listBill,
                    StatusDelivery = bill.StatusDelivery.Status
                },
                message = ""
            });
        }


        //// GET: ManagerBill/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Bill bill = await db.Bills.FindAsync(id);
        //    if (bill == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(bill);
        //}

        //// GET: ManagerBill/Create
        //public ActionResult Create()
        //{
        //    ViewBag.IdConsumer = new SelectList(db.Consumers, "IdConsumer", "Username");
        //    ViewBag.IdDetailBill = new SelectList(db.DetailBills, "IdDetailBill", "ListBill");
        //    ViewBag.IdDetailDiscount = new SelectList(db.Discounts, "IdDiscount", "TitleDiscount");
        //    ViewBag.IdDetailDelivery = new SelectList(db.StatusDeliveries, "IdStatus", "Status");
        //    return View();
        //}

        //// POST: ManagerBill/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "IdBild,IdConsumer,PercentDiscount,IdDetailDiscount,DateOfPurchase,TotalBill,TotalPay,IdDetailDelivery,IdDetailBill")] Bill bill)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Bills.Add(bill);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.IdConsumer = new SelectList(db.Consumers, "IdConsumer", "Username", bill.IdConsumer);
        //    ViewBag.IdDetailBill = new SelectList(db.DetailBills, "IdDetailBill", "ListBill", bill.IdDetailBill);
        //    ViewBag.IdDetailDiscount = new SelectList(db.Discounts, "IdDiscount", "TitleDiscount", bill.IdDetailDiscount);
        //    ViewBag.IdDetailDelivery = new SelectList(db.StatusDeliveries, "IdStatus", "Status", bill.IdDetailDelivery);
        //    return View(bill);
        //}

        //// GET: ManagerBill/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Bill bill = await db.Bills.FindAsync(id);
        //    if (bill == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.IdConsumer = new SelectList(db.Consumers, "IdConsumer", "Username", bill.IdConsumer);
        //    ViewBag.IdDetailBill = new SelectList(db.DetailBills, "IdDetailBill", "ListBill", bill.IdDetailBill);
        //    ViewBag.IdDetailDiscount = new SelectList(db.Discounts, "IdDiscount", "TitleDiscount", bill.IdDetailDiscount);
        //    ViewBag.IdDetailDelivery = new SelectList(db.StatusDeliveries, "IdStatus", "Status", bill.IdDetailDelivery);
        //    return View(bill);
        //}

        //// POST: ManagerBill/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "IdBild,IdConsumer,PercentDiscount,IdDetailDiscount,DateOfPurchase,TotalBill,TotalPay,IdDetailDelivery,IdDetailBill")] Bill bill)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(bill).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.IdConsumer = new SelectList(db.Consumers, "IdConsumer", "Username", bill.IdConsumer);
        //    ViewBag.IdDetailBill = new SelectList(db.DetailBills, "IdDetailBill", "ListBill", bill.IdDetailBill);
        //    ViewBag.IdDetailDiscount = new SelectList(db.Discounts, "IdDiscount", "TitleDiscount", bill.IdDetailDiscount);
        //    ViewBag.IdDetailDelivery = new SelectList(db.StatusDeliveries, "IdStatus", "Status", bill.IdDetailDelivery);
        //    return View(bill);
        //}

        //// GET: ManagerBill/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Bill bill = await db.Bills.FindAsync(id);
        //    if (bill == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(bill);
        //}

        //// POST: ManagerBill/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    Bill bill = await db.Bills.FindAsync(id);
        //    db.Bills.Remove(bill);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

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
