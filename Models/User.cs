namespace SalesTracker.Models
{
    public enum UserRole
    {
        Administrator,
        Manager,
        Seller
    }

    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public UserRole Role { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
