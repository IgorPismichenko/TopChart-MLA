using TopChart_DLL.Entities;

namespace TopChart_BLL.DTO
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string? Message { get; set; }

        public Users? User { get; set; }

        public int UserId { get; set; }
        public Tracks? Track { get; set; }
        public int TrackId { get; set; }
        public string? Date { get; set; }
    }
}
