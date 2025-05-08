using System.ComponentModel.DataAnnotations;

namespace TableReservationSystem.Viewmodels
{
    public class RestaurantFormViewModel: RestaurantBaseViewModel
    {
        public int RestaurantId { get; set; }  // Bei "Create" kann man ihn weglassen

        [Required(ErrorMessage = "Der Name ist erforderlich.")]
        [MaxLength(30)]
        [Display(Name = "Name des Restaurants")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Die PLZ ist erforderlich.")]
        [MaxLength(15)]
        [Display(Name = "Postleitzahl")]
        public string PostalCode { get; set; } = null!;

        [Required(ErrorMessage = "Der Ort ist erforderlich.")]
        [MaxLength(20)]
        [Display(Name = "Ort")]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "Straße und Hausnummer sind erforderlich.")]
        [MaxLength(25)]
        [Display(Name = "Straße und Hausnummer")]
        public string StreetHouseNr { get; set; } = null!;

        [Display(Name = "Aktiv")]
        public bool Activ { get; set; }

        [Required(ErrorMessage = "Das Land ist erforderlich.")]
        [MaxLength(2)]
        [Display(Name = "Land")]
        public string Country { get; set; } = null!;

        // Aufgelöste Kontaktinformationen
        [Phone(ErrorMessage = "Bitte eine gültige Telefonnummer eingeben.")]
        [MaxLength(20)]
        [Display(Name = "Telefonnummer")]
        public string? PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Bitte eine gültige E-Mail-Adresse eingeben.")]
        [MaxLength(50)]
        [Display(Name = "E-Mail-Adresse")]
        public string? Email { get; set; }
    }
}
