namespace TopChart_DLL.Entities
{
    public class Users
    {
        public int Id { get; set; }
        public string? ConnectionId { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Salt { get; set; }
        public int Status { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
    }
}
