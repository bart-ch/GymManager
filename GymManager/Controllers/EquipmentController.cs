using GymManager.Models;
using System.Web.Mvc;

namespace GymManager.Controllers
{
    public class EquipmentController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            return View("EquipmentForm");
        }
    }
}