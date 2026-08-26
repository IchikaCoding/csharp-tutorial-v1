using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Practice
{
    public class FavoriteModel : PageModel
    {
        // ここでは、作ったModelを使用してInputの中身をいれる
        [BindProperty]
        public FavoriteInput Input { get; set; } = new FavoriteInput();
        // 送信されたのかどうか確認するためのプロパティ
        public bool Submitted { get; private set; }
        // 送信された時刻のプロパティを追加
        public DateTime SubmittedAt { get; private set; }
        public string ResultMessage { get; private set; } = "";
        public void OnGet() { }
        // IActionResult は、このリクエストに対して、次に何を返すか、をあわらせる型
        public IActionResult OnPost()
        {
            Console.WriteLine("OnPostが実行されました。");
            //モデルバインディングとモデル検証でエラーがないならTrue。エラーがあるならfalse
            // MVCでもよくでてくるif文
            if (!ModelState.IsValid)
            {
                // Page()はページを再表示してくれる。
                return Page();
            }
            // Nameプロパティに入力値が入るのは、OnPostが実行される前。
            // だからここが実行される頃にはNameが入っています
            ResultMessage = $"{Input.Name}さん、Post成功しましたyo🎉";
            Console.WriteLine("ResultMessage: " + ResultMessage);

            // Page()を返すようにする
            Submitted = true;
            SubmittedAt = DateTime.Now;
            // 同じページに入力結果を表示したいから使うらしい
            return Page();
        }
    }
}