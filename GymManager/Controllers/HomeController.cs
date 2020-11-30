using System.Web.Mvc;

namespace GymManager.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View("LoggedInIndex");
            }

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}