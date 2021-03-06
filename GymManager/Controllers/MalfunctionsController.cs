﻿using System.Web.Mvc;

namespace GymManager.Controllers
{
    public class MalfunctionsController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult New()
        {
            return View();
        }

        public ViewResult Edit()
        {
            return View();
        }

        [Route("Malfunctions/History")]
        public ViewResult History()
        {
            return View();
        }

        public ViewResult History(int id)
        {
            return View("SingleEquipmentHistory");
        }

        public ViewResult Details()
        {
            return View();
        }
    }
}