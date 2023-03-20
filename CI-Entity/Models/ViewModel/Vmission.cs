using CI_Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Entity.Models.ViewModel
{
    internal class Vmission
    {
        public List<Mission> missions { get; set; }
        public List<City> cities { get; set; }
        public List<Country> countries { get; set; }
        public List<MissionTheme> missionThemes { get; set; }
        public List<Skill> skills { get; set; }
        // public List<MissionApplicatoin> missionApplicatoins { get; set; }
        //public List<MissionApplicatoin> volunteerMission { get; set; }
        //public List<MissionApplicatoin> totalMissionApplication { get; set; }
        public List<User> users { get; set; }
        public List<MissionMedium> missionMedia { get; set; }
        public List<Mission> relatedMission { get; set; }
        public List<MissionRating> missionRatings { get; set; }
        public List<FavoriteMission> favoriteMissions { get; set; }
        public List<MissionSkill> missionSkills { get; set; }
        public List<GoalMission> goalMissions { get; set; }
        public List<Comment> comments { get; set; }
        public List<Timesheet> timesheets { get; set; }
        public List<MissionDocument> missionDocuments { get; set; }
        public Mission singleMission { get; set; }
    }
}
