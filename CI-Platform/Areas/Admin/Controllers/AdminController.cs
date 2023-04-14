using Microsoft.AspNetCore.Mvc;

namespace CI_Platform.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        public IActionResult Admin()
        {
            return View();
        }
        public IActionResult GetUsers()
        {
            return PartialView("_User");
        }


    }
}
