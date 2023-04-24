using CI_Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Entity.ViewModel
{
    public class AdminVM
    {
        public List<User> Users { get; set; }
        public List<Mission> Missions { get; set; }
        public List<MissionApplication> missionApplications { get; set; }
        public List<Story> stories { get; set; }
        public List<Skill> skills { get; set; }
        public List<MissionTheme> missionThemes { get; set; }
        public List<MissionSkill> missionSkills { get; set; }
        public List<Country> countries { get; set; }
        public List<City> cities { get; set; }
        public List<CmsPage> cmsPages { get; set; }
        public List<Banner> banner { get; set; }
        public string singleTheme { get; set; }
        public string singleskill { get; set; }
        public int ThemeId { get; set; }
        public int skillId { get; set; }

        public List<long> selectedSkills { get; set; }
        public string cmstitle { get; set; }
        public string description { get; set; }
        public string slug { get; set; }
        public string status { get; set; }
        public int cmsid { get; set; }

        //User
        public string? firstname { get; set; }
        public string? lastname { get; set; }
        public string? Email { get; set; }
        //Misison

        public string title { get; set; }
        public string shortDescription { get; set; }

        //public string description { get; set; }
        public long countryId { get; set; }
        public long cityId { get; set; }
        public string OrganisationName { get; set; }
        public string OrganisationDetail { get; set; }
        public string missionType { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int seats { get; set; }
        public DateTime deadline { get; set; }
        public long missionThemeId { get; set; }
        public List<MissionMedium> missionMedia { get; set; }
        public long missionId { get; set; }
        public List<Skill> RemainingSkill { get; set; }


    }
}
