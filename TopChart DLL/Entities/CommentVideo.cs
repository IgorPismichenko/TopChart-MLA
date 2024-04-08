namespace TopChart_DLL.Entities
{
    public class CommentVideo
    {
        public int Id { get; set; }
        public string? Message { get; set; }

        public virtual Users? User { get; set; }

        public int UserId { get; set; }
        public virtual Video? Video { get; set; }
        public int VideoId { get; set; }
        public string? Date { get; set; }
    }
}
