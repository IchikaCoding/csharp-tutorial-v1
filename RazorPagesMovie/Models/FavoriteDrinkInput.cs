using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesMovie.Models
{
    public class FavoriteDrinkInput
    {
        [Display(Name = "Name")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "2文字以上20文字以内にしてね！")]
        [Required(ErrorMessage = "名前を入力してね")]
        public string Name { get; set; } = "";
        [Display(Name = "Rating")]
        [Range(1, 5, ErrorMessage = "1～5の間で入力してね")]
        public int Rating { get; set; }
    }
}
