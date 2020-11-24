using System.Web.Mvc;

namespace GymManager.Controllers
{
    public class EquipmentOrdersController : Controller
    {
        [Route("Orders/Equipment")]
        public ViewResult Index()
        {
            return View();
        }

        [Route("Orders/Equipment/New")]
        public ViewResult New()
        {
            return View();
        }        
        
        [Route("Orders/Equipment/Edit/{id}")]
        public ViewResult Edit()
        {
            return View();
        }
    }
}