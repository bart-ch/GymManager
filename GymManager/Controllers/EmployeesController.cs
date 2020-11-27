using GymManager.Core;
using System.Web.Mvc;

namespace GymManager.Controllers
{
    [Authorize(Roles = RoleName.CanManageEmployees)]
    public class EmployeesController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult Details()
        {
            return View();
        }

        public ViewResult Edit()
        {
            return View();
        }
    }
}