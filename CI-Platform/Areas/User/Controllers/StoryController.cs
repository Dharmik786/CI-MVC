using CI_Entity.Models;
using CI_Entity.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using CI_PlatForm.Repository.Interface;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.Text;
using MimeTypes.Core;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Security.Policy;

namespace CI_Platform.Areas.User.Controllers
{
    [Area("User")]
    public class StoryController : Controller
    {
        private readonly CIDbContext _CIDbContext;
        private readonly IUserInterface _IUser;
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;

        public StoryController(CIDbContext CIDbContext, IUserInterface Iuser, IHostingEnvironment IHostingEnvironment)
        {
            _CIDbContext = CIDbContext;
            _IUser = Iuser;
            _hostingEnvironment = IHostingEnvironment;
        }

        public IActionResult StoriesListing()
        {
            MissionList missionList = new MissionList();
            missionList.stories = _IUser.stories().Where(u => u.Status == "APPROVE").ToList();
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
            missionList.storyMedia = _IUser.GetStoryMediaByStoryId(storyId);
            if (userId != null)
            {

                missionList.users = _IUser.user().Where(u => u.UserId != Convert.ToInt32(userId)).ToList();
            }
            else
            {
                missionList.users = _IUser.user();
            }
            missionList.missionThemes = _IUser.missionThemes();
            missionList.storydetails = _CIDbContext.Stories.FirstOrDefault(e => e.StoryId == storyId);

            return View(missionList);
        }
        [HttpPost]
        public IActionResult StoryView(int id)
        {
            var data = _CIDbContext.Stories.FirstOrDefault(e => e.StoryId == id);
            data.Views = data.Views + 1;
            _CIDbContext.Update(data);
            _CIDbContext.SaveChanges();
            return Json(data);
        }

        [HttpPost]
        public void SendStorymail(int storyId, long[] emailList)
        {
            //ViewBag.UserId = int.Parse(userId);

            foreach (var i in emailList)
            {
                try
                {
                    var userId = Convert.ToInt32(HttpContext.Session.GetString("user"));
                    var user = _IUser.user().FirstOrDefault(u => u.UserId == i);

                    var missionlink = Url.Action("StoryDetails", "Story", new { storyId }, Request.Scheme);

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
            var storyTitle = _IUser.missionApplications().Where(u => u.UserId == Convert.ToInt32(userId));

            StoryModel ms = new StoryModel();

            if (storyId != 0)
            {
                // ms.stories = _IUser.stories().Where(e => e.StoryId == storyId).ToList();
                ms.storyMedia = _IUser.storyMedia().Where(e => e.StoryId == storyId).ToList();
                ms.missionApplications = _IUser.missionApplications().Where(u => u.UserId == Convert.ToInt32(userId)).ToList();

                var s = _IUser.stories().Where(e => e.StoryId == storyId).FirstOrDefault();
                ms.missionId = s.MissionId;
                ms.title = s.Title;
                ms.editor1 = s.Description;
                ms.date = (DateTime)s.PublishedAt;
                ms.storyId = storyId;

                var media = _CIDbContext.StoryMedia.Where(e => e.StoryId == storyId && e.StoryType =="Video").FirstOrDefault();
                if (media != null)
                {
                    ms.url = media.StoryPath;
                }

                var sm = _IUser.storyMedia().Where(e => e.StoryId == storyId).ToList();
                ms.attachment = new List<IFormFile>();

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
                ms.storyMedia = _IUser.storyMedia().Where(e => e.StoryId == storyId).ToList();

                //var sm = _IUser.storyMedia().Where(e => e.StoryId == s.StoryId).FirstOrDefault();
                //    ms.attachment = sm.StoryMediaId;

                ms.mission = _IUser.mission();
                ms.missionApplications = _IUser.missionApplications().Where(u => u.UserId == Convert.ToInt32(userId)).ToList();
            }
            else
            {
                ms.mission = _IUser.mission();
                ms.missionApplications = _IUser.missionApplications().Where(u => u.UserId == Convert.ToInt32(userId)).ToList();
            }
            return View(ms);
        }
        [HttpPost]
        public async Task<IActionResult> addStoryDetailAsync(StoryModel model, string action, long storyId)
        {

            if (action == "submit")
            {
                var userId = HttpContext.Session.GetString("user");
                var sId = _IUser.SubmitStory(model.missionId, Convert.ToInt32(userId), model.title, model.editor1, model.date, model.storyId, model.url);


                if (model.ImgList != null)
                {
                    foreach (var i in model.ImgList)
                    {

                        StoryMedium sm = new StoryMedium();
                        sm.StoryId = sId;
                        sm.StoryType = "Image";
                        sm.StoryPath = i;
                        _CIDbContext.Add(sm);
                        _CIDbContext.SaveChanges();
                       
                    }
                }
                

                //if (model.attachment != null)
                //{
                //    if (model.storyId != 0)
                //    {
                //        _IUser.RemoveMedia(storyId);
                //    }

                //    foreach (var i in model.attachment)
                //    {
                //        var FileName = "";
                //        using (var ms = new MemoryStream())
                //        {
                //            await i.CopyToAsync(ms);
                //            var imageBytes = ms.ToArray();
                //            var base64String = Convert.ToBase64String(imageBytes);
                //            FileName = "data:image/png;base64," + base64String;
                //        }

                //        _IUser.AddStoryMedia(i.ContentType.Split("/")[0], FileName, model.missionId, Convert.ToInt32(userId), model.storyId, sId);
                //    }
                //}

            }
            else if (action == "save")
            {
                var userId = HttpContext.Session.GetString("user");
                var sId = _IUser.SaveStory(model.missionId, Convert.ToInt32(userId), model.title, model.editor1, model.date, model.storyId, model.url);
                if (model.ImgList != null)
                {
                    foreach (var i in model.ImgList)
                    {

                        StoryMedium sm = new StoryMedium();
                        sm.StoryId = sId;
                        sm.StoryType = "Image";
                        sm.StoryPath = i;
                        _CIDbContext.Add(sm);
                        _CIDbContext.SaveChanges();

                    }
                }
                
                //if (model.attachment != null)
                //{
                //    if (model.storyId != 0)
                //    {
                //        _IUser.RemoveMedia(storyId);
                //    }

                //    foreach (var i in model.attachment)
                //    {
                //        var FileName = "";
                //        using (var ms = new MemoryStream())
                //        {
                //            await i.CopyToAsync(ms);
                //            var imageBytes = ms.ToArray();
                //            var base64String = Convert.ToBase64String(imageBytes);
                //            FileName = "data:image/png;base64," + base64String;
                //        }

                //        _IUser.AddStoryMedia(i.ContentType.Split("/")[0], FileName, model.missionId, Convert.ToInt32(userId), model.storyId, sId);
                //    }
                //}
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
        public ActionResult DeleteImg(int id)
        {
            var media = _CIDbContext.StoryMedia.FirstOrDefault(e=>e.StoryMediaId == id);
            _CIDbContext.Remove(media);
            _CIDbContext.SaveChanges();
            return Json(new { success = true });
        }
    }
}
