using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymManager.Controllers
{
    public class EquipmentController : Controller
    {
        // GET: GymEquipment
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