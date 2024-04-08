using System.ComponentModel.DataAnnotations;
using TopChart_DLL.Entities;

namespace TopChart_BLL.DTO
{
    public class VideoDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title must be filled")]
        [Display(Name = "Title")]
        public string? Name { get; set; }
        public Singer? Singer { get; set; }
        [Required(ErrorMessage = "Choose a singer")]
        [Display(Name = "Singer")]
        public int SingerId { get; set; }
        public string? Album { get; set; }
        public Genre? Genre { get; set; }
        [Required(ErrorMessage = "Choose the genre")]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }
        public int Like { get; set; }
        [RegularExpression(@"\.mp4$", ErrorMessage = "Only mp4 files")]
        public string? Path { get; set; }
        public string? Date { get; set; }
        public string? Size { get; set; }
    }
}
