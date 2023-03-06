using CI_Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CI_Platform.Controllers
{
    public class LandingpageController : Controller
    {
        private readonly CIDbContext _CIDbContext;

        public LandingpageController(CIDbContext CIDbContext)
        {
            _CIDbContext = CIDbContext;
        }

        public IActionResult landingpage(long id,int? pageIndex,string inputSearch)

        {
            int? userid = HttpContext.Session.GetInt32("userID");
            if (userid == null)
            {
                return RedirectToAction("Login", "Home");
            }

            List<Mission> mission = _CIDbContext.Missions.ToList();

            List<City> city = _CIDbContext.Cities.ToList();
            ViewBag.City = city;

            List<Country> country = _CIDbContext.Countries.ToList();
            ViewBag.Country = country;

            List<MissionTheme> themes = _CIDbContext.MissionThemes.ToList();
            ViewBag.Themes = themes;    

            foreach (var item in mission)
            {
                var City = _CIDbContext.Cities.FirstOrDefault(u => u.CityId == item.CityId);
                var Theme = _CIDbContext.MissionThemes.FirstOrDefault(u => u.MissionThemeId == item.ThemeId);
            }

            if(inputSearch != null)
            {
                mission = _CIDbContext.Missions.Where(m => m.Title.Contains(inputSearch)).ToList();
                ViewBag.InputSearch = inputSearch;

                if (mission.Count() == 0)
                {
                    return RedirectToAction("NoMissionFound", "Home");
                }
            }

            int pageSize = 6;
            int skip = (pageIndex ?? 0) * pageSize;
            var Missions = mission.Skip(skip).Take(pageSize).ToList();
            int totalMissions = mission.Count();
            ViewBag.TotalMission = totalMissions;
            ViewBag.TotalPages = (int)Math.Ceiling(totalMissions / (double)pageSize);
            ViewBag.CurrentPage = pageIndex ?? 0;

            return View(Missions);
            
        }

        //public IActionResult landingpage()
        //{
        //    List<Mission> mission = _CIDbContext.Missions.ToList();
        //    foreach (var item in mission)
        //    {                
        //        var City = _CIDbContext.Cities.FirstOrDefault(u => u.CityId == item.CityId);
        //        var Theme = _CIDbContext.MissionThemes.FirstOrDefault(u => u.MissionThemeId == item.ThemeId);
        //    }
        //    return View(mission);
        //}
    }
}
