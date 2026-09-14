using System.ComponentModel.Design;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;

namespace MvcMovie.Controllers
{
    public class HelloWorldController : Controller
    {
        //
        // GET: https://localhost:{PORT}/HelloWorld/
        // Controllerのメソッドは、アクションメソッドと呼ばれる
        // 通常の戻り値は、IActionResult型じゃなくて、ActionResultとかString派生のクラスとかになるらしい。
        public IActionResult Index()
        {
            // これを戻り値にすると、Viewメソッドを呼び出します
            return View();
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
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
