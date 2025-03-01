namespace StationaryClash.Models
{
    public class UserSession
    {
        public int ID { get; set; }
        public string Username { get; set; } = string.Empty;
        public int Token { get; set; }
        public string? Description { get; set; }
    }
}
