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
    public class UserProfile
    {
        public User user { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide Old Password")]
        public string OldPsw { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password should contain atleast 8 charachter")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Password should contain atleast one Capital letter , one small case letter, one Digit and one special symbol")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public List<Country> countries { get; set; }
        public List<City> cities { get; set; }
        public List<Skill> skills { get; set; }
        public List<UserSkill> Userskills { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeId { get; set; }
        public string Manger { get; set; }
        public string Title { get; set; }
        public string Department { get; set; }
        public string MyProfile { get; set; }
        public string WhyIVol { get; set; }
        public int City { get; set; }
        public int Country { get; set; }
        public string Availablity { get; set; }
        public string LinkedIn { get; set; }
        public IFormFile Avatar { get; set; }


    }
}
