namespace StationaryClash.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public int Token { get; set; }
        public DateTime Created { get; set; }
    }
}
