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
using DotNet_E_Commerce_Glasses_Web.App_Start;

namespace DotNet_E_Commerce_Glasses_Web.Controllers.ForManager
{
    [ManagerAuthorize]
    public class ManagerBillController : Controller
    {
        private readonly GlassesEntities db = new GlassesEntities();

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
            Dictionary<int, ProductSaleModel> productSaleDic = bill.DetailBill
             .listProductDetail()
             .ToDictionary(
                 item => item.Key,
                 item => new ProductSaleModel(item.Value, db.Products.FirstOrDefault(sanPham => sanPham.IdProduct.Equals(item.Key)) ?? new ProductSaleModel(item.Value))
             );

            List<ItemListBillModel> listBill = new List<ItemListBillModel>();
            productSaleDic.Values.ToList().ForEach(item => listBill.Add(new ItemListBillModel(item.NameProduct, item.QuanitySale)));
            return Json(new
            {
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
