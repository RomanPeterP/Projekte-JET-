using System.ComponentModel.DataAnnotations;

namespace BenutzerverwaltungAP09.Models
{
    public class Benutzer
    {
        public int BenutzerId { get; set; }
        [MaxLength(20)]
        public string Vorname { get; set; } = null!;
        [MaxLength(50)]
        public string Nachname { get; set; } = null!;
        [Range(0, 110)]
        public byte Alter { get; set; }

        public LoginData? LoginData { get; set; }
    }
}
