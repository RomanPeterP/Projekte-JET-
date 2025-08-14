namespace TableReservationSystem.Models.Interfaces
{
    public interface IUser
    {
        int UserId { get; set; }
        string UserName { get; set; }
        string PasswordHash { get; set; }
        string RoleId { get; set; }
        string Email { get; set; }
    }
}
