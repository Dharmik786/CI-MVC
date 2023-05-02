using CI_Entity.Models;
using CI_Entity.ViewModel;
using CI_PlatForm.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.SqlServer.Server;
using NuGet.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PlatForm.Repository.Repository
{
    public class UserRepository : IUserInterface
    {
        private readonly CIDbContext _CIDbContext;

        public UserRepository(CIDbContext CIDbContext)
        {
            _CIDbContext = CIDbContext;
        }
        public bool Registration(string FirstName, string LastName, string Email, long PhoneNumber, string ConfirmPassword)
        {

            var c = _CIDbContext.Users.FirstOrDefault(u => u.Email == Email);
            var userData = new User
            {
                FirstName = FirstName,
                LastName = LastName,
                PhoneNumber = PhoneNumber,
                Email = Email,
                Password = ConfirmPassword,
                Status = "Active"
            };
            if (c == null)
            {
                _CIDbContext.Users.Add(userData);
                _CIDbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public User Login(string Email, string Password)
        {
            return _CIDbContext.Users.Where(u => u.Email == Email && u.Password == Password && u.DeletedAt == null && u.Status == "Active").FirstOrDefault();
        }
        public Admin GetAdminDetails(string email, string password)
        {
            return _CIDbContext.Admins.Where(e => e.Email == email && e.Password == password).FirstOrDefault();
        }
        public List<MissionDocument> GetMissionDocument()
        {
            return _CIDbContext.MissionDocuments.Where(e => e.DeletedAt == null).ToList();
        }
        public List<StoryMedium> GetStoryMediaByStoryId(int id)
        {
            return _CIDbContext.StoryMedia.Where(e => e.StoryId == id && e.DeletedAt == null).ToList();
        }
        public User Forget(string Email)
        {
            return _CIDbContext.Users.FirstOrDefault(u => u.Email == Email);
            //if(c!=null)
            //{
            //    return false;
            //}
            //else
            //{
            //    return true;
            //}
        }
        public PasswordReset passwordResets(string email, string token)
        {
            var c = _CIDbContext.PasswordResets.FirstOrDefault(u => u.Email == email && u.Token == token);
            if (c == null)
            {
                //_CIDbContext.Add(c);

                var p = new PasswordReset
                {
                    Email = email,
                    Token = token
                };
                _CIDbContext.PasswordResets.Add(p);
                _CIDbContext.SaveChanges();
            }
            return c;
        }


        public List<Mission> mission()
        {
            return _CIDbContext.Missions.Where(e => e.DeletedAt == null).ToList();
        }

        public List<User> user()
        {
            return _CIDbContext.Users.ToList();
        }

        public User? GetUserByUserId(long userId)
        {
            return _CIDbContext.Users.Where(e => e.UserId == userId).FirstOrDefault();
        }

        public List<Country> countries()
        {
            return _CIDbContext.Countries.ToList();
        }

        public List<City> cities()
        {
            return _CIDbContext.Cities.ToList();
        }

        public List<Skill> skills()
        {
            return _CIDbContext.Skills.ToList();
        }

        public List<MissionTheme> missionThemes()
        {
            return _CIDbContext.MissionThemes.ToList();
        }
        public List<Banner> GetBanner()
        {
            return _CIDbContext.Banners.ToList();
        }

        public List<GoalMission> goalMissions()
        {
            return _CIDbContext.GoalMissions.ToList();
        }

        public List<FavoriteMission> favoriteMissions()
        {
            return _CIDbContext.FavoriteMissions.ToList();
        }

        public List<MissionRating> MissionRatings()
        {
            return _CIDbContext.MissionRatings.ToList();
        }
        public List<MissionMedium> missionMedia()
        {
            return _CIDbContext.MissionMedia.ToList();
        }
        public List<Timesheet> timesheets()
        {
            return _CIDbContext.Timesheets.Where(e => e.DeletedAt == null).ToList();
        }
        public List<Comment> comments()
        {
            return _CIDbContext.Comments.ToList();
        }
        public List<MissionApplication> missionApplications()
        {
            return _CIDbContext.MissionApplications.ToList();
        }
        public List<Skill> GetAllskill()
        {
            return _CIDbContext.Skills.ToList();
        }
        public FavoriteMission addfav(int missionId, int userId)
        {
            var fav = new FavoriteMission
            {
                MissionId = missionId,
                UserId = userId,
            };
            _CIDbContext.FavoriteMissions.Add(fav);
            return fav;
        }

        public FavoriteMission FavMission(int missionId, int userId)
        {
            var tempFav = _CIDbContext.FavoriteMissions.Where(e => (e.MissionId == missionId) && (e.UserId == Convert.ToInt32(userId))).FirstOrDefault();
            if (tempFav == null)
            {
                FavoriteMission fav = new FavoriteMission
                {
                    MissionId = missionId,
                    UserId = userId,
                };
                _CIDbContext.Add(fav);

            }
            else
            {
                _CIDbContext.FavoriteMissions.Remove(tempFav);
            }
            _CIDbContext.SaveChanges();
            return tempFav;
        }
        public MissionInvite AddMissionInvite(int FromUserId, int missionId, long Touserid)
        {
            MissionInvite mi = new MissionInvite();
            mi.FromUserId = FromUserId;
            mi.ToUserId = Touserid;
            mi.MissionId = missionId;
            mi.CreatedAt = DateTime.Now;
            _CIDbContext.MissionInvites.Add(mi);
            _CIDbContext.SaveChanges();
            return mi;
        }

        public Comment addcomment(int missionId, int userId, string cmt)
        {
            Comment c = new Comment();
            c.UserId = userId;
            c.MissionId = missionId;
            c.CommentText = cmt;
            _CIDbContext.Add(c);
            _CIDbContext.SaveChanges();
            return c;
        }
        public MissionRating rating(int missionId, string starId, int userId)
        {
            MissionRating rate = _CIDbContext.MissionRatings.Where(e => e.MissionId == missionId && e.UserId == Convert.ToInt32(userId)).FirstOrDefault();
            if (rate != null)
            {
                rate.Rating = starId;
                _CIDbContext.Update(rate);
            }
            else
            {
                MissionRating mr = new MissionRating();
                mr.UserId = Convert.ToInt32(userId);
                mr.MissionId = missionId;
                mr.Rating = starId;
                _CIDbContext.Add(mr);
            }
            _CIDbContext.SaveChanges();
            return rate;
        }
        public MissionApplication applymission(int missionId, int userId)
        {
            //MissionApplication ma= _CIDbContext.MissionApplications.Where(e => e.MissionId == missionId).FirstOrDefault();
            //if(ma==null)
            //{
            //    _CIDbContext.Remove(ma);
            //}
            //else
            //{
            MissionApplication am = new MissionApplication();
            am.MissionId = missionId;
            am.UserId = userId;
            am.ApprovalStatus = "Pending";
            am.AppliedAt = DateTime.Now;
            _CIDbContext.Add(am);
            //}
            _CIDbContext.SaveChanges();
            return am;
        }
        public List<Story> stories()
        {
            return _CIDbContext.Stories.ToList();
        }

        public long SubmitStory(long missionId, long userId, string title, string description, DateTime date, long storyId, string url)
        {
            if (storyId == 0)
            {
                Story st = new Story();
                st.MissionId = missionId;
                st.UserId = userId;
                st.Title = title;
                st.Description = description;
                st.Status = "PENDING";
                st.PublishedAt = date;
                st.CreatedAt = DateTime.Now;
                _CIDbContext.Stories.Add(st);
                _CIDbContext.SaveChanges();

                if (url != null)
                {
                    var videoUrls = url.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var videoUrl in videoUrls)
                    {
                        var s = new StoryMedium();
                        s.CreatedAt = DateTime.Now;
                        s.StoryId = st.StoryId;
                        s.StoryPath = videoUrl;
                        s.StoryType = "Video";
                        _CIDbContext.Add(s);
                        _CIDbContext.SaveChanges();
                    }
                }
                return st.StoryId;

            }
            else
            {
                var st = _CIDbContext.Stories.FirstOrDefault(e => e.StoryId == storyId);
                st.MissionId = missionId;
                st.UserId = userId;
                st.Title = title;
                st.Description = description;
                st.Status = "PENDING";
                st.PublishedAt = date;
                st.CreatedAt = DateTime.Now;
                _CIDbContext.Stories.Update(st);
                _CIDbContext.SaveChanges();
                if (url != null)
                {
                    var media = _CIDbContext.StoryMedia.Where(e => e.StoryId == st.StoryId && e.StoryType == "Video").ToList();

                    if (media != null)
                    {
                        foreach (var i in media)
                        {
                            _CIDbContext.Remove(i);
                            _CIDbContext.SaveChanges();
                        }
                    }

                    var videoUrls = url.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var videoUrl in videoUrls)
                    {
                        var s = new StoryMedium();
                        s.CreatedAt = DateTime.Now;
                        s.StoryId = st.StoryId;
                        s.StoryPath = videoUrl;
                        s.StoryType = "Video";
                        _CIDbContext.Add(s);
                        _CIDbContext.SaveChanges();
                    }
                }


                return st.StoryId;


            }


        }
        public void RemoveMedia(long StoryId)
        {
            var storyMedia = _CIDbContext.StoryMedia.Where(e => e.StoryId == StoryId).ToList();
            foreach (var s in storyMedia)
            {
                _CIDbContext.StoryMedia.Remove(s);
                _CIDbContext.SaveChanges();
            }
        }
        public void AddStoryMedia(string mediaType, string mediaPath, long missionId, long userId, long storyId, long sId)
        {

            StoryMedium sm = new StoryMedium();
            sm.StoryId = sId;
            sm.StoryType = mediaType;
            sm.StoryPath = mediaPath;
            _CIDbContext.Add(sm);
            _CIDbContext.SaveChanges();

        }
        public long SaveStory(long missionId, long userId, string title, string description, DateTime date, long storyId, string url)
        {
            if (storyId == 0)
            {
                Story st = new Story();
                st.MissionId = missionId;
                st.UserId = userId;
                st.Title = title;
                st.Description = description;
                st.Status = "DRAFT";
                st.PublishedAt = date;
                st.CreatedAt = DateTime.Now;
                _CIDbContext.Stories.Add(st);
                _CIDbContext.SaveChanges();
                if (url != null)
                {
                    var videoUrls = url.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var videoUrl in videoUrls)
                    {
                        var s = new StoryMedium();
                        s.CreatedAt = DateTime.Now;
                        s.StoryId = st.StoryId;
                        s.StoryPath = videoUrl;
                        s.StoryType = "Video";
                        _CIDbContext.Add(s);
                        _CIDbContext.SaveChanges();
                    }
                }
                return st.StoryId;
            }
            else
            {
                var story = _CIDbContext.Stories.Where(s => s.StoryId == storyId).FirstOrDefault();
                story.MissionId = missionId;
                story.UserId = userId;
                story.Title = title;
                story.PublishedAt = date;
                story.Description = description;
                story.UpdatedAt = DateTime.Now;
                _CIDbContext.Update(story);
                _CIDbContext.SaveChanges();

                if (url != null)
                {
                    var media = _CIDbContext.StoryMedia.Where(e => e.StoryId == story.StoryId && e.StoryType == "Video").ToList();

                    if (media != null)
                    {
                        foreach (var i in media)
                        {
                            _CIDbContext.Remove(i);
                            _CIDbContext.SaveChanges();
                        }
                    }

                    var videoUrls = url.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var videoUrl in videoUrls)
                    {
                        var s = new StoryMedium();
                        s.CreatedAt = DateTime.Now;
                        s.StoryId = story.StoryId;
                        s.StoryPath = videoUrl;
                        s.StoryType = "Video";
                        _CIDbContext.Add(s);
                        _CIDbContext.SaveChanges();
                    }
                }

                return story.StoryId;

            }


        }
        public List<StoryMedium> storyMedia()
        {
            return _CIDbContext.StoryMedia.ToList();
        }
        public void DraftDelete(int storyId)
        {
            var story = _CIDbContext.Stories.Where(e => e.StoryId == storyId).FirstOrDefault();
            var stroryMedia = _CIDbContext.StoryMedia.Where(e => e.StoryId == story.StoryId).ToList();
            if (storyMedia != null)
            {
                foreach (var s in stroryMedia)
                {
                    _CIDbContext.Remove(s);
                }
            }

            _CIDbContext.Remove(story);
            _CIDbContext.SaveChanges();
        }

        public void cmtdetele(int cmtId, int userId)
        {
            var cmt = _CIDbContext.Comments.Where(c => c.CommentId == cmtId).FirstOrDefault();
            _CIDbContext.Comments.Remove(cmt);
            _CIDbContext.SaveChanges();
        }
        public void AddTime(long missionId, int userId, int? hour, int? min, int? action, DateTime date, string? notes, long TimesheetId)
        {
            if (TimesheetId == 0)
            {
                if (hour != null && min != null)
                {
                    Timesheet ts = new Timesheet();
                    ts.MissionId = missionId;
                    ts.UserId = userId;
                    ts.TimesheetTime = hour + ":" + min;
                    ts.DateVolunteered = date;
                    ts.Notes = notes;
                    ts.Status = "1";
                    ts.CreatedAt = DateTime.Now;
                    _CIDbContext.Timesheets.Add(ts);
                    _CIDbContext.SaveChanges();
                }
                else
                {
                    Timesheet ts = new Timesheet();
                    ts.MissionId = missionId;
                    ts.UserId = userId;
                    ts.DateVolunteered = date;
                    ts.Action = action;
                    ts.Notes = notes;
                    ts.Status = "1";
                    ts.CreatedAt = DateTime.Now;
                    _CIDbContext.Timesheets.Add(ts);
                    _CIDbContext.SaveChanges();
                }
            }
            else
            {
                if (hour != null && min != null)
                {
                    Timesheet ts = _CIDbContext.Timesheets.Where(e => e.TimesheetId == TimesheetId).FirstOrDefault();
                    ts.MissionId = missionId;
                    ts.UserId = userId;
                    ts.TimesheetTime = hour + ":" + min;
                    ts.DateVolunteered = date;
                    ts.Notes = notes;
                    ts.Status = "1";
                    ts.UpdatedAt = DateTime.Now;
                    _CIDbContext.Timesheets.Update(ts);
                    _CIDbContext.SaveChanges();
                }
                else
                {
                    Timesheet ts = _CIDbContext.Timesheets.Where(e => e.TimesheetId == TimesheetId).FirstOrDefault();
                    ts.MissionId = missionId;
                    ts.UserId = userId;
                    ts.DateVolunteered = date;
                    ts.Action = action;
                    ts.Notes = notes;
                    ts.Status = "1";
                    ts.UpdatedAt = DateTime.Now;
                    _CIDbContext.Update(ts);
                    _CIDbContext.SaveChanges();
                }
            }

        }

        public void DeleteTimeSheet(int id)
        {
            var time = _CIDbContext.Timesheets.Where(e => e.TimesheetId == id).FirstOrDefault();
            time.DeletedAt = DateTime.Now;
            time.Status = "Deleted";
            _CIDbContext.Update(time);
            _CIDbContext.SaveChanges();
        }

        public void ChangePassword(string NewPsw, string CnfPsw, int Userid)
        {
            User u = _CIDbContext.Users.Where(u => u.UserId == Userid).FirstOrDefault();
            if (u != null)
            {
                u.Password = NewPsw;
                u.UpdatedAt = DateTime.Now;
                _CIDbContext.Update(u);
                _CIDbContext.SaveChanges();
            }
        }
        public List<MissionSkill> GetMissionSkill()
        {
            return _CIDbContext.MissionSkills.ToList();
        }

        public List<UserSkill> GetUserSkill(int userId)
        {
            return _CIDbContext.UserSkills.Where(e => e.UserId == userId).ToList();
        }
        public Skill SkillStatus(int skillid)
        {
            var s = _CIDbContext.Skills.FirstOrDefault(e => e.SkillId == skillid);

            if (s.Status == "1")
            {
                s.Status = "0";
            }
            else
            {
                s.Status = "1";
            }
            s.UpdatedAt = DateTime.Now;
            _CIDbContext.Update(s);
            _CIDbContext.SaveChanges();
            return s;
        }
        public void AddUserSkills(long SkillId, int UserId)
        {
            UserSkill skill = new UserSkill();
            skill.SkillId = SkillId;
            skill.UserId = UserId;
            _CIDbContext.Add(skill);
            _CIDbContext.SaveChanges();
        }
        public MissionTheme AddMissionTheme(string theme, int status)
        {
            MissionTheme mt = new MissionTheme();
            if (theme != null)
            {
                mt.Title = theme;
                if (status == 1)
                {
                    mt.Status = 1;
                }
                else
                {
                    mt.Status = 2;
                }
                _CIDbContext.Add(mt);
                _CIDbContext.SaveChanges();
            }
            return mt;

        }
        public MissionTheme DeleteTheme(int themeId)
        {
            MissionTheme m = _CIDbContext.MissionThemes.FirstOrDefault(e => e.MissionThemeId == themeId);
            if (m != null)
            {
                _CIDbContext.Remove(m);
                _CIDbContext.SaveChanges();
            }
            return m;
        }
        public MissionTheme GetTheme(int themeId)
        {
            MissionTheme m = _CIDbContext.MissionThemes.FirstOrDefault(e => e.MissionThemeId == themeId);
            return m;
        }
        public MissionTheme EditTheme(string singleTheme, int ThemeId, int status)
        {
            MissionTheme m = _CIDbContext.MissionThemes.FirstOrDefault(e => e.MissionThemeId == ThemeId);
            m.Title = singleTheme;
            if (status == 1)
            {
                m.Status = 1;
            }
            else
            {
                m.Status = 2;
            }
            m.UpdatedAt = DateTime.Now;
            _CIDbContext.Update(m);
            _CIDbContext.SaveChanges();
            return m;

        }


        //skill
        public Skill DeleteSkill(int skillid)
        {
            Skill m = _CIDbContext.Skills.FirstOrDefault(e => e.SkillId == skillid);
            if (m != null)
            {
                _CIDbContext.Remove(m);
                _CIDbContext.SaveChanges();
            }
            return m;
        }
        public Skill AddSkill(string skill,int status)
        {
            Skill mt = new Skill();
            if (skill != null)
            {
                mt.SkillName = skill;
                mt.Status = Convert.ToString(status);
                _CIDbContext.Add(mt);
                _CIDbContext.SaveChanges();
            }
            return mt;

        }
        public Skill GetSkill(int skillid)
        {
            return _CIDbContext.Skills.FirstOrDefault(e => e.SkillId == skillid);
        }
        public Skill EditSkill(string singleSkill, int skillId, int s)
        {
            Skill m = _CIDbContext.Skills.FirstOrDefault(e => e.SkillId == skillId);
            m.SkillName = singleSkill;
            m.Status = Convert.ToString(s);
            m.UpdatedAt = DateTime.Now;
            _CIDbContext.Update(m);
            _CIDbContext.SaveChanges();
            return m;
        }
        public MissionApplication approveApplication(int id)
        {
            var app = _CIDbContext.MissionApplications.FirstOrDefault(e => e.MissionApplicationId == id);
            app.ApprovalStatus = "Approve";
            _CIDbContext.Update(app);
            _CIDbContext.SaveChanges();

            var mission = _CIDbContext.Missions.FirstOrDefault(e => e.MissionId == app.MissionId);
            var s = int.Parse(mission.Seats);
            var ss = s - 1;
            mission.Seats = Convert.ToString(ss);
            _CIDbContext.Update(mission);
            _CIDbContext.SaveChanges();

            return app;
        }
        public MissionApplication rejectApplication(int id)
        {
            var app = _CIDbContext.MissionApplications.FirstOrDefault(e => e.MissionApplicationId == id);
            app.ApprovalStatus = "Rejected";
            _CIDbContext.Update(app);
            _CIDbContext.SaveChanges();
            return app;
        }
        public List<CmsPage> GetCmsPage()
        {
            return _CIDbContext.CmsPages.Where(e => e.DeletedAt == null).ToList();
        }
        public CmsPage GetCmsPageById(int id)
        {
            return _CIDbContext.CmsPages.FirstOrDefault(e => e.CmsPageId == id);
        }
        public CmsPage DeleteCmsPageById(int id)
        {
            var cms = _CIDbContext.CmsPages.FirstOrDefault(e => e.CmsPageId == id);
            cms.DeletedAt = DateTime.Now;
            cms.Status = "In-Active";
            _CIDbContext.Update(cms);
            _CIDbContext.SaveChanges();

            return cms;
        }
        public bool EditCmsPage(int id, string tiitle, string desc, string slug, string status)
        {
            var cms = _CIDbContext.CmsPages.FirstOrDefault(e => e.CmsPageId == id);
            cms.Title = tiitle;
            cms.Description = desc;
            cms.Status = status;
            cms.Slug = slug;
            cms.UpdatedAt = DateTime.Now;
            _CIDbContext.Update(cms);
            _CIDbContext.SaveChanges(true);
            return true;
        }
        public bool AddCmsPage(string tiitle, string desc, string slug, string status)
        {
            CmsPage cms = new CmsPage();
            cms.Title = tiitle;
            cms.Description = desc;
            cms.Status = status;
            cms.Slug = slug;
            cms.CreatedAt = DateTime.Now;
            _CIDbContext.Add(cms);
            _CIDbContext.SaveChanges(true);
            return true;
        }
        public bool approveStory(int id)
        {
            Story s = _CIDbContext.Stories.Where(e => e.StoryId == id).FirstOrDefault();
            s.Status = "APPROVE";
            _CIDbContext.Update(s);
            _CIDbContext.SaveChanges();
            return true;
        }
        public bool rejectStory(int id)
        {
            Story s = _CIDbContext.Stories.Where(e => e.StoryId == id).FirstOrDefault();
            s.Status = "REJECT";
            _CIDbContext.Update(s);
            _CIDbContext.SaveChanges();
            return true;
        }
        public bool DeleteStory(int id)
        {
            Story s = _CIDbContext.Stories.Where(e => e.StoryId == id).FirstOrDefault();
            s.Status = "DELETE";
            s.DeletedAt = DateTime.Now;
            _CIDbContext.Update(s);
            _CIDbContext.SaveChanges();
            return true;
        }
        public bool UpdateUser(int Id, string Img, string Fname, string Lname, string Email, string Password,
               string Employeeid, string Department, string Profiletext, string status, int Country, int City)
        {


            if (Id != 0)
            {
                var user = _CIDbContext.Users.FirstOrDefault(e => e.UserId == Id);

                user.FirstName = Fname;
                user.LastName = Lname;
                user.Email = Email;
                user.Password = Password;
                user.EmployeeId = Employeeid;
                user.Department = Department;
                user.ProfileText = Profiletext;
                user.Status = status;
                user.CountryId = Country;
                user.CityId = City;
                if (Img != null)
                {

                    user.Avatar = Img;
                }

                _CIDbContext.Update(user);
                _CIDbContext.SaveChanges();
            }
            else
            {
                User user = new User();

                user.FirstName = Fname;
                user.LastName = Lname;
                user.Email = Email;
                user.Password = Password;
                user.EmployeeId = Employeeid;
                user.Department = Department;
                user.ProfileText = Profiletext;
                user.Status = status;
                user.CountryId = Country;
                user.CityId = City;
                if (Img != null)
                {

                    user.Avatar = Img;
                }

                _CIDbContext.Add(user);
                _CIDbContext.SaveChanges();
            }


            return true;
        }
        public bool DeleteUserById(int Id)
        {
            var u = _CIDbContext.Users.FirstOrDefault(e => e.UserId == Id);
            u.DeletedAt = DateTime.Now;
            _CIDbContext.Update(u);
            _CIDbContext.SaveChanges();
            return true;
        }

        public Mission GetMissionNtId(int id)
        {
            Mission m = _CIDbContext.Missions.FirstOrDefault(e => e.MissionId == id);
            return m;
        }
        public List<MissionMedium> GetMissionMediaById(int id)
        {
            return _CIDbContext.MissionMedia.Where(e => e.MissionId == id).ToList();
        }
        public List<MissionSkill> GetMissionSkillById(int id)
        {
            return _CIDbContext.MissionSkills.Where(e => e.MissionId == id).ToList();
        }
        public bool UpdateMissionSkill(long[] skills, int id)
        {
            if (id != 0)
            {
                foreach (var i in skills)
                {
                    MissionSkill m = new MissionSkill();
                    m.SkillId = i;
                    m.MissionId = id;
                    _CIDbContext.Add(m);
                    _CIDbContext.SaveChanges();
                }
            }

            return true;
        }

        public bool AddMission(MissionVM mission, IFormFileCollection? files)
        {
            Mission m = new Mission();
            m.Title = mission.title;
            m.ShortDescription = mission.shortDescription;
            m.Description = mission.description;
            m.CountryId = mission.countryId;
            m.CityId = mission.cityId;
            m.OrganizationName = mission.OrganisationName;
            m.OrganizationDetail = mission.OrganisationDetail;
            m.MissionType = mission.missionType;
            m.StartDate = mission.startDate;
            m.EndDate = mission.endDate;
            m.Seats = Convert.ToString(mission.seats);
            m.ThemeId = mission.missionThemeId;
            m.Deadline = mission.deadline;
            _CIDbContext.Add(m);
            _CIDbContext.SaveChanges();

            if (mission.selectedSkills != null)
            {
                foreach (var s in mission.selectedSkills)
                {
                    MissionSkill ms = new MissionSkill();
                    ms.MissionId = m.MissionId;
                    ms.SkillId = Convert.ToInt64(s);
                    ms.CreatedAt = DateTime.Now;
                    _CIDbContext.Add(ms);
                    _CIDbContext.SaveChanges();
                }


            }
            if (mission.missionType == "Time")
            {
                var missiongoal = new GoalMission();
                missiongoal.MissionId = m.MissionId;
                missiongoal.GoalObjectiveText = "default";
                missiongoal.GoalValue = "0";
                _CIDbContext.Add(missiongoal);
                _CIDbContext.SaveChanges();
            }
            else
            {
                var missiongoal = new GoalMission();
                missiongoal.MissionId = m.MissionId;
                missiongoal.GoalObjectiveText = mission.goalObjectiveText;
                missiongoal.GoalValue = mission.goalValue;
                _CIDbContext.Add(missiongoal);
                _CIDbContext.SaveChanges();
            }
            if (mission.url != null)
            {
                var videoUrls = mission.url.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var videoUrl in videoUrls)
                {
                    var missionMedia = new MissionMedium();
                    missionMedia.CreatedAt = DateTime.Now;
                    missionMedia.MissionId = m.MissionId;
                    missionMedia.MediaPath = videoUrl;
                    missionMedia.MediaType = "Video";
                    _CIDbContext.Add(missionMedia);
                    _CIDbContext.SaveChanges();
                }
            }
            if (files != null)
            {
                foreach (var file in files)
                {
                    if (file.ContentType.Contains("pdf") || file.ContentType.Contains("docx") || file.ContentType.Contains("xlxs"))
                    {
                        byte[] fileBytes;
                        using (var stream = new MemoryStream())
                        {
                            file.CopyTo(stream);
                            fileBytes = stream.ToArray();
                        }
                        var missionmedia = new MissionDocument();
                        missionmedia.MissionId = m.MissionId;
                        var ext = file.ContentType.Split("/");
                        missionmedia.DocumentType = ext[1];
                        missionmedia.DocumentName = file.FileName;
                        missionmedia.DocumentPath = fileBytes;
                        _CIDbContext.Add(missionmedia);

                    }
                    else
                    {
                        byte[] fileBytes;
                        using (var stream = new MemoryStream())
                        {
                            file.CopyTo(stream);
                            fileBytes = stream.ToArray();
                        }
                        string base64String = Convert.ToBase64String(fileBytes);
                        var missionmedia = new MissionMedium();
                        missionmedia.MissionId = m.MissionId;
                        var ext = file.ContentType.Split("/");

                        missionmedia.MediaType = ext[1];
                        missionmedia.MediaInBytes = fileBytes;
                        missionmedia.MediaName = file.FileName;
                        missionmedia.MediaPath = "data:image/" + ext[1] + ";base64," + base64String;
                        _CIDbContext.Add(missionmedia);

                    }
                }
            }
            _CIDbContext.SaveChanges();
            return true;
        }
        public bool EditMission(MissionVM mission, IFormFileCollection? files)
        {
            Mission m = _CIDbContext.Missions.FirstOrDefault(f => f.MissionId == mission.missionId);
            m.Title = mission.title;
            m.ShortDescription = mission.shortDescription;
            m.Description = mission.description;
            m.CountryId = mission.countryId;
            m.CityId = mission.cityId;
            m.OrganizationName = mission.OrganisationName;
            m.OrganizationDetail = mission.OrganisationDetail;
            m.MissionType = mission.missionType;
            m.StartDate = mission.startDate;
            m.EndDate = mission.endDate;
            m.Seats = Convert.ToString(mission.seats);
            m.Deadline = mission.deadline;
            m.Availability = mission.availability;
            if (mission.missionType == "Time")
            {
                var missiongoal = _CIDbContext.GoalMissions.FirstOrDefault(g => g.MissionId == m.MissionId);
                missiongoal.MissionId = m.MissionId;
                missiongoal.GoalObjectiveText = "default";
                missiongoal.GoalValue = "0";
                _CIDbContext.Update(missiongoal);
                _CIDbContext.SaveChanges();
            }
            else
            {
                var missiongoal = _CIDbContext.GoalMissions.FirstOrDefault(g => g.MissionId == m.MissionId);
                missiongoal.MissionId = m.MissionId;
                missiongoal.GoalObjectiveText = mission.goalObjectiveText;
                missiongoal.GoalValue = mission.goalValue;
                _CIDbContext.Update(missiongoal);
                _CIDbContext.SaveChanges();
            }
            m.ThemeId = mission.missionThemeId;
            _CIDbContext.Update(m);
            _CIDbContext.SaveChanges();

            if (mission.selectedSkills != null)
            {
                var missionskill = _CIDbContext.MissionSkills.Where(e => e.MissionId == mission.missionId).ToList();

                if (missionskill.Count() > 0)
                {
                    foreach (var skill in missionskill)
                    {
                        _CIDbContext.Remove(skill);
                        _CIDbContext.SaveChanges(true);
                    }
                }

                foreach (var s in mission.selectedSkills)
                {
                    MissionSkill ms = new MissionSkill();
                    ms.MissionId = m.MissionId;
                    ms.SkillId = Convert.ToInt64(s);
                    ms.CreatedAt = DateTime.Now;
                    _CIDbContext.Add(ms);
                    _CIDbContext.SaveChanges();
                }


            }



            if (files != null)
            {
                foreach (var file in files)
                {
                    if (file.ContentType.Contains("pdf") || file.ContentType.Contains("docx") || file.ContentType.Contains("xlxs"))
                    {
                        byte[] fileBytes;
                        using (var stream = new MemoryStream())
                        {
                            file.CopyTo(stream);
                            fileBytes = stream.ToArray();
                        }
                        var missionmedia = new MissionDocument();
                        missionmedia.MissionId = m.MissionId;
                        var ext = file.ContentType.Split("/");
                        missionmedia.DocumentType = ext[1];
                        missionmedia.DocumentName = file.FileName;
                        missionmedia.DocumentPath = fileBytes;
                        _CIDbContext.Add(missionmedia);

                    }
                    else
                    {
                        byte[] fileBytes;
                        using (var stream = new MemoryStream())
                        {
                            file.CopyTo(stream);
                            fileBytes = stream.ToArray();
                        }
                        string base64String = Convert.ToBase64String(fileBytes);
                        var missionmedia = new MissionMedium();
                        missionmedia.MissionId = m.MissionId;
                        var ext = file.ContentType.Split("/");
                        missionmedia.MediaType = ext[1];
                        missionmedia.MediaInBytes = fileBytes;
                        missionmedia.MediaName = file.FileName;
                        missionmedia.MediaPath = "data:image/" + ext[1] + ";base64," + base64String;
                        _CIDbContext.Add(missionmedia);

                    }
                }
            }
            if (mission.url != null)
            {
                var video = _CIDbContext.MissionMedia.Where(e => e.MissionId == m.MissionId && e.MediaType == "Video").ToList();
                if (video != null)
                {
                    foreach (var v in video)
                    {
                        _CIDbContext.Remove(v);
                        _CIDbContext.SaveChanges();
                    }
                }

                var videoUrls = mission.url.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var videoUrl in videoUrls)
                {
                    var missionMedia = new MissionMedium();
                    missionMedia.CreatedAt = DateTime.Now;
                    missionMedia.MissionId = m.MissionId;
                    missionMedia.MediaPath = videoUrl;
                    missionMedia.MediaType = "Video";
                    _CIDbContext.Add(missionMedia);
                    _CIDbContext.SaveChanges();
                }
            }
            _CIDbContext.SaveChanges();
            return true;
        }

        public void delDoc(long id)
        {
            var doc = _CIDbContext.MissionDocuments.FirstOrDefault(d => d.MissionDocumentId == id);
            doc.DeletedAt = DateTime.Now;
            _CIDbContext.Update(doc);
            _CIDbContext.SaveChanges();
        }
        public void delImg(long id)
        {
            var doc = _CIDbContext.MissionMedia.FirstOrDefault(d => d.MissionMediaId == id);
            doc.DeletedAt = DateTime.Now;
            _CIDbContext.Update(doc);
            _CIDbContext.SaveChanges();
        }

        public void DeleteMission(long missionId)
        {
            var mission = _CIDbContext.Missions.FirstOrDefault(t => t.MissionId == missionId);
            mission.DeletedAt = DateTime.Now;
            mission.UpdatedAt = DateTime.Now;
            _CIDbContext.Update(mission);
            var missionApplication = _CIDbContext.MissionApplications.Where(t => t.MissionId == missionId).ToList();
            foreach (var timesheet in missionApplication)
            {
                timesheet.DeletedAt = DateTime.Now;
                timesheet.UpdatedAt = DateTime.Now;
                _CIDbContext.Update(timesheet);

            }
            var skills = _CIDbContext.MissionSkills.Where(t => t.MissionId == missionId).ToList();
            foreach (var skill in skills)
            {
                skill.DeletedAt = DateTime.Now;
                skill.UpdatedAt = DateTime.Now;
                _CIDbContext.Update(skill);

            }
            var timesheets = _CIDbContext.Timesheets.Where(t => t.MissionId == missionId).ToList();
            foreach (var timesheet in timesheets)
            {
                timesheet.DeletedAt = DateTime.Now;
                timesheet.UpdatedAt = DateTime.Now;
                _CIDbContext.Update(timesheet);

            }
            var story = _CIDbContext.Stories.Where(t => t.MissionId == missionId).ToList();
            foreach (var skill in story)
            {
                skill.DeletedAt = DateTime.Now;
                skill.UpdatedAt = DateTime.Now;
                _CIDbContext.Update(skill);

            }
            var favmission = _CIDbContext.FavoriteMissions.Where(t => t.MissionId == missionId).ToList();
            foreach (var timesheet in favmission)
            {
                timesheet.DeletedAt = DateTime.Now;
                timesheet.UpdatedAt = DateTime.Now;
                _CIDbContext.Update(timesheet);

            }
            var comment = _CIDbContext.Comments.Where(t => t.MissionId == missionId).ToList();
            foreach (var timesheet in comment)
            {
                timesheet.DeletedAt = DateTime.Now;
                timesheet.UpdatedAt = DateTime.Now;
                _CIDbContext.Update(timesheet);

            }
            var missionRating = _CIDbContext.MissionRatings.Where(t => t.MissionId == missionId).ToList();
            foreach (var timesheet in missionRating)
            {
                timesheet.DeletedAt = DateTime.Now;
                timesheet.UpdatedAt = DateTime.Now;
                _CIDbContext.Update(timesheet);

            }
            var missionmedia = _CIDbContext.MissionMedia.Where(t => t.MissionId == missionId).ToList();
            foreach (var timesheet in missionmedia)
            {
                timesheet.DeletedAt = DateTime.Now;
                timesheet.UpdatedAt = DateTime.Now;
                _CIDbContext.Update(timesheet);

            }
            var missiondoc = _CIDbContext.MissionDocuments.Where(t => t.MissionId == missionId).ToList();
            foreach (var timesheet in missiondoc)
            {
                timesheet.DeletedAt = DateTime.Now;
                timesheet.UpdatedAt = DateTime.Now;
                _CIDbContext.Update(timesheet);

            }
            var goalmission = _CIDbContext.GoalMissions.Where(t => t.MissionId == missionId).ToList();
            foreach (var timesheet in goalmission)
            {
                timesheet.DeletedAt = DateTime.Now;
                timesheet.UpdatedAt = DateTime.Now;
                _CIDbContext.Update(timesheet);

            }
            _CIDbContext.SaveChanges();
        }

        public Banner GetBannerById(int id)
        {
            var banner = _CIDbContext.Banners.FirstOrDefault(e => e.BannerId == id);
            return banner;
        }
        public Banner CheckBannerSortOrder(int sortOrder)
        {
            var s = _CIDbContext.Banners.FirstOrDefault(e => e.SortOrder == sortOrder);
            return s;

        }
        public bool AddBanner(int id, string image, string description, int sort)
        {

            if (id == 0)
            {
                Banner banner = new Banner();
                banner.Image = image;
                banner.Text = description;
                banner.SortOrder = sort;
                banner.CreatedAt = DateTime.Now;
                _CIDbContext.Add(banner);
                _CIDbContext.SaveChanges();


            }
            else
            {
                var banner = _CIDbContext.Banners.FirstOrDefault(e => e.BannerId == id);
                if (image != null)
                {

                    banner.Image = image;
                }
                banner.Text = description;
                banner.SortOrder = sort;
                banner.UpdatedAt = DateTime.Now;
                _CIDbContext.Update(banner);
                _CIDbContext.SaveChanges();
            }
            return true;
        }
        public bool DeleteBanner(int id)
        {

            var banner = _CIDbContext.Banners.FirstOrDefault(e => e.BannerId == id);
            banner.DeletedAt = DateTime.Now;
            _CIDbContext.Update(banner);
            _CIDbContext.SaveChanges();
            return true;
        }
        public bool ContactUs(UserProfile userProfile)
        {
            ContactU contactU = new ContactU();
            contactU.UserName = userProfile.Name;
            contactU.Email = userProfile.Email;
            contactU.Subject = userProfile.Subject;
            contactU.Message = userProfile.Message;
            _CIDbContext.Add(contactU);
            _CIDbContext.SaveChanges();
            return true;
        }
    }
}
