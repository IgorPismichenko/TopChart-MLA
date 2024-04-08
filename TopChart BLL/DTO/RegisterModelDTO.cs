using System.ComponentModel.DataAnnotations;

namespace TopChart_BLL.DTO
{
    public class RegisterModelDTO
    {
        [Required(ErrorMessage = "Login must be filled")]
        [RegularExpression(@"^[a-zA-Z0-9.-]+$", ErrorMessage = "Login can only contain letters, digits, dot, and hyphen")]
        [Display(Name = "Login")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Password must be filled")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [RegularExpression(@"^[a-zA-Z0-9]*[0-9]+[a-zA-Z0-9]*$", ErrorMessage = "Password must contain at least one letter and one digit")]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "You must confirm password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string? PasswordConfirm { get; set; }
    }
}
