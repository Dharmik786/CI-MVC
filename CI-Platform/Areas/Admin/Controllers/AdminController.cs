using CI_Entity.Models;
using CI_Entity.ViewModel;
using Microsoft.AspNetCore.Mvc;
using CI_PlatForm.Repository.Interface;

namespace CI_Platform.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly CIDbContext _CIDbContext;
        private readonly IUserInterface _IUser;
        [Obsolete]

        public AdminController(CIDbContext CIDbContext, IUserInterface Iuser)
        {
            _CIDbContext = CIDbContext;
            _IUser = Iuser;
        }

        public IActionResult Admin()
        {
            return View();
        }
        public IActionResult GetUsers()
        {
            AdiminVM a = new AdiminVM();
            a.Users = _IUser.user();
            a.countries = _IUser.countries();
            a.cities = _IUser.cities();
            return PartialView("_User", a);
        }
        public IActionResult GetMission()
        {
            AdiminVM a = new AdiminVM();
            a.Missions = _IUser.mission();
            return PartialView("_MissionA", a);
        }
        public IActionResult GetApplication()
        {
            AdiminVM a = new AdiminVM();
            a.missionApplications = _IUser.missionApplications();
            a.Missions = _IUser.mission();
            a.Users = _IUser.user();
            return PartialView("_Application", a);
        }
        public IActionResult GetStory()
        {
            AdiminVM a = new AdiminVM();
            a.Users = _IUser.user();
            a.Missions = _IUser.mission();
            a.stories = _IUser.stories();
            return PartialView("_Stories", a);
        }
        public IActionResult GetSkills()
        {
            AdiminVM a = new AdiminVM();
            a.missionSkills = _IUser.GetMissionSkill();
            a.skills = _IUser.skills();
            return PartialView("_Skills", a);
        }
        public IActionResult GetThemes()
        {
            AdiminVM a = new AdiminVM();
            a.missionThemes = _IUser.missionThemes();
            return PartialView("_Themes", a);
        }

        public IActionResult GetCmsPage()
        {
            AdiminVM a = new AdiminVM();
            a.cmsPages = _IUser.GetCmsPage();
            return PartialView("_CmsPage", a);
        }
        //-----------------------------------------------------------THEME-----------------------------------------------------
        //[HttpPost]
        //public IActionResult AddTheme(AdiminVM model)
        //{
        //    _IUser.AddMissionTheme(model.singleTheme);
        //    return RedirectToAction("Admin", "Admin");
        //}
        [HttpPost]
        public async Task<IActionResult> DeleteTheme(int ThemeId)
        {
            var DeleteTheme = _IUser.DeleteTheme(ThemeId);
            return Json(new { success = true, DeleteTheme = DeleteTheme });
        }
        public async Task<IActionResult> EditMissionTheme(int ThemeId)
        {
            var missionTheme = _IUser.GetTheme(ThemeId);
            return Json(new { success = true, MissionTheme = missionTheme });
        }
        //public ActionResult UpdateTheme(AdiminVM model)
        //{
        //    _IUser.EditTheme(model.singleTheme, model.ThemeId);
        //    return RedirectToAction("Admin", "Admin");
        //}
        public ActionResult UpdateMissionTheme(string theme, int themmeid)
        {
            _IUser.EditTheme(theme, themmeid);
            return Json(new { success = true });

        }
        [HttpPost]
        public IActionResult AddMissionTheme(string theme)
        {
            _IUser.AddMissionTheme(theme);
            return Json(new { success = true });
        }


        //-----------------------------------------------------------------------Skill------------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> DeleteSkill(int skillid)
        {
            var DeleteSkill = _IUser.DeleteSkill(skillid);
            return Json(new { success = true, DeleteSkill = DeleteSkill });
        }
        [HttpPost]
        public IActionResult AddSkill(string skill)
        {
            _IUser.AddSkill(skill);
            return Json(new { success = true });
        }
        public async Task<IActionResult> EditSkill(int Skillid)
        {
            var skill = _IUser.GetSkill(Skillid);
            return Json(new { success = true, skill = skill });
        }
        public IActionResult UpdateSkill(string skill, int skillid)
        {
            _IUser.EditSkill(skill, skillid);
            return Json(new { success = true });
        }
        //------------------------------------------------------------------aaplication---------------------------------------------------------
        public async Task<IActionResult> ApproveApplication(int id)
        {
            _IUser.approveApplication(id);
            return Json(new { success = true, approve = id });
        }
        public async Task<IActionResult> RejectApplication(int id)
        {
            _IUser.rejectApplication(id);
            return Json(new { success = true, approve = id });
        }


        /*---------------------------------------------------------------Cms page---------------------------------------------------------------*/
        public async Task<IActionResult> GetCms(int cmsid)
        {
            var cms = _IUser.GetCmsPageById(cmsid);
            return Json(new { success = true, cms = cms });
        }
        public async Task<IActionResult> DeleteCms(int cmsid)
        {
            var cms = _IUser.DeleteCmsPageById(cmsid);
            return Json(new { success = true });
        }
        [HttpPost]
        public async Task<IActionResult> AddCms(string title, string desc, string slug, string status, int id)
        {
            if (id == 0)
            {
                _IUser.AddCmsPage(title, desc, slug, status);
            }
            else
            {
                _IUser.EditCmsPage(id, title, desc, slug, status);
            }


            return Json(new { success = true });
        }

        //-------------------------------------------------------------Story---------------------------------------------------------
        public async Task<IActionResult> ApproveStory(int id)
        {
            _IUser.approveStory(id);
            return Json(new { success = true });
        }
        public async Task<IActionResult> RejectStory(int id)
        {
            _IUser.rejectStory(id);
            return Json(new { success = true });
        }
        public async Task<IActionResult> DeleteStory(int id)
        {
            _IUser.DeleteStory(id);
            return Json(new { success = true });
        }
    }
}
