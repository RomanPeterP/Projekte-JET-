using System.ComponentModel.DataAnnotations;

namespace TableReservationSystem.Viewmodels
{
    public class LoginViewModel
    {
        [MaxLength(30)]
        public string UserName { get; set; } = null!;

        [MaxLength(20)]
        public string Password {  get; set; } = null!;
    }
}