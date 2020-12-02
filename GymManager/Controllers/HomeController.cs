using System.Web.Mvc;

namespace GymManager.Controllers
{
    public class HomeController : Controller
    {

        [AllowAnonymous]
        public ViewResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View("LoggedInIndex");
            }

            return View();
        }

        public ViewResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}