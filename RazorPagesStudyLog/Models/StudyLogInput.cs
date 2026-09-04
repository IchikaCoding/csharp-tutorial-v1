using System.ComponentModel.DataAnnotations;

namespace RazorPagesStudyLog.Models
{
    public class StudyLogInput
    {
        [Display(Name = "名前")]
        public string Name { get; set; } = "";
        public string LearnedToday { get; set; } = "";
        public int UnderstandingLevel { get; set; }
        public bool NeedsReview { get; set; }
    }
}