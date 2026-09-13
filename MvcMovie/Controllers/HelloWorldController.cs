using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;

namespace MvcMovie.Controllers
{
    public class HelloWorldController : Controller
    {
        //
        // GET: https://localhost:{PORT}/HelloWorld/
        public string Index()
        {
            return "This is my default action...";
        }
        // これはアクションメソッドという
        // 引数の1は、既定値らしい。値が指定されていないなら、1が設定される
        // モデルバインドされるらしい。。これはよくわからなった
        // クエリ文字列は、`?`の後ろに書いてあるものたち。フィールド（ex. name）と値のペアを区切るのが`&`文字
        // GET: http://localhost:{PORT}/HelloWorld/welcome?name=Ichika&days=353
        // MapControllerRouteというものがWelcomeメソッドに含まれている。これのおかげで、以下のURLでもアクセスできるようになったのかな？ 
        // https://localhost:{PORT}/HelloWorld/Welcome/3?name=ichika
        public string Welcome(string name, int id = 1)
        {
            // HtmlEncoder.Default.EncodeによってJSとか悪意のある入力からアプリを保護しているらしい
            return HtmlEncoder.Default.Encode($"Hello {name}, ID: {id}");
        }
        //
        // GET: https://localhost:{PORT}/HelloWorld/Ichika/
        public string Ichika()
        {
            return "いちかどん🍠";
        }
    }
}
