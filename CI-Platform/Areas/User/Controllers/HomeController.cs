using CI_Entity.Models;
using CI_Entity.ViewModel;
using CI_Platform.Models;
using CI_PlatForm.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace CI_Platform.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {


        private readonly CIDbContext _CIDbContext;
        private readonly IUserInterface _IUser;
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;

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
            if (userId == null)
            {
                return RedirectToAction("Login", "User", new { area = "User" });
            }

            MissionList m = new MissionList();
            m.users = _IUser.user();
            m.mission = _IUser.mission();
            m.missionApplications = _IUser.missionApplications().Where(u => u.UserId == Convert.ToInt32(userId)).ToList();
            m.timesheets = _IUser.timesheets().Where(U => U.UserId == Convert.ToInt64(userId)).ToList();
            return View(m);
        }

        [HttpPost]
        public IActionResult AddTimeSheet(MissionList model)
            {
            var userId = HttpContext.Session.GetString("user");
            _IUser.AddTime(model.missionId, Convert.ToInt32(userId), model.hour, model.min, model.action, model.date, model.notes, model.Hidden);
            return RedirectToAction("VolunteeringTimeSheet", "Home");
        }

        public IActionResult DeleteTimeSheet(int id)
        {
            _IUser.DeleteTimeSheet(id);
            return RedirectToAction("VolunteeringTimeSheet", "Home");
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
            var userId = HttpContext.Session.GetString("user");
            if (userId == null)
            {
                return RedirectToAction("Login", "User", new { Areas = "User" });
            }
            UserProfile u = new UserProfile();
            u.user = _IUser.GetUserByUserId(Convert.ToInt32(userId));
            u.cities = _IUser.cities();
            u.countries = _IUser.countries();
            u.skills = _IUser.GetAllskill();

            u.Userskills = _IUser.GetUserSkill(Convert.ToInt32(userId));

            var r = from row1 in u.skills.AsEnumerable()
                    where !u.Userskills.AsEnumerable().Select(r => r.SkillId).Contains(row1.SkillId)
                    select row1;

            u.RemainingSkill = r.ToList();

            var allskills = _IUser.GetAllskill();
            ViewBag.allskills = allskills;
            var skills = from US in _CIDbContext.UserSkills
                         join S in _CIDbContext.Skills on US.SkillId equals S.SkillId
                         select new { US.SkillId, S.SkillName, US.UserId };
            var uskills = skills.Where(e => e.UserId == Convert.ToInt32(userId)).ToList();
            ViewBag.userskills = uskills;
            foreach (var skill in uskills)
            {
                var rskill = allskills.FirstOrDefault(e => e.SkillId == skill.SkillId);
                allskills.Remove(rskill);
            }
            ViewBag.remainingSkills = allskills;

            List<UserSkill> us = _IUser.GetUserSkill(Convert.ToInt32(userId));
            List<Skill> s = _IUser.GetAllskill();




            var user = _IUser.GetUserByUserId(Convert.ToInt32(userId));
            u.FirstName = user.FirstName;
            u.LastName = user.LastName;
            u.EmployeeId = user.EmployeeId;
            u.Title = user.Title;
            u.Department = user.Department;
            u.MyProfile = user.ProfileText;
            u.WhyIVol = user.WhyIVolunteer;
            u.Email = user.Email;
            u.Name = user.FirstName + " " + user.LastName;

            if (user.CountryId != null && user.CityId != null)
            {
                u.Country = (int)user.CountryId;
                u.City = (int)user.CityId;
            }

            u.Availablity = user.Availablity;
            u.LinkedIn = user.LinkedInUrl;

            return View(u);
        }

        [HttpPost]
        public async Task<IActionResult> UserProfile(UserProfile model)
        {
            var userId = HttpContext.Session.GetString("user");
            UserProfile u = new UserProfile();
            u.user = _IUser.GetUserByUserId(Convert.ToInt32(userId));

            var user = _IUser.GetUserByUserId(Convert.ToInt32(userId));

            if (model.Avatar != null)
            {
                var FileName = "";
                using (var ms = new MemoryStream())
                {
                    await model.Avatar.CopyToAsync(ms);
                    var imageBytes = ms.ToArray();
                    var base64String = Convert.ToBase64String(imageBytes);
                    FileName = "data:image/png;base64," + base64String;
                }
                user.Avatar = FileName;
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.EmployeeId = model.EmployeeId;
            user.Title = model.Title;
            user.Department = model.Department;
            user.ProfileText = model.MyProfile;
            user.WhyIVolunteer = model.WhyIVol;
            user.CountryId = model.Country;
            user.CityId = model.City;
            user.Availablity = model.Availablity;
            user.LinkedInUrl = model.LinkedIn;
            user.UpdatedAt = DateTime.Now;
            _CIDbContext.Update(user);
            _CIDbContext.SaveChanges();


            return RedirectToAction("UserProfile", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> SaveUserSkills(long[] selectedSkills)
        {
            var userid = HttpContext.Session.GetString("user");
            long id = Convert.ToInt64(userid);
            var abc = _CIDbContext.UserSkills.Where(e => e.UserId == id).ToList();
            _CIDbContext.RemoveRange(abc);
            _CIDbContext.SaveChanges();
            foreach (var skills in selectedSkills)
            {
                _IUser.AddUserSkills(skills, Convert.ToInt32(userid));
            }
            return RedirectToAction("UserProfile", "Home");
        }

        [HttpPost]
        public bool ChangePassword(string oldPsw, string NewPsw, string CnfPsw)
        {
            var userId = HttpContext.Session.GetString("user");

            UserProfile u = new UserProfile();
            u.user = _IUser.GetUserByUserId(Convert.ToInt32(userId));

            if (oldPsw != u.user.Password)
            {
                return false;
            }
            else
            {
                _IUser.ChangePassword(NewPsw, CnfPsw, Convert.ToInt32(userId));
                return true;
            }


        }

        [HttpPost]
        public IActionResult ContactUs(UserProfile model)
        {
            _IUser.ContactUs(model);
            return RedirectToAction("UserProfile", "Home");
        }






        public IActionResult Policy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}