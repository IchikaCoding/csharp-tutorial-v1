using System.ComponentModel.DataAnnotations;

namespace RazorPagesStudyLog.Models
{
    public class StudyLogInput
    {
        [Display(Name = "名前")]
        [Required(ErrorMessage = "君の名は。")]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "1文字以上、40文字以内で入力してください")]
        public string Name { get; set; } = "";
        [Display(Name = "今日学んだこと")]
        [Required(ErrorMessage = "【必須】今日学んだことを書いてください。")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "5文字以上、200文字以内でまとめてください。")]
        public string LearnedToday { get; set; } = "";
        // どうしてnull許容にしたの？👉️空で登録されると""で登録される👉️だからnullをOKにしておくとOK
        [Display(Name = "理解度")]
        [Required(ErrorMessage = "理解度を数字で入力してください")]
        [Range(0, 5, ErrorMessage = "0～5の間で入力してください。")]
        public int? UnderstandingLevel { get; set; }
        [Display(Name = "復習が必要ですか？")]
        public bool NeedsReview { get; set; }
    }
}