using System.ComponentModel.DataAnnotations;

namespace TRSAP09.Viewmodels
{
    public class RestaurantFormViewModel
    {
        public int RestaurantId { get; set; }  // Bei "Create" kann man ihn weglassen

        [Required(ErrorMessage = "Der Name ist erforderlich.")]
        [Display(Name = "Name des Restaurants")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Die PLZ ist erforderlich.")]
        [Display(Name = "Postleitzahl")]
        [RegularExpression(@"^\d{4,10}$", ErrorMessage = "Bitte eine gültige Postleitzahl eingeben.")]
        public string PostalCode { get; set; } = null!;

        [Required(ErrorMessage = "Der Ort ist erforderlich.")]
        [Display(Name = "Ort")]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "Straße und Hausnummer sind erforderlich.")]
        [Display(Name = "Straße und Hausnummer")]
        public string StreetHouseNr { get; set; } = null!;

        [Display(Name = "Aktiv")]
        public bool Activ { get; set; }

        [Required(ErrorMessage = "Das Land ist erforderlich.")]
        [Display(Name = "Land")]
        public string Country { get; set; } = null!;

        // Aufgelöste Kontaktinformationen
        [Phone(ErrorMessage = "Bitte eine gültige Telefonnummer eingeben.")]
        [Display(Name = "Telefonnummer")]
        public string? PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Bitte eine gültige E-Mail-Adresse eingeben.")]
        [Display(Name = "E-Mail-Adresse")]
        public string? Email { get; set; }

        [Url(ErrorMessage = "Bitte eine gültige URL eingeben.")]
        [Display(Name = "Webseite")]
        public string? Website { get; set; }
    }
}
