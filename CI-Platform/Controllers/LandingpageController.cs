using CI_Entity.Models;
using CI_Entity.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NuGet.Packaging;
using System;
using System.Linq;
using System.Reflection;

namespace CI_Platform.Controllers
{
    public class LandingpageController : Controller
    {
        private readonly CIDbContext _CIDbContext;
        private readonly object JsonRequestBehavior;

        public LandingpageController(CIDbContext CIDbContext)
        {
            _CIDbContext = CIDbContext;
        }

        public IActionResult landingpage(long userId, int? pageIndex, string search, string searchQuery, string sortOrder, long[] ACountries, long[] ACity, string countryId)
            {
           // int? userid = HttpContext.Session.GetInt32("userID");
            //if (userid == null)
            //{
            //    return RedirectToAction("Login", "Home");
            //}

            MissionList missionList = new MissionList();
            missionList.mission = _CIDbContext.Missions.ToList();
            missionList.cities = _CIDbContext.Cities.ToList();
            missionList.countries = _CIDbContext.Countries.ToList();
            missionList.userId = userId;
            missionList.missionThemes = _CIDbContext.MissionThemes.ToList();
            missionList.goalMissions = _CIDbContext.GoalMissions.ToList();

            List<City> city = _CIDbContext.Cities.ToList();
            ViewBag.City = city;

            List<Country> country = _CIDbContext.Countries.ToList();
            ViewBag.Country = country;

            List<MissionTheme> themes = _CIDbContext.MissionThemes.ToList();
            ViewBag.Themes = themes;

            List<GoalMission> goalMissions = _CIDbContext.GoalMissions.ToList();
            ViewBag.GoalMissions = goalMissions;

            missionList.favoriteMissions = _CIDbContext.FavoriteMissions.ToList();
            //Pagination
            //int pageSize = 9;
            //int skip = (pageIndex ?? 0) * pageSize;
            //var Missions = missionList.mission.Skip(skip).Take(pageSize).ToList();
            //int totalMissions = missionList.mission.Count();
            //missionList.mission = Missions
            //ViewBag.TotalMission = totalMissions;
            //ViewBag.TotalPages = (int)Math.Ceiling(totalMissions / (double)pageSize);
            //ViewBag.CurrentPage = pageIndex ?? 0;

            return View(missionList);

        }

        public IActionResult _Missions(long userId,int missionid,string? search, int? pageIndex, string? sortValue, string[] country, string[] city, string[] theme)
        {
            var id = HttpContext.Session.GetString("user");

            MissionList missionList = new MissionList();
            missionList.mission = _CIDbContext.Missions.ToList();
            missionList.cities = _CIDbContext.Cities.ToList();
            missionList.countries = _CIDbContext.Countries.ToList();
            missionList.missionThemes = _CIDbContext.MissionThemes.ToList();
            missionList.goalMissions = _CIDbContext.GoalMissions.ToList();
            missionList.users = _CIDbContext.Users.ToList();
            //missionList.userId = userId;
            List<Mission> mission = _CIDbContext.Missions.ToList();

            missionList.favoriteMissions = _CIDbContext.FavoriteMissions.ToList();
            missionList.missionRatings=_CIDbContext.MissionRatings.ToList();
            //Avg Rating
            //int avgRating = 0;
            //int rat = 0;
            //var ratingList = missionList.missionRatings.Where(m => m.MissionId == missionList.singleMission.MissionId).ToList();

            //if (ratingList.Count() > 0)
            //{

            //    foreach (var r in ratingList)
            //    {
            //        rat = rat + int.Parse(r.Rating);
            //    }
            //    avgRating = rat / ratingList.Count();
            //}



            //Seacrh
            if (search != null)
            {
                mission = mission.Where(m => m.Title.Contains(search)).ToList();
                if (mission.Count() == 0)
                {
                    return PartialView("_NoMission");
                }
            }

            ////Sort By
            ViewBag.sort = sortValue;
            switch (sortValue)
            {
                case "Newest":
                    mission = mission.OrderByDescending(mission => mission.StartDate).ToList();
                    break;

                case "Oldest":
                    mission = mission.OrderBy(mission => mission.StartDate).ToList();
                    break;

                case "Theme":
                    mission = mission.OrderBy(mission => mission.MissionType).ToList();
                    break;
                default:
                    mission = mission.ToList();
                    break;
            }

            //filter
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

            //Pagination
            int pageSize = 6;
            int skip = (pageIndex ?? 0) * pageSize;
            var Missions = mission.Skip(skip).Take(pageSize).ToList();
            int totalMissions = mission.Count();

            ViewBag.TotalMission = totalMissions;
            ViewBag.TotalPages = (int)Math.Ceiling(totalMissions / (double)pageSize);
            ViewBag.CurrentPage = pageIndex ?? 0;

            missionList.mission = mission;

            return PartialView("_Missions", missionList);
        }

    }
}
