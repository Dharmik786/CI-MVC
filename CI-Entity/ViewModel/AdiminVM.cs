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

        public string singleTheme { get; set; }
        public string singleskill { get; set; }
        public int  ThemeId { get; set; }
        public int skillId { get; set; }
    }
}
