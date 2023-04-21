using CI_Entity.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Entity.ViewModel
{
    public class MissionVM
    {
        public List<Country> countries { get; set; }
        public List<City> cities { get; set; }
        public List<Mission> Missions { get; set; }
        public List<Skill> skills { get; set; }
        public List<string> selectedSkills { get; set; }
        public List<MissionTheme> missionThemes { get; set; }
        public List<MissionSkill> missionSkills { get; set; }
        public string title { get; set; }
        public string shortDescription { get; set; }

        public string description { get; set; }
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

        public List<IFormFile> Images { get; set; }
        public List<IFormFile> Docs { get; set; }
    }
}
