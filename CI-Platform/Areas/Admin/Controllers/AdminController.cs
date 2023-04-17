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

        [HttpPost]
        public IActionResult AddTheme(AdiminVM model)
        {
            _IUser.AddMissionTheme(model.singleTheme);
            return RedirectToAction("Admin", "Admin");
        }
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
        public ActionResult UpdateTheme(AdiminVM model)
        {
            _IUser.EditTheme(model.singleTheme, model.ThemeId);
            return RedirectToAction("Admin", "Admin");
        }


        //Skill
        [HttpPost]
        public async Task<IActionResult> DeleteSkill(int skillid)
        {
            var DeleteSkill = _IUser.DeleteSkill(skillid);
            return Json(new { success = true, DeleteSkill = DeleteSkill });
        }
        [HttpPost]
        public IActionResult AddSkill(AdiminVM model)
        {
            _IUser.AddSkill(model.singleskill);
            return RedirectToAction("Admin", "Admin");
        }
        public async Task<IActionResult> EditSkill(int Skillid)
        {
            var skill = _IUser.GetSkill(Skillid);
            return Json(new { success = true, skill = skill });
        }
        public IActionResult UpdateSkill(AdiminVM model)
        {
            _IUser.EditSkill(model.singleskill, model.skillId);
            return RedirectToAction("Admin", "Admin");
        }

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
    }
}
