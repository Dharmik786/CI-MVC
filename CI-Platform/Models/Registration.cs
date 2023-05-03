﻿using CI_Entity.Models;
using System.ComponentModel.DataAnnotations;

namespace CI_Platform.Models
{
    public class Registration
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "The First Name field is required.")]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The Email field is required.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,3}$", ErrorMessage = "Please Provide Valid Email")]
        //[Remote(action: "EmailAlreadyExists", controller: "Login")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "The Phone Number is required")]
        //[StringLength(10, ErrorMessage = "Phone number is invalid")]
        //[RegularExpression(@"^(\+\d{1, 2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$,ErrorMeaadae="")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Phone Number not valid")]
        public long PhoneNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The Password field is required.")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password should contain atleast 8 charachter")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Password should contain atleast one Capital letter , one small case letter, one Digit and one special symbol")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public List<Banner> banners { get; set; }   
    }
}