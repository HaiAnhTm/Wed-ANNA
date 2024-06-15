using DotNet_E_Commerce_Glasses_Web.Models;
using DotNet_E_Commerce_Glasses_Web.Sessions;
using DotNet_E_Commerce_Glasses_Web.Utils;
using Microsoft.Ajax.Utilities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DotNet_E_Commerce_Glasses_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly GlassesEntities db = new GlassesEntities();

        private readonly Dictionary<int, int> dicCart;
        private readonly Consumer consumer;

        public HomeController()
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
        public ActionResult Index()
        {
            if (consumer != null)
                ViewBag.Consumer = consumer;
            else
                ViewBag.Consumer = null;
            return View();
        }

        [HttpPost]
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

            foreach (var item in dicCart)
            {
                var product = await db.Products.FirstOrDefaultAsync(p => p.IdProduct == item.Key);

                if (product != null)
                {
                    var json = new
                    {
                        id_product = product.IdProduct,
                        quanity = item.Value,
                        type_product = product.TypeProduct.TypeProductName,
                        price = product.Price,
                        iamge_url = product.Image

                    };
                    jsonData.Add(json);
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