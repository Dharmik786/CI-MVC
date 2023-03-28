﻿using CI.Models;
using CI_Entity.Models;
using CI_Entity.ViewModel;
using CI_Platform.Models;
using CI_PlatForm.Repository.Interface;
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
        private readonly IUserInterface _IUser;

        public HomeController(CIDbContext CIDbContext,IUserInterface Iuser)
        {
            _CIDbContext = CIDbContext;
            _IUser = Iuser;
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
            _IUser.AddStory(model.missionId,Convert.ToInt32(userId),model.title,model.editor1, model.date);

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

        public IActionResult StoryDetails(int missionid)
        {
            MissionList missionList= new MissionList();
            missionList.stories = _IUser.stories();
            missionList.users = _IUser.user();
            missionList.missionThemes = _IUser.missionThemes();

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