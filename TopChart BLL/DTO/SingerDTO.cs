using System.ComponentModel.DataAnnotations;

namespace TopChart_BLL.DTO
{
    public class SingerDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name must be filled")]
        [Display(Name = "Name")]
        public string? Name { get; set; }
        public string? Path { get; set; }
    }
}
