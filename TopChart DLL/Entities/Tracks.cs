using System.ComponentModel.DataAnnotations;

namespace TopChart_DLL.Entities
{
    public class Tracks
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual Singer? Singer { get; set; }
        public int SingerId { get; set; }
        public string? Album { get; set; }
        public virtual Genre? Genre { get; set; }
        public int GenreId { get; set; }
        public int Like { get; set; }
        public string? Path { get; set; }

        public string? Date { get; set; }
        public string? Size { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
    }
}
