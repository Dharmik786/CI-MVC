using System.ComponentModel.DataAnnotations;

namespace CI_Platform.Models
{
    public class Forget
    {
        [Required(ErrorMessage = "Please Enter Email")]
        public string? Email { get; set; }
    }
}
