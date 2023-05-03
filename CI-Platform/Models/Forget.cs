using CI_Entity.Models;
using System.ComponentModel.DataAnnotations;

namespace CI_Platform.Models
{
    public class Forget
    {
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }
        public List<Banner> Banners { get; set; }
    }
}
