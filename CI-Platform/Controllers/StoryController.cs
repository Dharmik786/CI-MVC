using CI_Entity.Models;
using CI_Entity.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using CI_PlatForm.Repository.Interface;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.Text;
using MimeTypes.Core;

namespace CI_Platform.Controllers
{
    public class StoryController : Controller
    {
        private readonly CIDbContext _CIDbContext;
        private readonly IUserInterface _IUser;
        [Obsolete]
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public StoryController(CIDbContext CIDbContext, IUserInterface Iuser, IHostingEnvironment IHostingEnvironment)
        {
            _CIDbContext = CIDbContext;
            _IUser = Iuser;
            _hostingEnvironment = IHostingEnvironment;
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
            data.Views = data.Views + 1;
            missionList.storydetails = data;
            _CIDbContext.Update(data);
            _CIDbContext.SaveChanges();
            return View(missionList);
        }
        [HttpPost]
        public void SendStorymail(int missionid, long[] emailList)
        {
            //ViewBag.UserId = int.Parse(userId);

            foreach (var i in emailList)
            {
                try
                {
                    var userId = Convert.ToInt32(HttpContext.Session.GetString("user"));
                    var user = _IUser.user().FirstOrDefault(u => u.UserId == i);

                    var missionlink = Url.Action("StoryDetails", "Story", new { user = user.UserId, missionid = missionid }, Request.Scheme);

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
                catch (Exception ex)
                {
                        
                }


            }
        }
        public IActionResult AddStory(long storyId)
        {
            var userId = HttpContext.Session.GetString("user");
            var storyTitle = _IUser.missionApplications().Where(u => u.UserId == (Convert.ToInt32(userId)));

            MissionList ms = new MissionList();

            if (storyId != 0)
            {
                // ms.stories = _IUser.stories().Where(e => e.StoryId == storyId).ToList();
                ms.storyMedia = _IUser.storyMedia().Where(e => e.StoryId == storyId).ToList();
                ms.missionApplications = _IUser.missionApplications().Where(u => u.UserId == (Convert.ToInt32(userId))).ToList();

                var s = _IUser.stories().Where(e => e.StoryId == storyId).FirstOrDefault();
                ms.missionId = s.MissionId;
                ms.title = s.Title;
                ms.editor1 = s.Description;
                ms.date = s.CreatedAt; 
                ms.storyId = storyId;

                var sm = _IUser.storyMedia().Where(e => e.StoryId == storyId).ToList();
                ms.attachment = new List<IFormFile>();
                //ms.attachment = sm.StoryPath;

                foreach (var img in sm)
                {
                    var fileBytes = Encoding.UTF8.GetBytes(img.StoryPath);

                    using (var memoryStream = new MemoryStream(fileBytes))
                    {
                        var fileName = Path.GetFileName(img.StoryPath);
                        var contentType = MimeTypeMap.GetMimeType(fileName);
                        var file = new FormFile(memoryStream, 0, memoryStream.Length, fileName, contentType);
                        // Use the 'file' object as needed
                        ms.attachment.Add(file);
                    }
                }
                ms.storyMedia = _IUser.storyMedia().Where(e=>e.StoryId == storyId).ToList();    

                //var sm = _IUser.storyMedia().Where(e => e.StoryId == s.StoryId).FirstOrDefault();
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
        public async Task<IActionResult> addStoryDetailAsync(MissionList model, string action, long storyId)
        {

            if (action == "submit")
            {
                var userId  = HttpContext.Session.GetString("user");
                var sId = _IUser.SubmitStory(model.missionId, Convert.ToInt32(userId), model.title, model.editor1, model.date, model.storyId);

                if (model.attachment != null)
                {
                    if (model.storyId != 0)
                    {
                        _IUser.RemoveMedia(storyId);    
                    }

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

                        _IUser.AddStoryMedia(i.ContentType.Split("/")[0], FileName, model.missionId, Convert.ToInt32(userId), model.storyId, sId);
                    }
                }

            }
            else if (action == "save")
            {
                var userId = HttpContext.Session.GetString("user");
                var sId = _IUser.SaveStory(model.missionId, Convert.ToInt32(userId), model.title, model.editor1, model.date, model.storyId);

                if (model.attachment != null)
                {
                    if (model.storyId != 0)
                    {
                        _IUser.RemoveMedia(storyId);
                    }

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

                        _IUser.AddStoryMedia(i.ContentType.Split("/")[0], FileName, model.missionId, Convert.ToInt32(userId), model.storyId, sId);
                    }
                }
            }
            else
            {
                return RedirectToAction("StoriesListing", "Story");
            }
            //return View();

            return RedirectToAction("StoriesListing", "Story");
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

        public bool DraftDelete(int storyId)
        {
            _IUser.DraftDelete(storyId);
            return true;
        }
    }
}
