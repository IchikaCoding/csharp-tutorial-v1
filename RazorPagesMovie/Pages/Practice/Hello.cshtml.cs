using System.Dynamic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;

namespace RazorPagesMovie.Pages.Practice
{
    public class HelloModel : PageModel
    {
        public string? Message { get; private set; }
        public DateTime DisplayAt { get; private set; }
        // ブラウザからのHTTPのGETリクエストを処理するメソッド
        public void OnGet()
        {
            Message = "これはCSで設定しました。設定した方は「いちかどん」です";
            DisplayAt = DateTime.Now;
        }
    }

}