using GymManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymManager.Controllers
{
    public class EquipmentController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();


        public ActionResult Index()
        {
            var areas = context.Areas.ToList();
            return View(areas);
        }

        public ActionResult New()
        {
            return View("EquipmentForm");
        }
    }
}