using CI.Models;
using CI_Entity.Models;
using CI_Entity.ViewModel;
using CI_Platform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CI_Platform.Controllers
{
    public class HomeController : Controller
    {
      

        private readonly CIDbContext _CIDbContext;

        public HomeController(CIDbContext CIDbContext)
        {
            _CIDbContext = CIDbContext;
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
            missionList.stories = _CIDbContext.Stories.ToList();
            missionList.users = _CIDbContext.Users.ToList();    
            missionList.mission=_CIDbContext.Missions.ToList();
            return View(missionList);
        }

        public IActionResult StoryDetails(int missionid)
        {
            MissionList missionList= new MissionList();
            missionList.stories = _CIDbContext.Stories.ToList();
            missionList.users = _CIDbContext.Users.ToList();
            
            var data = missionList.stories.Where(e=>e.MissionId == missionid).FirstOrDefault();
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