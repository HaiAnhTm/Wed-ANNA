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
using DotNet_E_Commerce_Glasses_Web.Utils;

namespace DotNet_E_Commerce_Glasses_Web.Controllers.ForManager
{
    public class ManagerBillController : Controller
    {
        private GlassesEntities db = new GlassesEntities();

        public ActionResult Index()
        {
            var bills = db.Bills.Include(h => h.DetailBill).Include(h => h.Consumer);
            return View(bills.ToList());
        }

        [HttpGet]
        public async Task<JsonResult> GetDetailBill(string idBillStr)
        {
            int idBill = int.Parse(idBillStr);
            var searchBill = db.Bills.FirstOrDefault(item => item.IdBild == idBill) ?? null;
            if (searchBill != null)
            {
                Dictionary<int, ProductSale> listProductBuy = searchBill
                .DetailBill
                .ListBills()
                .ToDictionary(
                    item => item.Key,
                    item => new ProductSale(
                        quanitySale: item.Value,
                        product: db.Products.FirstOrDefault(product => product.IdProduct == item.Key)));
                return Json(new
                {
                    status = true,
                    message = string.Format("Giá trị " + CurrencyUtils.CurrencyConvertToString(searchBill.TotalPay)),
                    data = new
                    {
                        Consumer = new
                        {
                            Name = searchBill.Consumer.Username,
                            Image = searchBill.Consumer.Image,
                        },
                        Bill = new
                        {
                            TotalBill = searchBill.TotalBill,
                            TotalPay = searchBill.TotalPay,
                            Discount = searchBill.Discount
                        },
                        ListProducts = listProductBuy.Values
                    }
                });
            }
            return Json(new
            {
                status = false,
                message = "Lỗi tìm kiếm đơn hàng",
                data = new
                {

                }
            });
        }

        // GET: ManagerBill/Details/5
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

        // GET: ManagerBill/Create
        public ActionResult Create()
        {
            ViewBag.IdDetailDiscount = new SelectList(db.DetailBills, "IdDetailBill", "ListBill");
            ViewBag.IdConsumer = new SelectList(db.Consumers, "IdConsumer", "Username");
            ViewBag.IdDiscount = new SelectList(db.Discounts, "IdDiscount", "TitleDiscount");
            ViewBag.IdDetailDelivery = new SelectList(db.StatusDeliveries, "IdStatus", "Status");
            return View();
        }

        // POST: ManagerBill/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdBild,IdConsumer,IdDiscount,IdDetailDiscount,DateOfPurchase,TotalBill,TotalPay,IdDetailDelivery")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                db.Bills.Add(bill);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdDetailDiscount = new SelectList(db.DetailBills, "IdDetailBill", "ListBill", bill.IdDetailDiscount);
            ViewBag.IdConsumer = new SelectList(db.Consumers, "IdConsumer", "Username", bill.IdConsumer);
            ViewBag.IdDiscount = new SelectList(db.Discounts, "IdDiscount", "TitleDiscount", bill.IdDiscount);
            ViewBag.IdDetailDelivery = new SelectList(db.StatusDeliveries, "IdStatus", "Status", bill.IdDetailDelivery);
            return View(bill);
        }

        // GET: ManagerBill/Edit/5
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
            ViewBag.IdDetailDiscount = new SelectList(db.DetailBills, "IdDetailBill", "ListBill", bill.IdDetailDiscount);
            ViewBag.IdConsumer = new SelectList(db.Consumers, "IdConsumer", "Username", bill.IdConsumer);
            ViewBag.IdDiscount = new SelectList(db.Discounts, "IdDiscount", "TitleDiscount", bill.IdDiscount);
            ViewBag.IdDetailDelivery = new SelectList(db.StatusDeliveries, "IdStatus", "Status", bill.IdDetailDelivery);
            return View(bill);
        }

        // POST: ManagerBill/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdBild,IdConsumer,IdDiscount,IdDetailDiscount,DateOfPurchase,TotalBill,TotalPay,IdDetailDelivery")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bill).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdDetailDiscount = new SelectList(db.DetailBills, "IdDetailBill", "ListBill", bill.IdDetailDiscount);
            ViewBag.IdConsumer = new SelectList(db.Consumers, "IdConsumer", "Username", bill.IdConsumer);
            ViewBag.IdDiscount = new SelectList(db.Discounts, "IdDiscount", "TitleDiscount", bill.IdDiscount);
            ViewBag.IdDetailDelivery = new SelectList(db.StatusDeliveries, "IdStatus", "Status", bill.IdDetailDelivery);
            return View(bill);
        }

        // GET: ManagerBill/Delete/5
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

        // POST: ManagerBill/Delete/5
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
