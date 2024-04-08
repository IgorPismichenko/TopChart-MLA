using System.ComponentModel.DataAnnotations;

namespace TopChart_BLL.DTO
{
    public class LoginModelDTO
    {
        [Required(ErrorMessage = "Login must be filled")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Password must be filled")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
