using DotNet_E_Commerce_Glasses_Web.App_Start;
using DotNet_E_Commerce_Glasses_Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DotNet_E_Commerce_Glasses_Web.Controllers.ForManager
{
    [ManagerAuthorize]
    public class DashBoardController : Controller
    {
        private GlassesEntities db = new GlassesEntities();
        private Dictionary<DateTime, DashBoardModel> dicTotalPay;
        // Constructor
        public DashBoardController()
        {
            dicTotalPay = new Dictionary<DateTime, DashBoardModel>();
            db.Bills.ToList().ForEach(item =>
            {
                if (dicTotalPay.ContainsKey(item.DateOfPurchase.Value.Date))
                {
                    dicTotalPay[item.DateOfPurchase.Value.Date].TotalPay += item.TotalPay.Value;
                    dicTotalPay[item.DateOfPurchase.Value.Date].Product += item.DetailBill.totalQuanityProduct();
                }
                else dicTotalPay.Add(item.DateOfPurchase.Value.Date, new DashBoardModel(
                    date: item.DateOfPurchase.Value.Date,
                    totalPay: item.TotalPay.Value,
                    product: item.DetailBill.totalQuanityProduct()));
            });
            dicTotalPay = dicTotalPay.OrderBy(item => item.Key).ToDictionary(pair => pair.Key, pair => pair.Value);
        }
        public ActionResult Index() => View();
        // Thống kê doanh thu bán được theo ngày
        [HttpPost]
        public JsonResult GetThongKe(string date)
        {
            DateTime currentDate;
            if (DateTime.TryParseExact(date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out currentDate))
            {
                if (dicTotalPay.ContainsKey(currentDate))
                    return Json(
                        new
                        {
                            status = true,
                            message = "",
                            DoanhThu = dicTotalPay[currentDate].TotalPay,
                            SanPham = dicTotalPay[currentDate].Product
                        });
                else
                    return Json(
                        new
                        {
                            status = true,
                            message = "",
                            DoanhThu = 0,
                            SanPham = 0
                        });
            }
            else
                return Json(new { status = false, message = "", DoanhThu = 0, SanPham = 0 });
        }
        // Vẽ chart theo thời gian được truyền vào hàm
        [HttpPost]
        public JsonResult GetChart(string dateStart, string dateEnd)
        {
            DateTime dateTimeStart;
            DateTime dateTimeEnd;
            // Convert to DateTime
            if (DateTime.TryParseExact(dateStart, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out dateTimeStart) && DateTime.TryParseExact(dateEnd, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out dateTimeEnd))
                return Json(
                    new
                    {
                        status = true,
                        message = "",
                        data = ConvertDoanhThuJson(dateTimeStart, dateTimeEnd),
                        sanPham = ConvertSanPhamJson(dateTimeStart, dateTimeEnd)
                    });
            else
                return Json(
                    new
                    {
                        status = false,
                        message = "",
                        data = ""
                    });
        }
        // Convert data thành Json để giao tiếp với View (DoanhThu)
        private string ConvertDoanhThuJson(DateTime start, DateTime end)
        {
            List<string> listDate = new List<string>();
            List<long> listDoanhThu = new List<long>();
            List<int> listSanPham = new List<int>();
            var listTemp = dicTotalPay
                .Where(item => item.Key >= start && item.Key <= end)
                .ToDictionary(item => item.Key, item => item.Value);
            foreach (var item in listTemp)
            {
                listDate.Add(item.Value.Date.ToString("dd/MM/yyyy"));
                listDoanhThu.Add(item.Value.TotalPay);
                listSanPham.Add(item.Value.Product);
            }
            var data = new
            {
                Ngay = listDate,
                DoanhThu = listDoanhThu,
                SanPham = listSanPham
            };
            return JsonConvert.SerializeObject(data);
        }

        // Convert data thành Json để giao tiếp với View (SanPham)
        private string ConvertSanPhamJson(DateTime start, DateTime end)
        {
            List<string> listSanPham = new List<string>();
            List<int> listSoLuong = new List<int>();
            var temp = db.Bills.Where(item => item.DateOfPurchase.Value >= start && item.DateOfPurchase.Value <= end).ToList();
            Dictionary<int, int> sanPhamDic = new Dictionary<int, int>();

            foreach (var item in temp)
            {
                Dictionary<int, int> a = item.DetailBill.listProductDetail();
                foreach (var item2 in a)
                {
                    if (sanPhamDic.ContainsKey(item2.Key))
                        sanPhamDic[item2.Key] += item2.Value;
                    else
                        sanPhamDic.Add(item2.Key, item2.Value);
                }
            }
            foreach (var item in sanPhamDic)
            {
                var temp1 = db.Products.FirstOrDefault(value => value.IdProduct.Equals(item.Key));
                if (temp1 != null)
                {
                    listSanPham.Add(temp1.NameProduct);
                    listSoLuong.Add(item.Value);
                }
            }
            var data = new
            {
                TenSanPham = listSanPham,
                SoLuong = listSoLuong
            };
            return JsonConvert.SerializeObject(data);
        }
    }
}