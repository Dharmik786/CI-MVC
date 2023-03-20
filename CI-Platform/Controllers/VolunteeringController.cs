using CI.Models;
using CI_Platform.Models;
using CI_Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using CI_Entity.Models.ViewModel;
using System.Security.Claims;
using System.Net.Mail;
using System.Net;

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

        public IActionResult Volunteering(long id, int missionid)
        {
            var userId = HttpContext.Session.GetString("user");
            ViewBag.UserId = int.Parse(userId);


            MissionList vMMission = new MissionList();
            vMMission.mission = _CIDbContext.Missions.ToList();
            vMMission.cities = _CIDbContext.Cities.ToList();
            vMMission.countries = _CIDbContext.Countries.ToList();
            vMMission.missionThemes = _CIDbContext.MissionThemes.ToList();
            vMMission.skills = _CIDbContext.Skills.ToList();
            vMMission.missionMedia = _CIDbContext.MissionMedia.ToList();
            vMMission.missionRatings = _CIDbContext.MissionRatings.ToList();
            vMMission.goalMissions = _CIDbContext.GoalMissions.ToList();
            vMMission.users = _CIDbContext.Users.ToList();
            vMMission.timesheets = _CIDbContext.Timesheets.ToList();
            vMMission.comments = _CIDbContext.Comments.ToList();
            

            vMMission.favoriteMissions = _CIDbContext.FavoriteMissions.Where(e => e.UserId == Convert.ToInt32(userId)).ToList();
            var data = vMMission.mission.Where(e => e.MissionId == missionid).FirstOrDefault();
            vMMission.singleMission = data;

            vMMission.relatedMission = _CIDbContext.Missions.Where(e => (e.ThemeId == data.ThemeId) && (e.MissionId != missionid)).ToList();



            int avgRating = 0;
            int rat = 0;
            var ratingList = vMMission.missionRatings.Where(m => m.MissionId == vMMission.singleMission.MissionId).ToList();

            if (ratingList.Count() > 0)
            {
                
                foreach (var r in ratingList)
                {
                    rat = rat + int.Parse(r.Rating);
                }
                avgRating = rat / ratingList.Count();
            }
            ViewBag.rat = ratingList.Count();

            vMMission.avgrating = avgRating;
           
            




            //vMMission.relatedMission = _dbCiPlatform.Missions.Where(e => (e.ThemeId == data.ThemeId) || (e.CityId == data.CityId)).ToList();

            //List<VolunteeringVM> relatedlist = new List<VolunteeringVM>();

            //var VolMission = _CIDbContext.Missions.FirstOrDefault(m => m.MissionId == missionid);
            //var theme = _CIDbContext.MissionThemes.FirstOrDefault(m => m.MissionThemeId == VolMission.ThemeId);
            //var City = _CIDbContext.Cities.FirstOrDefault(m => m.CityId == VolMission.CityId);
            //var themeobjective = _CIDbContext.GoalMissions.FirstOrDefault(m => m.MissionId == missionid);

            //string[] Startdate = VolMission.StartDate.ToString().Split(" ");
            //string[] Enddate = VolMission.EndDate.ToString().Split(" ");

            ////Voluntrring mission
            //VolunteeringVM volunteeringVM = new VolunteeringVM();

            //volunteeringVM.MissionId = missionid;
            //volunteeringVM.Title = VolMission.Title;
            //volunteeringVM.ShortDescription = VolMission.ShortDescription;
            //volunteeringVM.OrganizationName = VolMission.OrganizationName;
            //volunteeringVM.Description = VolMission.Description;
            //volunteeringVM.OrganizationDetail = VolMission.OrganizationDetail;
            //volunteeringVM.Availability = VolMission.Availability;
            //volunteeringVM.MissionType = VolMission.MissionType;
            //volunteeringVM.Cityname = City.Name;
            //volunteeringVM.Themename = theme.Title;
            //volunteeringVM.EndDate = Enddate[0];
            //volunteeringVM.StartDate = Startdate[0];
            //volunteeringVM.GoalObjectiveText = themeobjective.GoalObjectiveText;

            //ViewBag.Missiondetail = volunteeringVM;

            ////Related Mission
            //var relatedmission = _CIDbContext.Missions.Where(m => m.ThemeId == VolMission.ThemeId && m.MissionId != missionid).ToList();
            //foreach (var item in relatedmission.Take(3))
            //{
            //    var relcity = _CIDbContext.Cities.FirstOrDefault(m => m.CityId == item.CityId);
            //    var reltheme = _CIDbContext.MissionThemes.FirstOrDefault(m => m.MissionThemeId == item.ThemeId);
            //    var relgoalobj = _CIDbContext.GoalMissions.FirstOrDefault(m => m.MissionId == item.MissionId);
            //    string[] Startdate1 = item.StartDate.ToString().Split(" ");
            //    string[] Enddate2 = item.EndDate.ToString().Split(" ");

            //    relatedlist.Add(new VolunteeringVM
            //    {
            //        MissionId = item.MissionId,
            //        Cityname = relcity.Name,
            //        Themename = reltheme.Title,
            //        Title = item.Title,
            //        ShortDescription = item.ShortDescription,
            //        StartDate = Startdate1[0],
            //        EndDate = Enddate2[0],
            //        Availability = item.Availability,
            //        OrganizationName = item.OrganizationName,
            //        GoalObjectiveText = relgoalobj.GoalObjectiveText,
            //        MissionType = item.MissionType,


            //    });
            //}

            //ViewBag.relatedmission = relatedlist.Take(3);

            //List<VolunteeringVM> recentvolunteredlist = new List<VolunteeringVM>();

            //var resentV = _CIDbContext.MissionApplications.FirstOrDefault(m => m.MissionId == missionid);
            //var uname = _CIDbContext.Users.FirstOrDefault(m=>m.UserId == resentV.UserId);
            //ViewBag.resentV = uname;
            ////volunteeringVM.username = uname.FirstName;
            //volunteeringVM.username= uname.FirstName;



            //List<VolunteeringVM> recentvolunteredlist = new List<VolunteeringVM>();
            ////var recentvolunttered = from U in CID.Users join MA in CiMainContext.MissionApplications on U.UserId equals MA.UserId where MA.MissionId == mission.MissionId select U;
            //var recentvoluntered = from U in _CIDbContext.Users join MA in _CIDbContext.MissionApplications on U.UserId equals MA.UserId where MA.MissionId == missionid select U;
            //foreach (var item in recentvoluntered)
            //{
            //    recentvolunteredlist.Add(new VolunteeringVM
            //    {
            //        username = item.FirstName,
            //    });

            //}
            //ViewBag.recentvolunteered = recentvolunteredlist;
            return View(vMMission);
        }

        public void addToFavourite(int missonid)
        {
            var userId = HttpContext.Session.GetString("user");
            ViewBag.UserId = int.Parse(userId);

            //add favourite mission data
            if (missonid != null)
            {
                var tempFav = _CIDbContext.FavoriteMissions.Where(e => (e.MissionId == missonid) && (e.UserId == Convert.ToInt32(userId))).FirstOrDefault();
                if (tempFav == null)
                {
                    FavoriteMission fm = new FavoriteMission();
                    fm.UserId = Convert.ToInt32(userId);
                    fm.MissionId = missonid;
                    _CIDbContext.Add(fm);
                }
                else
                {
                    _CIDbContext.Remove(tempFav);
                }
                _CIDbContext.SaveChanges();

            }

        }

        public void PostComment(int missonid, string cmt)
        {
            var userId = HttpContext.Session.GetString("user");
            ViewBag.UserId = int.Parse(userId);

            Comment c = new Comment();
            c.UserId = Convert.ToInt32(userId);
            c.MissionId = missonid;
            c.CommentText = cmt;
            _CIDbContext.Add(c);
            _CIDbContext.SaveChanges(true);

        }

        [HttpPost]
        public void Sendmail(int missionid, long[] emailList)
        {
            //var userId = HttpContext.Session.GetString("user");
            //ViewBag.UserId = int.Parse(userId);

            foreach (var i in emailList)
            {
                var user = _CIDbContext.Users.FirstOrDefault(u => u.UserId == i);

                var missionlink = Url.Action("Volunteering", "Volunteering", new { user = user.UserId, mission = missionid }, Request.Scheme);

                var fromAddress = new MailAddress("officehl1882@gmail.com", "Sender Name");
                var toAddress = new MailAddress(user.Email);
                var subject = "Mission Request";
                var body = $"Hi,<br /><br />This is to <br /><br /><a href='{missionlink}'>{missionlink}</a>";

                var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                var smtpClient = new SmtpClient("smtp.gmail.com", 587)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("officehl1882@gmail.com", "yedkuuhuklkqfzwx"),
                    EnableSsl = true

                };
                smtpClient.Send(message);
            }
        }

        public async Task<IActionResult> Rating(int missonid, string starid)
        {
            var userId = HttpContext.Session.GetString("user");
            ViewBag.UserId = int.Parse(userId);

            MissionRating rate = _CIDbContext.MissionRatings.Where(e => e.MissionId == missonid && e.UserId == Convert.ToInt32(userId)).FirstOrDefault();
            if (rate != null)
            {
                rate.Rating = starid;
                _CIDbContext.Update(rate);
            }
            else
            {
                MissionRating mr = new MissionRating();
                mr.MissionId = missonid;
                mr.UserId = Convert.ToInt32(userId);
                mr.Rating = starid;
                _CIDbContext.Add(mr);
            }
            _CIDbContext.SaveChanges();
            return Json(new { success = true, starid });
        }



    }
}
