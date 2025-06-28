using System.Collections.Generic;

namespace FeedbackAppAP08.Viewmodels
{
    public class FeedbackListViewModel
    {
        public FeedbackSearchCriteriaViewModel? FeedbackSearchCriteria { get; set; }
        public IEnumerable<Feedback> FeedbackList { get; set; } = null!;
    }
}
