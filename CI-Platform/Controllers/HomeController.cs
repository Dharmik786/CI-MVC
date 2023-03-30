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

        public IActionResult StoriesListing()
        {
            MissionList missionList = new MissionList();
            missionList.stories = _IUser.stories().Where(u => u.Status == "1").ToList();
            missionList.users = _IUser.user();
            missionList.mission = _IUser.mission();
            missionList.missionThemes = _IUser.missionThemes();
            missionList.storyMedia = _IUser.storyMedia();
            return View(missionList);
        }

        public IActionResult StoryDetails(int storyId)
        {
            var userId = HttpContext.Session.GetString("user");
            MissionList missionList = new MissionList();
            missionList.stories = _IUser.stories();
            missionList.users = _IUser.user().Where(u => u.UserId != Convert.ToInt32(userId)).ToList();
            missionList.missionThemes = _IUser.missionThemes();

            var data = missionList.stories.Where(e => e.StoryId == storyId).FirstOrDefault();
            missionList.storydetails = data;
            return View(missionList);
        }
        [HttpPost]
        public void SendStorymail(int missionid, long[] emailList)
        {
            //ViewBag.UserId = int.Parse(userId);

            foreach (var i in emailList)
            {
                var userId = Convert.ToInt32(HttpContext.Session.GetString("user"));
                var user = _IUser.user().FirstOrDefault(u => u.UserId == i);

                var missionlink = Url.Action("StoryDetails", "Home", new { user = user.UserId, missionid = missionid }, Request.Scheme);

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
                //_IUser.AddMissionInvite(userId, missionid, user.UserId);
            }
        }
        public IActionResult AddStory(int? storyId)
        {
            var userId = HttpContext.Session.GetString("user");
            var storyTitle = _IUser.missionApplications().Where(u => u.UserId == (Convert.ToInt32(userId)));

            MissionList ms = new MissionList();

            if (storyId != null)
            {
               // ms.stories = _IUser.stories().Where(e => e.StoryId == storyId).ToList();
                ms.storyMedia = _IUser.storyMedia().Where(e => e.StoryId == storyId).ToList();
                ms.missionApplications = _IUser.missionApplications().Where(u => u.UserId == (Convert.ToInt32(userId))).ToList();

                var s = _IUser.stories().Where(e => e.StoryId == storyId).FirstOrDefault();
                ms.missionId = s.MissionId;
                ms.title = s.Title;
                ms.editor1 = s.Description;
                ms.date = s.CreatedAt;
                ms.storyId = (long)storyId;

                var sm = _IUser.storyMedia().Where(e => e.StoryId == s.StoryId).FirstOrDefault();
              //    ms.attachment = sm.StoryMediaId;

                ms.mission = _IUser.mission();
                ms.missionApplications = _IUser.missionApplications().Where(u => u.UserId == (Convert.ToInt32(userId))).ToList();
            }
            else
            {
                ms.mission = _IUser.mission();
                ms.missionApplications = _IUser.missionApplications().Where(u => u.UserId == (Convert.ToInt32(userId))).ToList();
            }
            return View(ms);
        }

        
        [HttpPost]
        public async Task<IActionResult> addStoryDetailAsync(MissionList model, string action,long storyId)
        {
           
            if (action == "submit")
            {
                var userId = HttpContext.Session.GetString("user");
                _IUser.SubmitStory(model.missionId, Convert.ToInt32(userId), model.title, model.editor1, model.date,model.storyId);

                if (model.attachment != null)
                {
                    foreach (var i in model.attachment)
                    {
                        //string UploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Image\\story");
                        //string FileName = i.FileName;
                        //string FilePath = Path.Combine(UploadsFolder, FileName);

                        //using (var FileStream = new FileStream(FilePath, FileMode.Create))
                        //{
                        //    i.CopyTo(FileStream);
                        //}
                        //var type = i.ContentType;

                        var FileName = "";
                        using (var ms = new MemoryStream())
                        {
                            await i.CopyToAsync(ms);
                            var imageBytes = ms.ToArray();
                            var base64String = Convert.ToBase64String(imageBytes);
                            FileName = "data:image/png;base64," + base64String;
                        }

                        _IUser.AddStoryMedia(i.ContentType.Split("/")[0], FileName, model.missionId, Convert.ToInt32(userId), model.storyId);
                    }
                }

            }
            else if (action == "save")
            {
                var userId = HttpContext.Session.GetString("user");
                _IUser.SaveStory(model.missionId, Convert.ToInt32(userId), model.title, model.editor1, model.date,model.storyId);

                if (model.attachment != null)
                {
                    foreach (var i in model.attachment)
                    {
                        var FileName = "";
                        using (var ms = new MemoryStream())
                        {
                            await i.CopyToAsync(ms);
                            var imageBytes = ms.ToArray();
                            var base64String = Convert.ToBase64String(imageBytes);
                            FileName = "data:image/png;base64," + base64String;
                        }

                        _IUser.AddStoryMedia(i.ContentType.Split("/")[0], FileName, model.missionId, Convert.ToInt32(userId), model.storyId);
                    }
                }
            }
            else
            {
                return RedirectToAction("StoriesListing", "Home");
            }
            //return View();

            return RedirectToAction("StoriesListing", "Home");
        }

        public IActionResult DraftStory()
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetString("user"));

            MissionList missionList = new MissionList();
            missionList.stories = _IUser.stories().Where(u => u.Status == "DRAFT" && u.UserId == userId).ToList();
            missionList.users = _IUser.user();
            missionList.mission = _IUser.mission();
            missionList.missionThemes = _IUser.missionThemes();
            missionList.storyMedia = _IUser.storyMedia();
            return View(missionList);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}