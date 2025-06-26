using System.ComponentModel.DataAnnotations;

namespace TableReservationSystem.Viewmodels
{
    public class RestaurantSearchCriteriaViewModel
    {
        [MaxLength(100)]
        [Display(Name = "Suchwörter und Phrasen")]
        public string? WordsAndPhrases { get; set; }
    }
}
