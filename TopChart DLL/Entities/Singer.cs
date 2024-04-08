using System.ComponentModel.DataAnnotations;

namespace TopChart_DLL.Entities
{
    public class Singer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Path { get; set; }
        public virtual ICollection<Tracks>? Tracks { get; set; }
    }
}
