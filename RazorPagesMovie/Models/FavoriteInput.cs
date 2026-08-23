using System.ComponentModel.DataAnnotations;

namespace RazorPagesMovie.Models
{
    public class FavoriteInput
    {
        // タイトル、category、理由、Rating、ネタバレを含むかどうか（ContainsSpoiler）
        [Display(Name = "あなたの名前")]
        public string Name { get; set; } = "";
        [Display(Name = "推しの料理のタイトル")]
        public string Title { get; set; } = "";
        [Display(Name = "料理のカテゴリー")]
        public string Category { get; set; } = "";
        [Display(Name = "おすすめ理由")]
        public string Reason { get; set; } = "";
        [Display(Name = "おすすめ度")]
        public int Rating { get; set; }
        [Display(Name = "ネタバレを含む")]
        public bool ContainsSpoiler { get; set; }
    }
}
