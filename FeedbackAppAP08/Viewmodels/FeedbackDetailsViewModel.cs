using System.Collections.Generic;

namespace FeedbackAppAP08.Viewmodels
{
    public class FeedbackDetailsViewModel
    {
        public Feedback Feedback { get; set; } = null!;
        public List<string>? DocsTags { get; set; }
    }
}
