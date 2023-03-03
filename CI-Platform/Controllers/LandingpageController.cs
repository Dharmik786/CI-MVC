using CI_Entity.Models;
using Microsoft.AspNetCore.Mvc;

namespace CI_Platform.Controllers
{
    public class LandingpageController : Controller
    {
        private readonly CIDbContext _CIDbContext;

        public LandingpageController(CIDbContext CIDbContext)
        {
            _CIDbContext = CIDbContext;
        }

        public IActionResult landingpage(long id)

        {
            int? userid = HttpContext.Session.GetInt32("userID");
            if (userid == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        public IActionResult test()
        {
            List<Mission> mission = _CIDbContext.Missions.ToList();
            return View(mission);
        }
    }
}
