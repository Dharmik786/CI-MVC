using CI_Entity.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Entity.ViewModel
{
    public class StoryModel
    {
        public List<StoryMedium> storyMedia { get; set; }
        public List<MissionApplication> missionApplications { get; set; }
        [Required(ErrorMessage = "Mission is a Required field.")]
        public long missionId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string title { get; set; }
        [Required(ErrorMessage = "Date is required")]
        public DateTime date { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required @@")]
        public string editor1 { get; set; }
        public string url { get; set; }
        public List<IFormFile> attachment { get; set; }
        public long storyId { get; set; }
        public List<Mission> mission { get; set; }
        public List<string> ImgList { get; set; }

    }
}
