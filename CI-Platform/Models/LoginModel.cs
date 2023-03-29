using System.ComponentModel.DataAnnotations;
namespace CI.Models
{
    public class Login
    {
        //[Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

    }
}