using System.ComponentModel.DataAnnotations;

namespace AniWatch.ViewModel
{
    public class Login
    {
        [Required(ErrorMessage = "Email is Required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password Required")]
        [MinLength(8, ErrorMessage = "At least 8 numbers")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
