using DotNet_E_Commerce_Glasses_Web.Models;
using DotNet_E_Commerce_Glasses_Web.Sessions;
using System.Linq;
using System.Web.Mvc;

namespace DotNet_E_Commerce_Glasses_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly GlassesEntities db = new GlassesEntities();
        private readonly Manager manager;

        [HttpGet]
        public ActionResult Index()
        {
            var consumerId = ConsumerSession.getConsumerSession();
            if (!string.IsNullOrEmpty(consumerId))
            {
                Consumer consumer = db.Consumers.ToList().FirstOrDefault(item => item.IdConsumer.ToString().Equals(consumerId));
                if (consumer != null)
                    ViewBag.Consumer = consumer;
            }
            else
                ViewBag.Consumer = null;
            return View();
        }
    }
}