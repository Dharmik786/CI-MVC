using CI_Entity.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Entity.ViewModel
{
    public class MissionVM
    {
        public List<Country> countries { get; set; }
        public List<IFormFile> DocFiles { get; set; }
        public List<MissionMedium> ImageFiles { get; set; }
        public string url { get; set; }
        public List<City> cities { get; set; }
        public List<Mission> Missions { get; set; }
        public List<Skill> skills { get; set; }
        public List<string> selectedSkills { get; set; }
        public List<MissionTheme> missionThemes { get; set; }
        public List<MissionSkill> missionSkills { get; set; }
        [Required(ErrorMessage = "Title is a Required field.")]
        public string title { get; set; }
        public string goalObjectiveText { get; set; }
        public string goalValue { get; set; }
        [Required(ErrorMessage = "Short Discription is a Required field.")]
        [StringLength(50, MinimumLength = 25, ErrorMessage = "Short Description must be atleast 25 characters")]
        public string shortDescription { get; set; }
        [Required(ErrorMessage = "Discription is a Required field.")]
        [StringLength(50, MinimumLength = 25, ErrorMessage = "Description must be atleast 25 characters")]
        public string description { get; set; }
        [Required(ErrorMessage = "Country Name is a Required field.")]
        public long countryId { get; set; }
        [Required(ErrorMessage = "City Name is a Required field.")]
        public long cityId { get; set; }
        [Required(ErrorMessage = "Organisation Name is a Required field.")]

        public string OrganisationName { get; set; }
        [Required(ErrorMessage = "Organisation Details is a Required field.")]

        public string OrganisationDetail { get; set; }
        [Required(ErrorMessage = "Select Missin Type")]

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
