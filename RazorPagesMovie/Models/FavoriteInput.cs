using System.ComponentModel.DataAnnotations;

namespace RazorPagesMovie.Models
{
    public class FavoriteInput
    {
        // タイトル、category、理由、Rating、ネタバレを含むかどうか（ContainsSpoiler）
        [Display(Name = "あなたの名前")]
        // 必須なのになかった場合はエラー
        [Required(ErrorMessage = "名前を入力してください")]
        // 第一引数は、Maxです
        [StringLength(50, MinimumLength = 2, ErrorMessage = "名前は2文字以上50文字以内で入力してください。")]
        public string Name { get; set; } = "";

        [Display(Name = "推しの料理のタイトル")]
        [Required(ErrorMessage = "推し料理が入力されてません")]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "料理名は1文字以上、60文字以内で入力してください。")]
        public string Title { get; set; } = "";
        [Display(Name = "料理のカテゴリー")]
        [Required(ErrorMessage = "カテゴリを選択してください")]
        public string Category { get; set; } = "";
        [Display(Name = "おすすめ理由")]
        [Required(ErrorMessage = "おすすめ理由が入力されていません")]
        public string Reason { get; set; } = "";
        [Display(Name = "おすすめ度")]
        [Required(ErrorMessage = "おすすめ度が入力されていません")]
        [Range(0, 10)]
        public int? Rating { get; set; }
        [Display(Name = "ネタバレを含む")]
        public bool ContainsSpoiler { get; set; }
        // 発売年
        // TODO: 現在の年ってどうやって計算するの？DateTime.Now.Yearでやれそうなのに。
        [Display(Name = "発売年")]
        [Range(1900, int.MaxValue, ErrorMessage = "発売年は1900年から現在の年までで入力してください。")]
        public int ReleaseYear { get; set; }
    }
}
