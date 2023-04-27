using CI_Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Entity.ViewModel
{
    public class LoginVM
    {  //[Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }

       [Required(ErrorMessage = "Please Enter The Password")]
        public string? Password { get; set; }


        public List<Banner> banners { get; set; }
    }
}