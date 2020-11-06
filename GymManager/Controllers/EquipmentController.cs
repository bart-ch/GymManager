using GymManager.Models;
using System.Web.Mvc;

namespace GymManager.Controllers
{
    public class EquipmentController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            return View();
        }        
        
        public ActionResult Edit()
        {
            return View();
        }
    }
}