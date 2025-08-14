using TableReservationSystem.Models.Interfaces;

namespace TableReservationSystem.Models
{
    public class Role : IRole
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public virtual ICollection<User> Users { get; set; } = null!;
    }
}
