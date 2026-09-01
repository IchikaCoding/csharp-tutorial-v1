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
        public void OnGet()
        {
            Practice();
        }
        // IActionResult は、このリクエストに対して、次に何を返すか、をあわらせる型
        // OnPostが実行されると、自動でBindPropertyが動くよ。
        public IActionResult OnPost()
        {
            Console.WriteLine("OnPostが実行されました。");
            // アプリ独自のルールのことをビジネスルールというらしい。
            if (Input.Title == Input.Reason)
            {
                ModelState.AddModelError("", "タイトルとおすすめ理由が一致しています。変更してください。");
            }
            // ModelState.AddModelError("", "これはフォーム全体のエラーです。");
            if (Input.Rating == 10 && Input.ContainsSpoiler == false)
            {
                ModelState.AddModelError("", "おすすめ度10の場合は、ネタバレの有無を確認してください");
            }
            // TODO: ここに年数の制約をいれる
            if (Input.ReleaseYear > DateTime.Now.Year)
            {
                ModelState.AddModelError("Input.ReleaseYear", $"発売年数が、現在の年({DateTime.Now.Year}年)を超えています。");
                // これっていれたらダメ？
                // AddModelErrorを実行すると、ModelStateがfalseになる。👉️だからここで再表示する必要なし！
                // return Page();
            }

            //モデルバインディングとモデル検証でエラーがないならTrue。エラーがあるならfalse
            // MVCでもよくでてくるif文
            // このIF文を削除した場合、成功した日付とかは表示しない
            // すぐエラーを表示できるようにreturn Page()をやっていました。
            // ここだけで再表示処理を入れておく👉️上で追加されたエラーが一度に表示できる。
            if (!ModelState.IsValid)
            {
                // Page()はページを再表示してくれる。
                return Page();
            }
            Console.WriteLine("ModelState: " + ModelState);
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

        // TODO: OnPostPreviewメソッドを追加する
        public IActionResult OnPostPreview()
        {
            Console.WriteLine("OnPostPreview()が実行されている");
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Submitted = true;
            SubmittedAt = DateTime.Now;
            return Page();
        }

        private void Practice()
        {
            // System.DateTimeが表示される
            Console.WriteLine(typeof(DateTime));
            // 2026が表示された
            Console.WriteLine(DateTime.Now.Year);
            // TODO: Yearが表示される。nameofの定義が見つからない。
            // 変数とか型とかメンバーの文字列定数が生成されるらしい。
            Console.WriteLine(nameof(DateTime.Now.Year));
            // TODO: どこで表示できるのか考えよう。
            System.Diagnostics.Debug.WriteLine("Hello");
        }
    }
}