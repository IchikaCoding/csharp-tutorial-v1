using System.Dynamic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;

namespace RazorPagesMovie.Pages.Practice
{
    public class HelloModel : PageModel
    {
        public string? Message { get; private set; }
        public DateTime DisplayAt { get; private set; }
        public string? Name { get; private set; }
        // ブラウザからのHTTPのGETリクエストを処理するメソッド
        // これはPageModelを継承しているからASP.NET Core が命名規則を見て呼び出せる
        public void OnGet()
        {
            Message = "これはCSで設定しました。設定した方は「いちかどん」です";
            DisplayAt = DateTime.Now;
            Name = "いちか丼定食";
        }
    }

}