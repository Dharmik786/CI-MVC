using CI_Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Entity.ViewModel
{
    public class MissionList
    {
        public List<Mission> mission { get; set; }
        public List<City> cities { get; set; }
        public List<Country> countries { get; set; }
        public List<MissionTheme> missionThemes { get; set; }
        public List<Skill> skills { get; set; }
        public List<MissionMedium> missionMedia { get; set; }
        public List<FavoriteMission> favoriteMissions { get; set; }
        public List<User> Users { get; set; }

        public List<GoalMission> goalMissions { get; set; }
        public List<MissionSkill> missionSkills { get; set; }

        public List<Comment> comments { get; set; }

        //  public List<Mission> missions { get; set; }
        //   public List<City> cities { get; set; }
        // public List<Country> countries { get; set; }
        //   public List<MissionTheme> missionThemes { get; set; }
        //   public List<Skill> skills { get; set; }
        //    public List<MissionApplicatoin> missionApplicatoins { get; set; }
        //    public List<MissionApplicatoin> volunteerMission { get; set; }
        //   public List<MissionApplicatoin> totalMissionApplication { get; set; }
        public List<User> users { get; set; }
        //   public List<MissionMedium> missionMedia { get; set; }
        public List<Mission> relatedMission { get; set; }
        public List<MissionRating> missionRatings { get; set; }
        //public List<FavoriteMission> favoriteMissions { get; set; }
        //public List<MissionSkill> missionSkills { get; set; }
        //public List<GoalMission> goalMissions { get; set; }


        public List<Timesheet> timesheets { get; set; }
        public List<MissionDocument> missionDocuments { get; set; }
        public List<MissionApplication> missionApplications { get; set; }
        public List<Story> stories { get; set; }
        public Mission singleMission { get; set; }

        public int avgrating { get; set; }
        public long missionId { get; set; }
        public long userId { get; set; }
        public Story storydetails { get; set; }
        public int isApplied { get; set; }  
        //public User User { get; set; }

        public int UserId { get; set; }
        public List<MissionApplication> recentVolunteering { get; set; }

        public MissionRating rating { get; set; }
        public long? userRate { get; set; } = null;

        public List<StoryMedium> storyMedia { get;set; }

        //public long MissionId { get; set; }

        //public long ThemeId { get; set; }

        //public long CityId { get; set; }

        //public long CountryId { get; set; }

        //public string Title { get; set; } = null!;

        //public string ShortDescription { get; set; } = null!;

        //public string Description { get; set; } = null!;

        //public DateOnly? StartDate { get; set; }

        //public DateOnly? EndDate { get; set; }

        //public string MissionType { get; set; } = null!;

        //public bool? Status { get; set; }

        //public string? OrganizationName { get; set; }

        //public string? OrganizationDetail { get; set; }

        //public string? Availability { get; set; }

        //public DateTime CreatedAt { get; set; }

        //public DateTime? UpdatedAt { get; set; }

        //public DateTime? DeletedAt { get; set; }

        //public int? Seats { get; set; }

        //public string CityName { get; set; }

        //public string Themetitle { get; set; }
        //public string MediaPath { get; set; }
        //public long FavrtUser { get; set; }
        //public long Favrtmission { get; set; }
        //public long RatedUser { get; set; }
        //public long Ratedmission { get; set; }
        //public int Ratingvalue { get; set; }

        public string title { get; set; }   
        public string editor1 { get; set; }
        public DateTime date { get; set; }


    }
}
