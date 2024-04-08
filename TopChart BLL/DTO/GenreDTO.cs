using System.ComponentModel.DataAnnotations;

namespace TopChart_BLL.DTO
{
    public class GenreDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Genre must be filled")]
        [Display(Name = "Genre")]
        public string? Name { get; set; }
    }
}
