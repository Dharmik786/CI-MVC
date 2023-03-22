﻿using CI_Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PlatForm.Repository.Interface
{
    public interface IUserInterface
    {
        public bool Registration(User user);
        public User Login(string Email, string Password);
        public User Forget(string Email);

        public List<PasswordReset> passwordResets(string email, string token);
        //public PasswordReset AddPassToken(string email, string token);

        public List<User> user();
        public List<Mission> mission();
        public List<Country> countries();
        public List<City> cities();
        public List<Skill> skills();
        public List<MissionTheme> missionThemes();

        public List<GoalMission> goalMissions();
        public List<FavoriteMission> favoriteMissions();
        public List<MissionRating> MissionRatings();
        public List<MissionMedium> missionMedia();
        public List<Timesheet> timesheets();
        public List<Comment> comments();

        public List<MissionApplication> missionApplications();


       
    }
}
