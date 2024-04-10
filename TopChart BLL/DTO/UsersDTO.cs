namespace TopChart_BLL.DTO
{
    public class UsersDTO
    {
        public int Id { get; set; }
        public string? ConnectionId { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Salt { get; set; }
        public int Status { get; set; }
    }
}
