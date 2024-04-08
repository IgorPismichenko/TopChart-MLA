namespace TopChart_DLL.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Message { get; set; }

        public virtual Users? User { get; set; }

        public int UserId { get; set; }
        public virtual Tracks? Track { get; set; }
        public int TrackId { get; set; }
        public string? Date { get; set; }
    }
}
