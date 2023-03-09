using CI_Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq;

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
        public IActionResult landingpage(long id,int? pageIndex,string searchQuery, int sortOrder )
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


            //Sort By
            // ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.Order = sortOrder;
            switch(sortOrder)
            {
                case 1:
                    mission = (List<Mission>)mission.OrderBy(mission => mission.Title).ToList();
                    break;

                case 2:
                    mission = (List<Mission>)mission.OrderBy(mission => mission.StartDate).ToList();
                    break;

                case 3:
                    mission = (List<Mission>)mission.OrderBy(mission => mission.MissionType).ToList();
                    break;
            }




            //foreach (var item in mission)
            //{
            //    var City = _CIDbContext.Cities.FirstOrDefault(u => u.CityId == item.CityId);
            //    var Theme = _CIDbContext.MissionThemes.FirstOrDefault(u => u.MissionThemeId == item.ThemeId);
            //}

            //if (searchQuery != null)
            //{
            //    mission = _CIDbContext.Missions.Where(m => m.Title.Contains(searchQuery)).ToList();
            //    ViewBag.InputSearch = searchQuery;

            //    if (mission.Count() == 0)
            //    {
            //        return RedirectToAction("NoMissionFound", "Home");
            //    }
            //}

            if (!string.IsNullOrEmpty(searchQuery))
            {
                mission = mission.Where(m => m.Title.ToUpper().Contains(searchQuery.ToUpper())).ToList();
                ViewBag.searchQuery = Request.Query["searchQuery"];
                if (mission.Count() == 0)
                {
                    return RedirectToAction("NoMissionFound", "Home");
                }
            }

            int pageSize = 9;
            int skip = (pageIndex ?? 0) * pageSize;
            var Missions = mission.Skip(skip).Take(pageSize).ToList();
            int totalMissions = mission.Count();

            ViewBag.TotalMission = totalMissions;
            ViewBag.TotalPages = (int)Math.Ceiling(totalMissions / (double)pageSize);
            ViewBag.CurrentPage = pageIndex ?? 0;

            return View(Missions);
            
        }

        //private JsonResult Json(string v, object allowGet)
        //{
        //    throw new NotImplementedException();
        //}

        //[HttpPost]
        //public JsonResult SaveList(string ItemList)
        //{
        //    return Json("", JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult SaveList(string ItemList)
        //{
        //    string[] arr = ItemList.Split(',');

        //    foreach (var id in arr)
        //    {
        //        var currentId = id;
        //    }

        //    return Json("", JsonRequestBehavior);
        //}

     
    }
}
