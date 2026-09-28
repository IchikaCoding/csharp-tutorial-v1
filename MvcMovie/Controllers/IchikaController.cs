using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using MvcMovie.Models;

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
        // 非同期処理でList化。アクションメソッドの戻り値型はIActionResultっぽい
        public async Task<IActionResult> Index()
        {
            // ResidentのListバージョンをViewに渡してみよう！
            List<Resident> residents = await _context.Resident.ToListAsync();
            // TODO: いったんResidentのデータをViewに渡してみました。合っているのはわかりません。
            return View(residents);
        }
        // GET: Ichika/pochipochi
        public string PochiPochi()
        {
            return "毎日お勉強✨️";
        }
        // Practice アクションメソッドを作成します
        // Listに変換。非同期処理でやる。そのデータ渡す
        public async Task<IActionResult> Practice()
        {
            // リスト作成
            // エラーが読めた！！！
            List<Resident> residents = await _context.Resident.ToListAsync();
            // 渡す
            return View(residents);
        }
    }
}