using CI_Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Entity.ViewModel
{
    public class AdiminVM
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
        public string singleTheme { get; set; }
        public string singleskill { get; set; }
        public int  ThemeId { get; set; }
        public int skillId { get; set; }


        public string cmstitle { get; set; }
        public string description { get; set; }
        public string slug { get; set; }
        public string status { get; set; }
        public int cmsid { get; set; }

        //User
        public string? firstname { get; set; }
        public string? lastname { get; set; }
        public string? Email { get; set; }
        
    }
}
