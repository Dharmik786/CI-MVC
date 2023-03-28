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

        [HttpPost]
        public IActionResult addStoryDetail(MissionList model)
        {
            var userId = HttpContext.Session.GetString("user");
            _IUser.AddStory(model.missionId, Convert.ToInt32(userId), model.title, model.editor1, model.date);

            foreach (var i in model.attachment)
            {
                if (i != null)
                {
                    string UploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Image\\story");
                    string FileName = i.FileName;
                    string FilePath = Path.Combine(UploadsFolder, FileName);

                    using (var FileStream = new FileStream(FilePath, FileMode.Create))
                    {
                        i.CopyTo(FileStream);
                    }
                    var type = i.ContentType;

                    //_IUser.AddStoryMedia(i.ContentType.Split("/")[0], FileName, model.missionId, Convert.ToInt32(userId));
                    _IUser.AddStoryMedia(i.ContentType.Split("/")[0], FilePath, model.missionId, Convert.ToInt32(userId));


                  // _homeRepository.AddStoryMedia(Convert.ToString(i.ContentType.Split("/")[1]), Convert.ToString(filename), (long)vmmission.missionId, uid);
                }
            }

            return RedirectToAction("StoriesListing", "Home");
        }
        public IActionResult AddStory()
        {
            var userId = HttpContext.Session.GetString("user");
            var storyTitle = _IUser.missionApplications().Where(u => u.UserId == (Convert.ToInt32(userId)));

            MissionList ms = new MissionList();
            ms.mission = _IUser.mission();
            ms.missionApplications = _IUser.missionApplications().Where(u => u.UserId == (Convert.ToInt32(userId))).ToList();
            return View(ms);
        }
        public IActionResult StoriesListing()
        {
            MissionList missionList = new MissionList();
            missionList.stories = _IUser.stories();
            missionList.users = _IUser.user();
            missionList.mission = _IUser.mission();
            missionList.missionThemes = _IUser.missionThemes();
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



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}