using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace MvcMovie.Controllers
{
    // このControllerクラスのControllerの手前までの名前がURLのセグメント（？）になるらしい
    public class IchikaController : Controller
    {
        // contextをもらってくる
        private MvcMovieContext _context;
        // コンストラクターインジェクション
        // 引数はなんだ？_contextと同じ型だった、、、当たり前だった
        public IchikaController(MvcMovieContext context)
        {
            _context = context;
        }

        // 既定のメソッドだから、indexと書かなくても呼び出さるメソッド
        // GET: Ichika/index か　ichikaだけでもアクセスできる
        // Controllerは、（DBから？）からContextをゲットして、それをViewに渡す仕事。
        // 改造は、View()を返す形が理想。引数は、_contextのResidentのデータ。
        public ViewResult Index()
        {
            // TODO: いったんResidentのデータをViewに渡してみました。合っているのはわかりません。
            return View(_context.Resident);
        }
        // GET: Ichika/pochipochi
        public string PochiPochi()
        {
            return "毎日お勉強✨️";
        }
    }
}