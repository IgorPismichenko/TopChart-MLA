using System.ComponentModel.DataAnnotations;

namespace TopChart_BLL.DTO
{
    public class EditUserModelDTO
    {
        [Required]
        public string? Login { get; set; }

        [Required]
        public int Status { get; set; }
    }
}
