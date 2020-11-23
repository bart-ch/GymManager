using System.Web.Mvc;

namespace GymManager.Controllers
{
    public class EquipmentOrdersController : Controller
    {
        [Route("Orders/Equipment")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("Orders/Equipment/New")]
        public ActionResult New()
        {
            return View();
        }
    }
}