using CI.Models;
using CI_Entity.Models;
using CI_Entity.ViewModel;
using CI_Platform.Models;
using CI_PlatForm.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace CI_Platform.Controllers
{
    public class HomeController : Controller
    {


        private readonly CIDbContext _CIDbContext;
        private readonly IUserInterface _IUser;
        [Obsolete]
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public HomeController(CIDbContext CIDbContext, IUserInterface Iuser, IHostingEnvironment IHostingEnvironment)
        {
            _CIDbContext = CIDbContext;
            _IUser = Iuser;
            _hostingEnvironment = IHostingEnvironment;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult Forget()
        {
            return View();
        }
        public IActionResult Reset_Password()
        {
            return View();
        }
        public IActionResult landingpage()
        {
            return View();
        }
        public IActionResult NoMissionFound()
        {
            return View();
        }
        public IActionResult Volunteering()
        {
            return View();
        }

        public IActionResult VolunteeringTimesheet()
        {
            var userId = HttpContext.Session.GetString("user");

            MissionList m = new MissionList();
            m.users = _IUser.user();
            m.mission = _IUser.mission();
            m.missionApplications = _IUser.missionApplications().Where(u => u.UserId == Convert.ToInt32(userId)).ToList();
            m.timesheets = _IUser.timesheets().Where(U=>U.UserId==Convert.ToInt64(userId)).ToList();
            return View(m);
        }

        [HttpPost]
        public IActionResult AddTimeSheet(MissionList model)
        {
            var userId = HttpContext.Session.GetString("user");
            _IUser.AddTime(model.missionId, Convert.ToInt32(userId),model.hour,model.min,model.action,model.date,model.notes,model.Hidden);
            return RedirectToAction("VolunteeringTimeSheet","Home");
        }

        public IActionResult DeleteTimeSheet(int id)
        {
            _IUser.DeleteTimeSheet(id);
            return RedirectToAction("VolunteeringTimeSheet","Home");
        }
        [HttpPost]
        public async Task<IActionResult> EditTimeTimeSheet(int id)
        {
            var timesheet = _IUser.timesheets().Where(e => e.TimesheetId == id).FirstOrDefault();
            return Json(new { success = true, Timesheet = timesheet });
        }
        
        [HttpPost]
        public async Task<IActionResult> EditGoalTimeSheet(int id)
        {           
            var timesheet = _IUser.timesheets().Where(e => e.TimesheetId == id).FirstOrDefault();
            return Json(new { success = true, Timesheet = timesheet });
        }
      

        public IActionResult UserProfile()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}