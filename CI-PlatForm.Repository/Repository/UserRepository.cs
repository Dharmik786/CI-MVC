using CI_Entity.Models;
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
        public bool Registration(User user)
        {
            var c = _CIDbContext.Users.FirstOrDefault(u => u.Email == user.Email);
            if (c == null)
            {
                _CIDbContext.Users.Add(user);
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
        public List<PasswordReset> passwordResets(string email, string token)
        {
            var c = _CIDbContext.PasswordResets.Where(u => u.Email == email && u.Token == token).ToList();
            //if(c==null)
            //{
            //    //_CIDbContext.Add(c);
            //    _CIDbContext.PasswordResets.Add(c);
            //    _CIDbContext.SaveChanges();
            //}
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
            return _CIDbContext.Timesheets.ToList();
        }
        public List<Comment> comments()
        {
            return _CIDbContext.Comments.ToList();
        }
        public List<MissionApplication> missionApplications()
        {
            return _CIDbContext.MissionApplications.ToList();
        }
    }
}
