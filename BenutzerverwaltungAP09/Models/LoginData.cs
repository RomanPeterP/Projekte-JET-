using System.ComponentModel.DataAnnotations;

namespace BenutzerverwaltungAP09.Models
{
    public class LoginData
    {
        public int BenutzerId { get; set; } 
        public string Benutzername { get; set; } = null!;
        public string Passwort { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedAt { get; set; }

        public Benutzer Benutzer { get; set; } = null!;
    }

}
