using CI_Entity.Models;
using CI_Entity.ViewModel;
using CI_PlatForm.Repository.Interface;
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
            return _CIDbContext.Users.Where(u => u.Email == Email && u.Password == Password).FirstOrDefault();
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
            return _CIDbContext.Missions.ToList();
        }

        public List<User> user()
        {
            return _CIDbContext.Users.ToList();
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
            return _CIDbContext.Timesheets.Where(e=>e.DeletedAt==null).ToList();
        }
        public List<Comment> comments()
        {
            return _CIDbContext.Comments.ToList();
        }
        public List<MissionApplication> missionApplications()
        {
            return _CIDbContext.MissionApplications.ToList();
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
            am.ApprovalStatus = "1";
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

        public long SubmitStory(long missionId, long userId, string title, string description, DateTime date, long storyId)
        {
            if (storyId == 0)
            {
                Story st = new Story();
                st.MissionId = missionId;
                st.UserId = userId;
                st.Title = title;
                st.Description = description;
                st.Status = "1";
                st.CreatedAt = date;
                _CIDbContext.Stories.Add(st);
                _CIDbContext.SaveChanges();
                return st.StoryId;
            }
            else
            {
                var st = _CIDbContext.Stories.FirstOrDefault(e => e.StoryId == storyId);
                st.MissionId = missionId;
                st.UserId = userId;
                st.Title = title;
                st.Description = description;
                st.Status = "1";
                st.CreatedAt = date;
                _CIDbContext.Stories.Update(st);
                _CIDbContext.SaveChanges();
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
        public long SaveStory(long missionId, long userId, string title, string description, DateTime date, long storyId)
        {
            if (storyId == 0)
            {
                Story st = new Story();
                st.MissionId = missionId;
                st.UserId = userId;
                st.Title = title;
                st.Description = description;
                st.Status = "DRAFT";
                st.CreatedAt = date;
                _CIDbContext.Stories.Add(st);
                _CIDbContext.SaveChanges();
                return st.StoryId;
            }
            else
            {
                var story = _CIDbContext.Stories.Where(s => s.StoryId == storyId).FirstOrDefault();
                story.MissionId = missionId;
                story.UserId = userId;
                story.Title = title;
                story.Description = description;
                story.UpdatedAt = DateTime.Now;
                _CIDbContext.Update(story);
                _CIDbContext.SaveChanges();
                return story.StoryId;

            }


        }
        public List<StoryMedium> storyMedia()
        {
            return _CIDbContext.StoryMedia.ToList();
        }
    
        public void cmtdetele(int cmtId, int userId)
        {
            var cmt = _CIDbContext.Comments.Where(c=>c.CommentId==cmtId).FirstOrDefault();
            _CIDbContext.Comments.Remove(cmt);
            _CIDbContext.SaveChanges();
        }
        public void AddTime(long missionId, int userId, int? hour, int? min,int? action, DateTime date, string? notes)
        {
            if (hour != null && min != null)
            {
                Timesheet ts = new Timesheet();
                ts.MissionId = missionId;
                ts.UserId = userId;
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

        public void DeleteTimeSheet(int id)
        {
            var time = _CIDbContext.Timesheets.Where(e=>e.TimesheetId==id).FirstOrDefault();
            time.DeletedAt = DateTime.Now;
            time.Status = "Deleted";
            _CIDbContext.Update(time);
            _CIDbContext.SaveChanges();
        }

    }
}
