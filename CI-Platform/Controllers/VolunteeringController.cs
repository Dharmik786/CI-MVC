using CI.Models;
using CI_Entity.Models;
using CI_Entity.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.Controllers
{
    public class VolunteeringController : Controller
    {
        private readonly CIDbContext _CIDbContext;
        private readonly object JsonRequestBehavior;

        public VolunteeringController(CIDbContext CIDbContext)
        {
            _CIDbContext = CIDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Addrating(string rating, long Id, long missionId)
        {
            MissionRating ratingExists = await _CIDbContext.MissionRatings.FirstOrDefaultAsync(fm => fm.UserId == Id && fm.MissionId == missionId);
            if (ratingExists != null)
            {
                ratingExists.Rating = rating;
                _CIDbContext.Update(ratingExists);
                _CIDbContext.SaveChanges();
                return Json(new { success = true, ratingExists, isRated = true });
            }
            else
            {
                var ratingele = new MissionRating();
                ratingele.Rating = rating;
                ratingele.UserId = Id;
                ratingele.MissionId = missionId;
                _CIDbContext.Add(ratingele);
                _CIDbContext.SaveChanges();
                return Json(new { success = true, ratingele, isRated = true });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Addfav(long Id, long missionId)
        {
            FavoriteMission fav = await _CIDbContext.FavoriteMissions.FirstOrDefaultAsync(m => m.UserId == Id && m.MissionId == missionId);
            if (fav != null)
            {
                _CIDbContext.Remove(fav);
                _CIDbContext.SaveChanges();
                return Json(new { success = true, favmission = "1" });
            }
            else
            {
                var ratingele = new FavoriteMission();
                 
                ratingele.UserId = Id;
                ratingele.MissionId = missionId;
                _CIDbContext.AddAsync(ratingele);
                _CIDbContext.SaveChanges();
                return Json(new { success = true, favmission = "0" });
            }
        }


        public IActionResult Volunteering(long id , int missionid)
        {
            //List<MissionList> missionLists = new List<MissionList>();
            //missionLists.countries = _CIDbContext.Cities.ToList();


            var userId = HttpContext.Session.GetString("user");
            ViewBag.UserId = int.Parse(userId);
            //List<Country> country = _CIDbContext.Countries.ToList();
            //List<City> city = _CIDbContext.Cities.ToList();
            //List<MissionTheme> theme = _CIDbContext.MissionThemes.ToList();
            //List<GoalMission> goalMissions = _CIDbContext.GoalMissions.ToList();

            //ViewBag.Goalmission = goalMissions;

            //var mission = _CIDbContext.Missions.FirstOrDefault(m => m.MissionId == id);
            //ViewBag.mission = mission;

            //var city1 = _CIDbContext.Cities.FirstOrDefault(m => m.CityId == mission.CityId);
            //ViewBag.City1 = city1;

            //var theme1 = _CIDbContext.MissionThemes.FirstOrDefault(m => m.MissionThemeId == mission.ThemeId);
            //ViewBag.Theme1 = theme1;

            //var goal1 = _CIDbContext.GoalMissions.FirstOrDefault(m => m.MissionId == mission.MissionId);
            //ViewBag.Goal1 = goal1;

            //var relatedMission = _CIDbContext.Missions.Where(m => m.ThemeId == mission.ThemeId && m.MissionId != mission.MissionId).ToList();
            //ViewBag.relatedMission = relatedMission.Take(3);

            //return View();


            List<VolunteeringVM> relatedlist = new List<VolunteeringVM>();

            var VolMission = _CIDbContext.Missions.FirstOrDefault(m => m.MissionId == missionid);
            var theme = _CIDbContext.MissionThemes.FirstOrDefault(m => m.MissionThemeId == VolMission.ThemeId);
            var City = _CIDbContext.Cities.FirstOrDefault(m => m.CityId == VolMission.CityId);
            var themeobjective = _CIDbContext.GoalMissions.FirstOrDefault(m => m.MissionId == missionid);

            string[] Startdate = VolMission.StartDate.ToString().Split(" ");
            string[] Enddate = VolMission.EndDate.ToString().Split(" ");

            //Voluntrring mission
            VolunteeringVM volunteeringVM = new VolunteeringVM();

            volunteeringVM.MissionId = missionid;
            volunteeringVM.Title = VolMission.Title;
            volunteeringVM.ShortDescription = VolMission.ShortDescription;
            volunteeringVM.OrganizationName = VolMission.OrganizationName;
            volunteeringVM.Description = VolMission.Description;
            volunteeringVM.OrganizationDetail = VolMission.OrganizationDetail;
            volunteeringVM.Availability = VolMission.Availability;
            volunteeringVM.MissionType = VolMission.MissionType;
            volunteeringVM.Cityname = City.Name;
            volunteeringVM.Themename = theme.Title;
            volunteeringVM.EndDate = Enddate[0];
            volunteeringVM.StartDate = Startdate[0];
            volunteeringVM.GoalObjectiveText = themeobjective.GoalObjectiveText;

            ViewBag.Missiondetail = volunteeringVM;

            //Related Mission
            var relatedmission = _CIDbContext.Missions.Where(m => m.ThemeId == VolMission.ThemeId && m.MissionId != missionid).ToList();
            foreach (var item in relatedmission.Take(3))
            {
                var relcity = _CIDbContext.Cities.FirstOrDefault(m => m.CityId == item.CityId);
                var reltheme = _CIDbContext.MissionThemes.FirstOrDefault(m => m.MissionThemeId == item.ThemeId);
                var relgoalobj = _CIDbContext.GoalMissions.FirstOrDefault(m => m.MissionId == item.MissionId);
                string[] Startdate1 = item.StartDate.ToString().Split(" ");
                string[] Enddate2 = item.EndDate.ToString().Split(" ");

                relatedlist.Add(new VolunteeringVM
                {
                    MissionId = item.MissionId,
                    Cityname = relcity.Name,
                    Themename = reltheme.Title,
                    Title = item.Title,
                    ShortDescription = item.ShortDescription,
                    StartDate = Startdate1[0],
                    EndDate = Enddate2[0],
                    Availability = item.Availability,
                    OrganizationName = item.OrganizationName,
                    GoalObjectiveText = relgoalobj.GoalObjectiveText,
                    MissionType = item.MissionType,


                });
            }

            ViewBag.relatedmission = relatedlist.Take(3);

            //List<VolunteeringVM> recentvolunteredlist = new List<VolunteeringVM>();

            //var resentV = _CIDbContext.MissionApplications.FirstOrDefault(m => m.MissionId == missionid);
            //var uname = _CIDbContext.Users.FirstOrDefault(m=>m.UserId == resentV.UserId);
            //ViewBag.resentV = uname;
            ////volunteeringVM.username = uname.FirstName;
            //volunteeringVM.username= uname.FirstName;
        


            List<VolunteeringVM> recentvolunteredlist = new List<VolunteeringVM>();
            //var recentvolunttered = from U in CID.Users join MA in CiMainContext.MissionApplications on U.UserId equals MA.UserId where MA.MissionId == mission.MissionId select U;
            var recentvoluntered = from U in _CIDbContext.Users join MA in _CIDbContext.MissionApplications on U.UserId equals MA.UserId where MA.MissionId == missionid select U;
            foreach (var item in recentvoluntered)
            {
                recentvolunteredlist.Add(new VolunteeringVM
                {
                    username = item.FirstName,
                });

            }
            ViewBag.recentvolunteered = recentvolunteredlist;
            return View();
        }

    }
}
