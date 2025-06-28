using System.ComponentModel.DataAnnotations;

namespace FeedbackAppAP08.Viewmodels
{
    public class FeedbackSearchCriteriaViewModel
    {
        [MaxLength(100)]
        [Display(Name = "Suchwörter")]
        public string? Words { get; set; }
    }
}
