using CI.Models;
using CI_Platform.Models;
using CI_Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using System.Security.Claims;
using System.Net.Mail;
using System.Net;
using CI_Entity.ViewModel;
using CI_PlatForm.Repository.Interface;

namespace CI_Platform.Areas.User.Controllers
{
    [Area("User")]
    public class VolunteeringController : Controller
    {
        private readonly CIDbContext _CIDbContext;
        private readonly object JsonRequestBehavior;
        private readonly IUserInterface _IUser;


        public VolunteeringController(CIDbContext CIDbContext, IUserInterface IUser)
        {
            _CIDbContext = CIDbContext;
            _IUser = IUser;
        }

        public IActionResult Volunteering(long id, int missionid, int pageIndex = 1)
        {
            var userId = HttpContext.Session.GetString("user");
            //ViewBag.UserId = int.Parse(userId);


            MissionList vMMission = new MissionList();
            vMMission.mission = _IUser.mission();
            vMMission.cities = _IUser.cities();
            vMMission.countries = _IUser.countries();
            vMMission.missionThemes = _IUser.missionThemes();
            vMMission.skills = _IUser.skills();
            vMMission.missionMedia = _IUser.missionMedia();
            vMMission.missionRatings = _IUser.MissionRatings();
            vMMission.goalMissions = _IUser.goalMissions();
            vMMission.users = _IUser.user().Where(u => u.UserId != Convert.ToInt32(userId)).ToList();
            vMMission.timesheets = _IUser.timesheets();

            MissionRating ratin = vMMission.missionRatings.FirstOrDefault(e => e.MissionId == missionid && e.UserId == Convert.ToInt32(userId));
            vMMission.userRate = ratin != null ? int.Parse(ratin.Rating) : 0;


            vMMission.comments = _IUser.comments().Where(e => e.MissionId == missionid).ToList();
            var cmt = _IUser.comments().Where(e => e.MissionId == missionid).ToList();
            vMMission.comments = cmt.OrderByDescending(cmt => cmt.CreatedAt).ToList();

            vMMission.missionApplications = _IUser.missionApplications();
            vMMission.userId = Convert.ToInt32(userId);
            vMMission.recentVolunteering = _IUser.missionApplications().Where(e => e.MissionId == missionid).ToList();




            // vMMission.isApplied = _IUser.missionApplications().Where(e=>e.MissionId == missionid && e.UserId == Convert.ToInt32(userId)).ToList();


            vMMission.favoriteMissions = _IUser.favoriteMissions().Where(e => e.UserId == Convert.ToInt32(userId)).ToList();

            var data = vMMission.mission.FirstOrDefault(e => e.MissionId == missionid);
            vMMission.singleMission = data;

            vMMission.relatedMission = _IUser.mission().Where(e => e.ThemeId == data.ThemeId && e.MissionId != missionid).ToList();
            if (userId != null)
            {
                vMMission.missionApplications = _IUser.missionApplications().Where(e => e.MissionId == missionid && e.UserId == Convert.ToInt32(userId)).ToList();
            }
            else
            {
                vMMission.missionApplications = _IUser.missionApplications().Where(e => e.MissionId == missionid).ToList();
            }


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



            int pageSize = 9; // Set the page size to 9
            var volunteers = vMMission.recentVolunteering; // Retrieve all volunteers from data source
            int totalCount = volunteers.Count(); // Get the total number of volunteers
            int skip = (pageIndex - 1) * pageSize;
            var volunteersOnPage = volunteers.Skip(skip).Take(pageSize).ToList(); // Get the volunteers for the current page

            ViewBag.TotalCount = totalCount;
            ViewBag.PageSize = pageSize;
            ViewBag.PageIndex = pageIndex;
            ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            ViewBag.TotalVol = vMMission.recentVolunteering.Count();
            ViewBag.recentvolunteered = volunteersOnPage;

            return View(vMMission);
        }

        public IActionResult addToFavourite(int missonid)
        {
            var userId = HttpContext.Session.GetString("user");
            ViewBag.UserId = int.Parse(userId);

            //add favourite mission data
            if (missonid != null)
            {
                //var tempFav = _IUser.favoriteMissions().Where(e => (e.MissionId == missonid) && (e.UserId == Convert.ToInt32(userId))).FirstOrDefault();
                _IUser.FavMission(missonid, Convert.ToInt32(userId));

                //if (tempFav == null)
                //{
                // _IUser.addfav(missonid, Convert.ToInt32(userId));
                //  FavoriteMission fm = new FavoriteMission();
                // fm.UserId = Convert.ToInt32(userId);
                // fm.MissionId = missonid;
                //_CIDbContext.Add(fm);
                //}
                //else
                //{
                //    _CIDbContext.Remove(tempFav);
                //}
                //_CIDbContext.SaveChanges();

            }
            return RedirectToAction("Volunteering", new { id = int.Parse(userId), missionid = missonid });

        }

        public IActionResult PostComment(int missonid, string cmt)
        {
            var userId = HttpContext.Session.GetString("user");
            ViewBag.UserId = int.Parse(userId);

            _IUser.addcomment(missonid, Convert.ToInt32(userId), cmt);

            //Comment c = new Comment();
            //c.UserId = Convert.ToInt32(userId);
            //c.MissionId = missonid;
            //c.CommentText = cmt;
            //_CIDbContext.Add(c);
            //_CIDbContext.SaveChanges(true);
            //return PartialView("_Comment");

            return RedirectToAction("Volunteering", new { id = int.Parse(userId), missionid = missonid });
        }

        [HttpPost]
        public void Sendmail(int missionid, long[] emailList)
        {
            //ViewBag.UserId = int.Parse(userId);

            foreach (var i in emailList)
            {
                var userId = Convert.ToInt32(HttpContext.Session.GetString("user"));
                var user = _IUser.user().FirstOrDefault(u => u.UserId == i);

                var missionlink = Url.Action("Volunteering", "Volunteering", new { user = user.UserId, missionid }, Request.Scheme);

                var fromAddress = new MailAddress("ciproject18@gmail.com", "Sender Name");
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
                    Credentials = new NetworkCredential("ciproject18@gmail.com", "ypijkcuixxklhrks"),
                    EnableSsl = true

                };
                smtpClient.Send(message);
                _IUser.AddMissionInvite(userId, missionid, user.UserId);
            }
        }

        public IActionResult Rating(int missonid, string starid)
        {
            var userId = HttpContext.Session.GetString("user");
            ViewBag.UserId = int.Parse(userId);

            MissionRating rate = _IUser.MissionRatings().Where(e => e.MissionId == missonid && e.UserId == Convert.ToInt32(userId)).FirstOrDefault();
            _IUser.rating(missonid, starid, Convert.ToInt32(userId));
            //if (rate != null)
            //{
            //    rate.Rating = starid;
            //    _CIDbContext.Update(rate);
            //}
            //else
            //{
            //    MissionRating mr = new MissionRating();
            //    mr.MissionId = missonid;
            //    mr.UserId = Convert.ToInt32(userId);
            //    mr.Rating = starid;
            //    _CIDbContext.Add(mr);
            //}
            //_CIDbContext.SaveChanges();
            //return Json(new { success = true, starid });
            return RedirectToAction("Volunteering", new { id = int.Parse(userId), missionid = missonid });

        }


        public IActionResult applyMission(int missionid)
        {
            var userId = HttpContext.Session.GetString("user");

            _IUser.applymission(missionid, Convert.ToInt32(userId));

            //MissionApplication ma = _IUser.missionApplications().Where(e=>e.MissionId == missionid).FirstOrDefault();
            //if(ma != null)
            //{
            //    _CIDbContext.Remove(ma);
            //}
            //else
            //{   
            //    MissionApplication mr = new MissionApplication();
            //    mr.MissionId = missionid;
            //    mr.UserId = Convert.ToInt32(userid);
            //    mr.ApprovalStatus = "1";
            //    mr.AppliedAt= DateTime.Now;
            //    _CIDbContext.Add(mr);
            //}
            //_CIDbContext.SaveChanges();
            return RedirectToAction("Volunteering", new { id = int.Parse(userId), missionid });
        }

        public IActionResult cmtDelete(int cmtId, int missionId)
        {
            var userId = HttpContext.Session.GetString("user");
            _IUser.cmtdetele(cmtId, int.Parse(userId));
            return RedirectToAction("Volunteering", new { id = int.Parse(userId), missionid = missionId });
        }

    }
}
