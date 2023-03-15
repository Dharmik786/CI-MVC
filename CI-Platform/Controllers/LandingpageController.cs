using CI_Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NuGet.Packaging;
using System.Linq;
using System.Reflection;

namespace CI_Platform.Controllers
{
    public class LandingpageController : Controller
    {
        int i = 0;
        int i1 = 0;
        int j = 0;
        int j1 = 0;
        int k = 0;
        int k1 = 0;

        private readonly CIDbContext _CIDbContext;
        private readonly object JsonRequestBehavior;

        public LandingpageController(CIDbContext CIDbContext)
        {
            _CIDbContext = CIDbContext;
        }

        public IActionResult landingpage(long id, int? pageIndex, string search,string searchQuery, string sortOrder, long[] ACountries, long[] ACity, string countryId)
        {
            int? userid = HttpContext.Session.GetInt32("userID");
            if (userid == null)
            {
                return RedirectToAction("Login", "Home");
            }


            List<Mission> mission = _CIDbContext.Missions.ToList();

            List<Mission> finalmission = _CIDbContext.Missions.ToList();
            List<Mission> newmission = _CIDbContext.Missions.ToList();

            List<City> city = _CIDbContext.Cities.ToList();
            ViewBag.City = city;

            List<Country> country = _CIDbContext.Countries.ToList();
            ViewBag.Country = country;

            List<MissionTheme> themes = _CIDbContext.MissionThemes.ToList();
            ViewBag.Themes = themes;

            List<GoalMission> goalMissions = _CIDbContext.GoalMissions.ToList();
            ViewBag.GoalMissions = goalMissions;

            List<MissionRating> rate = _CIDbContext.MissionRatings.ToList();
            ViewBag.Rate = rate;

          
            foreach (var item in mission)
            {
                var City = _CIDbContext.Cities.FirstOrDefault(u => u.CityId == item.CityId);
                var Theme = _CIDbContext.MissionThemes.FirstOrDefault(u => u.MissionThemeId == item.ThemeId);
            }

            //Rating
         

            //var rat = _CIDbContext.Mission


            //Filter
            if (ACountries != null && ACountries.Length > 0)
            {
                foreach (var country1 in ACountries)
                {
                    if (i == 0)
                    {
                        mission = mission.Where(m => m.CountryId == country1 + 500).ToList();
                        i++;
                    }
                    finalmission = newmission.Where(m => m.CountryId == country1).ToList();
                    mission.AddRange(finalmission);
                    if (mission.Count() == 0)
                    {
                        return RedirectToAction("NoMissionFound", "Home");
                    }
                    ViewBag.countryId = country1;
                    if (ViewBag.countryId != null)
                    {
                        var countryElement = _CIDbContext.Countries.Where(m => m.CountryId == country1).ToList();
                        if (i1 == 0)
                        {
                            country = _CIDbContext.Countries.Where(m => m.CountryId == country1 + 50000).ToList();
                            i1++;
                        }
                        country.AddRange(countryElement);
                    }
                }
                ViewBag.country1 = country;
            }

            if (ACity != null && ACity.Length > 0)
            {

                foreach (var city1 in ACity)
                {
                    //mission = mission.Where(m => m.CountryId == country).ToList();
                    if (i == 0)
                    {
                        mission = mission.Where(m => m.CityId == city1 + 500).ToList();
                        i++;
                    }

                    finalmission = newmission.Where(m => m.CityId == city1).ToList();

                    mission.AddRange(finalmission);
                    if (mission.Count() == 0)
                    {
                        return RedirectToAction("NoMissionFound", "Home");
                    }

                    ViewBag.cityId = city1;
                    if (ViewBag.cityId != null)
                    {
                        var cityElement = _CIDbContext.Cities.Where(m => m.CityId == city1).ToList();
                        if (i1 == 0)
                        {
                            city = _CIDbContext.Cities.Where(m => m.CityId == city1 + 50000).ToList();
                            i1++;
                        }
                        city.AddRange(cityElement);
                        //var c1 = _CIDbContext.Countries.FirstOrDefault(m => m.CountryId == country);
                        //ViewBag.country = c1.Name;
                    }
                }
                ViewBag.city1 = city;
                //Countries = _CIDbContext.Countries.ToList();


            }

      

            //Sort By
            // ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.Order = sortOrder;
            switch (sortOrder)
            {
                case "Newest":
                    mission = (List<Mission>)mission.OrderByDescending(mission => mission.StartDate).ToList();
                    break;

                case "Oldest":
                    mission = (List<Mission>)mission.OrderBy(mission => mission.StartDate).ToList();
                    break;

                case "Theme":
                    mission = (List<Mission>)mission.OrderBy(mission => mission.MissionType).ToList();
                    break;
            }

            //Search
            if (search != null)
            {
                mission = _CIDbContext.Missions.Where(m => m.Title.Contains(search)).ToList();
            }

            //if (searchQuery != null)
            //{
            //    mission = _CIDbContext.Missions.Where(m => m.Title.Contains(searchQuery)).ToList();
            //    ViewBag.InputSearch = searchQuery;

            //    if (mission.Count() == 0)
            //    {
            //        return RedirectToAction("NoMissionFound", "Home");
            //    }
            //}

            //Pagination
            int pageSize = 9;
            int skip = (pageIndex ?? 0) * pageSize;
            var Missions = mission.Skip(skip).Take(pageSize).ToList();
            int totalMissions = mission.Count();

            ViewBag.TotalMission = totalMissions;
            ViewBag.TotalPages = (int)Math.Ceiling(totalMissions / (double)pageSize);
            ViewBag.CurrentPage = pageIndex ?? 0;

            return View(Missions);

        }

        //public IActionResult search(string search)
        //{
        //    List<Mission> mission = _CIDbContext.Missions.ToList();
        //    if (search != null)
        //    {
        //        mission = _CIDbContext.Missions.Where(m => m.Title.Contains(search)).ToList();

        //    }
        //    return View(mission);
        //}

    }
}
