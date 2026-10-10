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
        // 今回はViewしか戻り値がないからViewResultに修正した
        public async Task<ViewResult> Practice()
        {
            // リスト作成
            // エラーが読めた！！！
            // 意味：DBから Resident のデータを取得し、List<Resident> として residents に入れるという意味
            List<Resident> residents = await _context.Resident.ToListAsync();
            // 渡す
            return View(residents);
        }
        // TODO: 処理を継続したいならIActionResultになるらしい。でも処理の継続とはなんだろう？
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        // 引数のBind属性には、プロパティを書く。次は、Resident residentでバインディングするモデルを書く。
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,AnimalType,Job,ResidentSince")] Resident resident)
        {
            // ModelStateが有効か確認。ModelStateも確認
            if (ModelState.IsValid)
            {
                // Contextにフォームの内容を追加。
                // TODO: 非同期処理でもいい？見本は同期処理だった。
                await _context.AddAsync(resident);
                // DBに保存
                await _context.SaveChangesAsync();
                // Indexに飛ぶ
                return RedirectToAction(nameof(Index));
            }
            // 今の画面のまま
            return View(resident);
        }
    }
}