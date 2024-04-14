using DotNet_E_Commerce_Glasses_Web.Models;
using DotNet_E_Commerce_Glasses_Web.Sessions;
using DotNet_E_Commerce_Glasses_Web.Utils;
using Microsoft.Ajax.Utilities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DotNet_E_Commerce_Glasses_Web.Controllers.ForCommon
{
    public class CommonController : Controller
    {

        private readonly GlassesEntities db = new GlassesEntities();

        private readonly Dictionary<int, int> dicCart;
        private readonly Consumer consumer;


        public CommonController()
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
                }
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetCart()
        {
            if (consumer == null)
                return Json(new
                {
                    status = false,
                    message = "Yêu cầu đăng nhập trước khi mua hàng",
                    url = "/LoginAccount/Index"
                });

            var jsonData = new List<object>();

            var index = 0;
            foreach (var item in dicCart)
            {
                if (index >= 4)
                    break;
                var product = await db.Products.FirstOrDefaultAsync(p => p.IdProduct == item.Key);

                if (product != null)
                {
                    var json = new
                    {
                        id_product = product.IdProduct,
                        quanity = item.Value,
                        type_product = product.TypeProduct.TypeProductName,
                        price = product.Price,
                        image_url = product.Image,
                        name_product = product.NameProduct

                    };
                    jsonData.Add(json);
                }
                index++;
            }

            return Json(new
            {
                status = true,
                data = jsonData
            });
        }

        [HttpPost]
        public async Task<JsonResult> GetProduct()
        {
            var products = await db.Products.ToListAsync();
            var jsonData = new List<object>();

            var index = 0;
            foreach (var item in products)
            {
                if (index >= 4)
                    break;
                if(item.Quantity > 0 && item.TypeProductSale.StatusProduct.Equals("Bán hàng"))
                {
                    var json = new
                    {
                        id_product = item.IdProduct,
                        type_product = item.TypeProduct.TypeProductName,
                        price = item.Price,
                        image_url = item.Image,
                        name_product = item.NameProduct

                    };
                    jsonData.Add(json);
                    index++;
                }
            }

            return Json(new
            {
                status = true,
                data = jsonData
            });
        }
    }
}