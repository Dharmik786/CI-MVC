using CI_Entities1.Models.ViewModel;
using CI_Entity.Models;
using CI_Entity.ViewModel;
using CI_PlatForm.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NuGet.Packaging;
using System;
using System.Linq;
using System.Reflection;

namespace CI_Platform.Areas.User.Controllers
{
    [Area("User")]
    public class LandingpageController : Controller
    {
        private readonly CIDbContext _CIDbContext;
        private readonly object JsonRequestBehavior;
        private readonly IUserInterface _IUser;

        public LandingpageController(CIDbContext CIDbContext, IUserInterface IUser)
        {
            _CIDbContext = CIDbContext;
            _IUser = IUser;
        }

        public IActionResult landingpage(long userId, int? pageIndex, string search, string searchQuery, string sortOrder, long[] ACountries, long[] ACity, string countryId)
        {
            // int? userid = HttpContext.Session.GetInt32("userID");
            //if (userid == null)
            //{
            //    return RedirectToAction("Login", "Home");
            //}

            MissionList missionList = new MissionList();
            missionList.mission = _IUser.mission();
            missionList.cities = _IUser.cities();
            missionList.countries = _IUser.countries();
            missionList.userId = userId;
            missionList.missionThemes = _IUser.missionThemes();
            missionList.goalMissions = _IUser.goalMissions();
            missionList.missionApplications = _IUser.missionApplications();
            missionList.userId = Convert.ToInt32(userId);
            missionList.missionMedia = _IUser.missionMedia();
            missionList.skills = _IUser.skills();

            List<City> city = _IUser.cities();
            ViewBag.City = city;

            List<Country> country = _IUser.countries();
            ViewBag.Country = country;

            List<MissionTheme> themes = _IUser.missionThemes();
            ViewBag.Themes = themes;

            List<GoalMission> goalMissions = _IUser.goalMissions();
            ViewBag.GoalMissions = goalMissions;

            List<Skill> skills = _IUser.GetAllskill();
            ViewBag.skills = skills;

            missionList.favoriteMissions = _IUser.favoriteMissions();

            return View(missionList);

        }

        public IActionResult _Missions(long userId, int missionid, string? search, int? pageIndex, string? sortValue, string[] country, string[] city, string[] theme, string[] skill, int jpg)
        {
            var id = HttpContext.Session.GetString("user");

            MissionList missionList = new MissionList();
            missionList.mission = _IUser.mission();
            missionList.cities = _IUser.cities();
            missionList.countries = _IUser.countries();
            missionList.missionThemes = _IUser.missionThemes();
            missionList.goalMissions = _IUser.goalMissions();
            missionList.users = _IUser.user();
            missionList.timesheets = _IUser.timesheets();
            missionList.goal = _IUser.goalMissions();
            missionList.missionSkills = _CIDbContext.MissionSkills.ToList();
            missionList.skills = _IUser.skills();
            //missionList.userId = userId;
            List<Mission> mission = _IUser.mission().ToList();





            missionList.missionApplications = _IUser.missionApplications();
            missionList.userId = Convert.ToInt32(userId);

            missionList.favoriteMissions = _IUser.favoriteMissions();
            missionList.missionRatings = _IUser.MissionRatings();
            missionList.missionMedia = _IUser.missionMedia();

            var msn = _IUser.mission().ToList();




            //Seacrh
            if (search != null)
            {
                mission = mission.Where(m => m.Title.ToUpper().Contains(search.ToUpper())).ToList();
                if (mission.Count() == 0)
                {
                    return PartialView("_NoMission");
                }
            }

            ////Sort By

            if (sortValue != null)
            {
                ViewBag.sort = sortValue;
            }

            switch (sortValue)
            {
                case "Newest":
                    mission = mission.OrderByDescending(mission => mission.StartDate).ToList();
                    break;

                case "Oldest":
                    mission = mission.OrderBy(mission => mission.StartDate).ToList();
                    break;

                case "Lowest Seats":
                    mission = mission.OrderByDescending(mission => mission.Availability).ToList();
                    break;

                case "Highest Seats":
                    mission = mission.OrderBy(mission => mission.Availability).ToList();
                    break;

                case "My Favourites":
                    var favmissions = _CIDbContext.FavoriteMissions.Where(m => m.UserId.ToString() == id).ToList();
                    mission = mission.OrderByDescending(x=>favmissions.Any(m=>m.MissionId == x.MissionId)).ToList();
                    break;

                case "Registration deadline":
                    mission = mission.OrderBy(mission => mission.MissionType).ToList();
                    break;

                default:
                    mission = mission.ToList();
                    break;
            }
            var s = from M in _CIDbContext.MissionSkills
                    join S in _CIDbContext.Skills
                    on M.SkillId equals S.SkillId
                    select M;



            //filter
            if (country.Length > 0 || city.Length > 0 || theme.Length > 0 || skill.Length > 0)
            {

                if (country.Length > 0)
                {
                    mission = mission.Where(s => country.Contains(s.Country.Name)).ToList();
                }
                if (city.Length > 0)
                {
                    mission = mission.Where(s => city.Contains(s.City.Name)).ToList();
                }
                if (theme.Length > 0)
                {
                    mission = mission.Where(s => theme.Contains(s.Theme.Title)).ToList();
                }
                if (skill.Length > 0)
                {
                    var tempFill = missionList.missionSkills.Where(x => skill.Contains(x.Skill.SkillName)).Select(x => x.MissionId).ToList();
                    mission = mission.Where(e => tempFill.Contains(e.MissionId)).ToList();

                }
                if (mission.Count() == 0)
                {
                    return PartialView("_NoMission");
                }
            }


            missionList.mission = mission;

            ViewBag.missionCount = missionList.mission.Count();
            const int pageSize = 3;
            if (jpg < 1)
            {
                jpg = 1;
            }
            int recsCount = missionList.mission.Count();
            var pager = new Pager(recsCount, jpg, pageSize);
            int recSkip = (jpg - 1) * pageSize;
            var data = missionList.mission.Skip(recSkip).Take(pager.PageSize).ToList();
            ViewBag.pager = pager;
            ViewBag.missionTempDate = data;
            missionList.mission = data.ToList();

            ViewBag.TM = recsCount;

            return PartialView("_Missions", missionList);

        }

    }
}
