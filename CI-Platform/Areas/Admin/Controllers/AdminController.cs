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
            var admin = HttpContext.Session.GetString("AdminId");
            if (admin == null)
            {

                return RedirectToAction("Login", "User", new { area = "User" });

            }
            return View();
        }
        public IActionResult GetUsers()
        {
            AdminVM a = new AdminVM();
            a.Users = _IUser.user();
            a.countries = _IUser.countries();
            a.cities = _IUser.cities();
            return PartialView("_User", a);
        }
        public IActionResult GetMission()
        {
            AdminVM a = new AdminVM();
            a.Missions = _IUser.mission();
            a.countries = _IUser.countries();
            a.cities = _IUser.cities();
            a.skills = _IUser.skills();
            a.missionThemes = _IUser.missionThemes();
            return PartialView("_MissionA", a);
        }
        public IActionResult GetApplication()
        {
            AdminVM a = new AdminVM();
            a.missionApplications = _IUser.missionApplications();
            a.Missions = _IUser.mission();
            a.Users = _IUser.user();
            return PartialView("_Application", a);
        }
        public IActionResult GetStory()
        {
            AdminVM a = new AdminVM();
            a.Users = _IUser.user();
            a.Missions = _IUser.mission();
            a.stories = _IUser.stories();
            return PartialView("_Stories", a);
        }
        public IActionResult GetSkills()
        {
            AdminVM a = new AdminVM();
            a.missionSkills = _IUser.GetMissionSkill();
            a.skills = _IUser.skills();
            return PartialView("_Skills", a);
        }
        public IActionResult GetThemes()
        {
            AdminVM a = new AdminVM();
            a.missionThemes = _IUser.missionThemes();
            return PartialView("_Themes", a);
        }

        public IActionResult GetCmsPage()
        {
            AdminVM a = new AdminVM();
            a.cmsPages = _IUser.GetCmsPage();
            return PartialView("_CmsPage", a);
        }

        public IActionResult GetBanner()
        {
            AdminVM a = new AdminVM();
            a.banner = _IUser.GetBanner();
            return PartialView("_Banner", a);
        }
        //-----------------------------------------------------------THEME-----------------------------------------------------
        //[HttpPost]
        //public IActionResult AddTheme(AdminVM model)
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
        //public ActionResult UpdateTheme(AdminVM model)
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
        public IActionResult SkillStatus(int id)
        {
            _IUser.SkillStatus(id);
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
        //---------------------------------------------------User -------------------------------------------------
        public async Task<IActionResult> GetUserDetails(int id)
        {
            var user = _IUser.GetUserByUserId(id);
            return Json(new { success = true, user = user });
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(string Image, int Id, string Fname, string Lname, string Email, string Password,
                string Employeeid, string Department, string Profiletext, string status, int Country, int City)
        {
            _IUser.UpdateUser(Id, Image, Fname, Lname, Email, Password, Employeeid, Department, Profiletext, status, Country, City);
            return Json(new { success = true });
        }
        public async Task<IActionResult> DeleteUser(int Id)
        {
            _IUser.DeleteUserById(Id);
            return Json(new { success = true });
        }
        //-----------------------------------------------Mission---------------------------------------------------
        public IActionResult EditMission(int id)
        {
            var Mission = _IUser.GetMissionNtId(id);

            return Json(new { success = true, Mission = Mission });
        }
        [HttpGet]
        public IActionResult AddMission(int id)
        {
            MissionVM vm = new MissionVM();
            vm.Missions = _IUser.mission();
            vm.countries = _IUser.countries();
            vm.cities = _IUser.cities();
            vm.skills = _IUser.skills();
            vm.missionThemes = _IUser.missionThemes();



            if (id != 0)
            {
                var finalurl = "";
                var VideoURLs = _IUser.missionMedia().Where(e => e.MissionId == id && e.MediaType == "Video").ToList();
                foreach (var videoURL in VideoURLs)
                {
                    finalurl = finalurl + videoURL.MediaPath + "\r\n";
                }

                var mission = _IUser.GetMissionNtId(id);
                vm.title = mission.Title;
                vm.shortDescription = mission.ShortDescription;
                vm.description = mission.Description;
                vm.countryId = mission.CountryId;
                vm.cityId = mission.CityId;
                vm.OrganisationName = mission.OrganizationName;
                vm.OrganisationDetail = mission.OrganizationDetail;
                vm.missionType = mission.MissionType;
                vm.startDate = (DateTime)mission.StartDate;
                vm.endDate = (DateTime)mission.EndDate;
                    vm.seats = Convert.ToInt32(mission.Seats);
                vm.url = finalurl;

                if (mission.Deadline != null)
                {
                    vm.deadline = (DateTime)mission.Deadline;
                }

                var time = _CIDbContext.GoalMissions.FirstOrDefault(e => e.MissionId == id);
                if(time != null)
                {
                    vm.goalObjectiveText = time.GoalObjectiveText;
                    vm.goalValue = time.GoalValue;

                }

                vm.missionThemeId = mission.ThemeId;
                vm.missionMedia = _IUser.GetMissionMediaById(id);
                vm.missionId = mission.MissionId;
                vm.missionSkills = _IUser.GetMissionSkillById(id);

                var r = from row1 in vm.skills.AsEnumerable()
                        where !vm.missionSkills.AsEnumerable().Select(r => r.SkillId).Contains(row1.SkillId)
                        select row1;
                vm.RemainingSkill = r.ToList();
                var imgfiles = _CIDbContext.MissionMedia.Where(m => m.MissionId == id && m.MediaType != "Video" && m.DeletedAt == null).ToList();
                var docfiles = _CIDbContext.MissionDocuments.Where(m => m.MissionId == id && m.DeletedAt == null).ToList();
                vm.ImageFiles = new List<MissionMedium>();
                int i = 1;
                if (imgfiles.Count > 0)
                {
                    foreach (var ifile in imgfiles)
                    {
                        vm.ImageFiles.Add(ifile);
                    }
                }
                i = 0;
                vm.DocFiles = new List<IFormFile>();
                foreach (var ifile in docfiles)
                {
                    i++;
                    string fileName = "example" + i + "." + ifile.DocumentType;  // specify the file name and extension
                    string contentType = ifile.MissionDocumentId.ToString();
                    MemoryStream ms = new MemoryStream(ifile.DocumentPath);
                    var file = new FormFile(ms, 0, ms.Length, contentType, fileName);
                    vm.DocFiles.Add(file);
                }

            }



            return PartialView("_MissionAddEdit", vm);
            }


        [HttpPost]
        public IActionResult AddEditMission(MissionVM model)
        {
            if(model.missionId == 0 || model.missionId == null)
            {
                var files = Request.Form.Files;
                _IUser.AddMission(model, files);
            }
            else
            {
                var files = Request.Form.Files;
                _IUser.EditMission(model, files);

            }

            model.Missions = _IUser.mission();
            model.countries = _IUser.countries();
            model.cities = _IUser.cities();
            model.skills = _IUser.skills();
            model.missionThemes = _IUser.missionThemes();
            //return PartialView("_MissionA", model);
            return RedirectToAction("Admin", "Admin");
        }

        [HttpPost]
        public IActionResult delDoc(string docId)
        {
            _IUser.delDoc(long.Parse(docId));
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult delImg(long imgId)
        {
            _IUser.delImg(imgId);
            return Json(new { success = true });
        }
        [HttpPost]
        public IActionResult DeleteMission(long missionId)
        {
            _IUser.DeleteMission(missionId);
            //var missionvm = new AdminMissionViewModel();
            //missionvm.missions = _IHome.Allmissions();
            //missionvm.countries = _IHome.allcountry();
            //missionvm.cities = _IHome.AllCity();
            //missionvm.themes = _IHome.alltheme();
            return Json(new { success = true });
        }

        //------------------------------------------------Banner Management---------------------------------
        public IActionResult AddBanner(int id, string Image, string desc, int sort)
        {
            _IUser.AddBanner(id, Image, desc, sort);
            return Json(new { sucess = true });
        }
        public IActionResult GetBannerById(int id)
        {
            var banner = _IUser.GetBannerById(id);
            return Json(banner);
        }

        [HttpPost]
        public IActionResult DeleteBanner(int id)
        {
            _IUser.DeleteBanner(id);
            return Json(new { sucess = true });
        }
    }
}
