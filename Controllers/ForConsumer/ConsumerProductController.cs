using DotNet_E_Commerce_Glasses_Web.App_Start;
using DotNet_E_Commerce_Glasses_Web.Models;
using DotNet_E_Commerce_Glasses_Web.Sessions;
using DotNet_E_Commerce_Glasses_Web.Utils;
using Microsoft.Ajax.Utilities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DotNet_E_Commerce_Glasses_Web.Controllers.ForConsumer
{
    public class ConsumerProductController : Controller
    {
        private GlassesEntities db = new GlassesEntities();

        private readonly Dictionary<int, int> dicCart;
        private readonly Consumer consumer;
        private readonly Bill bill;

        public ConsumerProductController()
        {
            string session = ConsumerSession.getConsumerSession();
            if (session != null && int.TryParse(session, out int consumerID))
            {
                consumer = db.Consumers.FirstOrDefault(item => item.IdConsumer.Equals(consumerID));
                if (consumer != null)
                {
                    if (consumer.ListCart.IsNullOrWhiteSpace())
                        dicCart = new Dictionary<int, int>();
                    else
                        dicCart = JsonUtils.convertJsonCartToDic(consumer.ListCart);
                    bill = new Bill();
                    bill.Consumer = consumer;
                    bill.IdConsumer = consumerID;
                }
            }   
        }

        [HttpGet]
        public async Task<ActionResult> Index(string typeProductID, int? page, string search)
        {
            ViewBag.Consumer = consumer;
            ViewBag.TypeProduct = await db.TypeProducts.ToListAsync() as ICollection<TypeProduct>;
            ViewBag.TypeProductID = typeProductID;
            ViewBag.Page = page ?? 1;
            ViewBag.Search = search;

            int pageNumber = page ?? 1;
            int pageSize = 12;

            var sanPhams = await db.Products.Where(item => item.Quantity.Value > 0).ToListAsync();
            if (!string.IsNullOrWhiteSpace(typeProductID))
                sanPhams = sanPhams.Where(item => item.TypeProduct.IdTypeProduct.ToString().Equals(typeProductID)).ToList();
            if (!string.IsNullOrWhiteSpace(search))
                sanPhams = sanPhams.Where(item => item.NameProduct.ToLower().Contains(search.ToLower())).ToList();
            return View(sanPhams.ToPagedList(pageNumber, pageSize));
        }


        [HttpGet]
        public async Task<ActionResult> DetailProduct(int? product_id)
        {
            if (product_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(product_id);
            if (product == null)
            {
                return HttpNotFound();
            }
            if (consumer != null)
            {
                ViewBag.Consumer = consumer;
            }
            return View(new ProductSaleModel(quanitySale: 1, product: product));
        }

        [HttpPost]
        public async Task<JsonResult> AddProductToCart(int productId, int quantity)
        {
            if (consumer == null)
                return Json(new
                {
                    status = false,
                    message = "Yêu cầu đăng nhập trước khi mua hàng",
                    url = "/LoginAccount/Index"
                });
            else
            {
                int temp = 0;
                if (dicCart.ContainsKey(productId))
                    temp = dicCart[productId];
                var product = await db.Products.FindAsync(productId);
                if (product != null && product.Quantity - temp > 0)
                {
                    if (dicCart.ContainsKey(productId))
                        dicCart[productId] += quantity;
                    else
                        dicCart.Add(productId, quantity);
                    if (dicCart[productId] < 0)
                        dicCart[productId] = 0;

                    consumer.ListCart = JsonUtils.convertDicToCartJson(dicCart);

                    db.Consumers.AddOrUpdate(consumer);
                    await db.SaveChangesAsync();
                    return Json(new
                    {
                        status = true,
                        message = "Thêm vào giỏ hàng thành công",
                        url = ""
                    });
                }
                else
                    return Json(new
                    {
                        status = false,
                        message = "Hết hàng",
                        url = ""
                    });
            }
        }
        [HttpGet]
        public async Task<ActionResult> Payment(string code)
        {
            if (consumer == null)
                return RedirectToAction("Index", "LogInAccount");
            else if (dicCart.Count <= 0)
            {
                return RedirectToAction("ConsumerCart", "ConsumerProduct");
            }
            else
            {
                Dictionary<int, ProductSaleModel> temp = new Dictionary<int, ProductSaleModel>();
                if (dicCart != null)
                {
                    foreach (var pair in dicCart)
                    {
                        var key = pair.Key;
                        var value = pair.Value;
                        var product = await db.Products.FindAsync(key);
                        if (product != null)
                            temp.Add(key, new ProductSaleModel(value, product));
                    }
                }
                if (!code.IsNullOrWhiteSpace())
                {
                    var a = db.Discounts.FirstOrDefault(item => item.CodeDiscount.Equals(code));
                    bill.Discount = db.Discounts.FirstOrDefault(item => item.CodeDiscount.Equals(code));
                }
                bill.TotalBill = CalculatorTotalBill(temp);
                bill.TotalPay = CalculatorTotalPay(bill);

                ViewBag.ListCart = temp.Values;
                ViewBag.Consumer = consumer;

                return View(bill);
            }
        }
        [HttpPost]
        public async Task<JsonResult> Payment(int productId, int quantity)
        {
            if (consumer == null)
                return Json(new
                {
                    status = false,
                    message = "Yêu cầu đăng nhập trước khi mua hàng",
                    url = "/LoginAccount/Index"
                });
            else
            {
                int temp = 0;
                if (dicCart.ContainsKey(productId))
                    temp = dicCart[productId];
                var product = await db.Products.FindAsync(productId);
                if (product != null && product.Quantity - temp > 0)
                {
                    if (dicCart.ContainsKey(productId))
                        dicCart[productId] += quantity;
                    else
                        dicCart.Add(productId, quantity);
                    if (dicCart[productId] < 0)
                        dicCart[productId] = 0;

                    consumer.ListCart = JsonUtils.convertDicToCartJson(dicCart);

                    db.Consumers.AddOrUpdate(consumer);
                    await db.SaveChangesAsync();
                    return Json(new
                    {
                        status = true,
                        message = "Thêm vào giỏ hàng thành công",
                        url = ""
                    });
                }
                else
                    return Json(new
                    {
                        status = false,
                        message = "Hết hàng",
                        url = ""
                    });
            }
        }
        private long CalculatorTotalBill(Dictionary<int, ProductSaleModel> dicSale)
        {
            long result = 0;
            dicSale.Values.ForEach(item => result += item.QuanitySale * item.Price.Value);
            return result;
        }

        private long CalculatorTotalPay(Bill bill)
        {
            if (bill.Discount != null && bill.Discount.PercentValue > 0)
                return (long)(bill.TotalBill * (100 - bill.Discount.PercentValue) / 100);
            else
                return bill.TotalBill ?? 0;
        }

        [HttpGet]
        public async Task<ActionResult> ConsumerCart(string code)
        {
            if (consumer == null)
                return RedirectToAction("Index", "LogInAccount");
            else
            {
                if (!string.IsNullOrWhiteSpace(code))
                {
                    var discount = db.Discounts.FirstOrDefault(item => item.CodeDiscount.Equals(code));
                    bill.Discount = discount;
                }


                Dictionary<int, ProductSaleModel> temp = new Dictionary<int, ProductSaleModel>();
                if (dicCart != null)
                {
                    foreach (var pair in dicCart)
                    {
                        var key = pair.Key;
                        var value = pair.Value;
                        var product = await db.Products.FindAsync(key);
                        if (product != null)
                            temp.Add(key, new ProductSaleModel(value, product));
                    }
                }
                bill.TotalBill = CalculatorTotalBill(temp);
                bill.TotalPay = CalculatorTotalPay(bill);

                ViewBag.ListCart = temp.Values;
                ViewBag.Consumer = consumer;

                return View(bill);
            }
        }

        [HttpPost]
        public ActionResult ConsumerCart(Bill bill)
        {
            if (consumer == null)
                return RedirectToAction("DangNhap", "DangNhap");
            else
            {
                Dictionary<int, ProductSaleModel> temp = new Dictionary<int, ProductSaleModel>();
                if (dicCart != null)
                {
                    foreach (var pair in dicCart)
                    {
                        var key = pair.Key;
                        var value = pair.Value;
                        temp.Add(key, new ProductSaleModel(value, db.Products.FirstOrDefault(item => item.IdProduct.Equals(key))));
                    }
                }
                bill.TotalBill = CalculatorTotalBill(temp);
                bill.TotalPay = CalculatorTotalPay(bill);

                ViewBag.ListCart = temp.Values;
                ViewBag.Consumer = consumer;
                return View(bill);
            }
        }

        [HttpPost]
        public async Task<JsonResult> AddDiscount(string codeDiscount)
        {
            if(consumer == null)
                return Json(new
                {
                    status = false,
                    message = "",
                    url = "/LoginAccount/Index"
                });
            var discount = await db.Discounts.FirstOrDefaultAsync(item => item.CodeDiscount.Equals(codeDiscount));
            if (discount != null)
            {
                if (discount.DateOfStart > DateTime.Now)
                    return Json(new
                    {
                        status = false,
                        message = "Chưa đến ngày bắt đầu!"
                    });
                else if (discount.Quantity <= 0)
                    return Json(new
                    {
                        status = false,
                        message = "Số lượng đã hết!"
                    });
                else if (discount.DateOfEnd < DateTime.Now)
                    return Json(new
                    {
                        status = false,
                        message = "Mã khuyến mãi đã hết hạn!"
                    });
                else if (!discount.IdStatus.Equals(
                    db.StatusDiscounts.FirstOrDefault(
                        item => item.Status.Equals("Hoạt động")).IdStatus))
                    return Json(new
                    {
                        status = false,
                        message = "Mã khuyến mãi không hoạt động!",
                        url = ""
                    });
                else
                {
                    bill.Discount = discount;

                    return Json(new
                    {
                        status = true,
                        message = "",
                        url = "/ConsumerProduct/Payment?code=" + codeDiscount
                    });
                }
            }
            return Json(new
            {
                status = false,
                message = "Không có mã hoạt động!",
                url = ""
            });

        }

        [HttpPost]
        public async Task<JsonResult> RemoveFromCart(int productId)
        {
            if (consumer == null)
                return Json(new
                {
                    status = false,
                    url = "/LogInAccount/Index"
                });
            else
            {
                dicCart.Remove(productId);
                consumer.ListCart = JsonUtils.ConvertDicToJson(dicCart);
                db.Consumers.AddOrUpdate(consumer);
                await db.SaveChangesAsync();
                return Json(new
                {
                    status = true
                });
            }
        }

        [HttpPost]
        public async Task<JsonResult> CancelBill()
        {
            if (consumer == null)
                return Json(new
                {
                    status = false,
                    message = "Yêu cầu đăng nhập trước khi hủy thanh toán!",
                    url = "/LogInAccount/Index"
                });
            else
            {
                consumer.ListCart = string.Empty;
                db.Consumers.AddOrUpdate(consumer);
                await db.SaveChangesAsync();
                return Json(new
                {
                    status = true,
                    message = "Hủy thanh toán thành công!"
                });
            }
        }

        [HttpPost]
        public async Task<JsonResult> PayBill()
        {
            if (consumer == null)
                return Json(new
                {
                    status = false,
                    url = "/LogInAccount/Index",
                    message = ""
                });

            if (JsonUtils.convertJsonCartToDic(consumer.ListCart) == null)
                return Json(new
                {
                    status = false,
                    url = "",
                    message = "Giỏ hàng trống"
                });
            if (bill != null)
            {
                DetailBill detailBill = new DetailBill
                {
                    ListBill = consumer.ListCart
                };
                db.DetailBills.Add(detailBill);
                await db.SaveChangesAsync();


                Dictionary<int, ProductSaleModel> temp = new Dictionary<int, ProductSaleModel>();

                if (dicCart != null)
                {
                    foreach (var pair in dicCart)
                    {
                        var key = pair.Key;
                        var value = pair.Value;
                        var product = await db.Products.FindAsync(key);

                        if (product != null)
                        {
                            temp.Add(key, new ProductSaleModel(value, product));
                            product.Quantity -= value;
                            db.Products.AddOrUpdate(product);
                            await db.SaveChangesAsync();
                        }
                    }
                }

                bill.DetailBill = detailBill;
                bill.DateOfPurchase = DateTime.Now;
                bill.TotalBill = CalculatorTotalBill(temp);
                bill.TotalPay = CalculatorTotalPay(bill);
                bill.StatusDelivery = await db.StatusDeliveries.FirstOrDefaultAsync(item => item.Status.Equals("Chưa giao hàng"));

                dicCart.Clear();
                consumer.ListCart = string.Empty;
                db.Consumers.AddOrUpdate(consumer);
                db.Bills.Add(bill);
                await db.SaveChangesAsync();
                return Json(new
                {
                    status = true,
                    url = "/ConsumerProduct/Index",
                    message = "Thanh toán thành công!"
                });
            }
            else
                return Json(new
                {
                    status = false,
                    url = "",
                    message = "Thanh toán thất bại!"
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
