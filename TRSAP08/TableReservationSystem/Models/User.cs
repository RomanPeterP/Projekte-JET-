
using TableReservationSystem.Models.Interfaces;

namespace TableReservationSystem.Models
{
    public class User : IUser
    { 
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public int RoleId { get; set; }
        public string Email { get; set; } = null!;

        public virtual Role Role { get; set; } = null!;
    }
}
