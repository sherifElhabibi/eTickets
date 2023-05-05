namespace eTickets.Models
{
    public class Roles
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public ICollection<User> User { get; set; } = new HashSet<User>();
    }
}
