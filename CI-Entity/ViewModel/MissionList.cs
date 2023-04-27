using CI_Entity.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;

//using Microsoft.Build.Framework;

namespace CI_Entity.ViewModel
{
    public class MissionList
    {   
       
        public List<Mission> mission { get; set; }
        public List<City> cities { get; set; }
        public List<Country> countries { get; set; }
        public List<MissionTheme> missionThemes { get; set; }
        public List<MissionDocument> missionDocuments { get; set; } 
        public List<Skill> skills { get; set; }
        public List<MissionMedium> missionMedia { get; set; }
        public List<FavoriteMission> favoriteMissions { get; set; }
        public List<User> Users { get; set; }
        public User user { get; set; }
        public List<GoalMission> goalMissions { get; set; }
        public List<MissionSkill> missionSkills { get; set; }
        public List<GoalMission> goal { get; set; }

        public List<Comment> comments { get; set; }
        public List<User> users { get; set; }
        public List<Mission> relatedMission { get; set; }
        public List<MissionRating> missionRatings { get; set; }

        public List<Timesheet> timesheets { get; set; }
        public List<MissionApplication> missionApplications { get; set; }
        public List<Story> stories { get; set; }
        public Mission singleMission { get; set; }
        public int missionCount { get; set; }

        public int avgrating { get; set; }
        [Required(ErrorMessage = "Select Mission")]
        public long missionId { get; set; }
        public long userId { get; set; }
        public Story storydetails { get; set; }
        public int isApplied { get; set; }

        public int UserId { get; set; }
        public List<MissionApplication> recentVolunteering { get; set; }

        public MissionRating rating { get; set; }
        public long? userRate { get; set; } = null;

        public List<StoryMedium> storyMedia { get; set; }


        public string title { get; set; }
        public string editor1 { get; set; }

       
        public DateTime date { get; set; }
        public List<IFormFile> attachment { get; set; }
        public long storyId { get; set; }

        public Timesheet timesheet { get; set; }
        public List<MissionApplication> Goal { get; set; }
        public List<MissionApplication> Time { get; set; }
        public string? notes { get; set; }
        [Required(ErrorMessage="Hour field is Required")]
        public int hour { get; set; }
        [Required(ErrorMessage = "Minute field is Required")]

        public int? min { get; set; }
        [Required(ErrorMessage = "Please Enter the Action")]

        public int? action { get; set; }
        public long Hidden { get; set; }

    }
}
