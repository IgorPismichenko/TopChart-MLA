using System.ComponentModel.DataAnnotations;

namespace TopChart_DLL.Entities
{
    public class RegisterModel
    {
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? PasswordConfirm { get; set; }
    }
}
