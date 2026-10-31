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
            // _contextがDBの接続文字列とかテーブルとかを持っている。
            // .Residentで指定すると、Residentテーブルを操作するための`DbSet<Resident>`オブジェクトがゲットできるらしい
            // residentsRaw自体がテーブルではない。
            // 👉️DbSet<Resident> は、Resident エンティティの集合に対して、検索・追加・削除などを行うためのクラスです。
            DbSet<Resident> residentsRaw = _context.Resident;
            // ここで実際にテーブルからデータが取得される。
            List<Resident> residents = await residentsRaw.ToListAsync();
            string firstName = "";
            if (residents.Count > 0)
            {
                firstName = residents[0].Name;

            }
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,AnimalType,Job,ResidentSince")] Resident resident)
        {
            // ModelStateが有効か確認。ModelStateも確認
            // trueならエラー1件もなし
            if (ModelState.IsValid)
            {
                // 日付系を追加
                // バインドしていないプロパティがあっても、resident変数として利用できるっぽい
                resident.CreatedAt = DateTime.Now;
                resident.UpdatedAt = DateTime.Now;
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
        // Index画面からID情報をもらう
        // 引数で、IDの情報を受け取る、Viewの引数とする


        // そうじゃないなら、IDから取得したデータを表示する
        // TODO: idはどこからやってきたの？
        public async Task<IActionResult> Delete(int? id)
        {
            // IDを引数とする。もしIDがnullなら、NotFound()を返す。
            if (id == null)
            {
                return NotFound();
            }
            // 取得したデータがIDと一致していないならNotFound()
            DbSet<Resident> residents = _context.Resident;
            // residentsをリストととしてデータ取得、IDと一致
            // FirstOrDefaultAsync()とは？👉️IDで一致するものがあったら、Resident型のデータ1件が取得できる。なかったらNull。
            // awaitしたら、非同期処理をして、データが取得出来てからもらえる。Resident?が取れる。
            // もしawait していない場合は、Task<Resident?>型となる。まだ非同期処理を実行していないから（？）
            Resident? resident = await residents.FirstOrDefaultAsync(item => item.Id == id);
            // →DBに保存されているデータのIDと今回のIDが一致するか見る
            if (resident == null)
            {
                return NotFound();
            }
            return View(resident);
        }
        // DeleteConfirmメソッドを作成しよう！
        // TODO: Postリクエストのときのみ実行されるように修正
        // [ValidateAntiForgeryToken]を追加して、Postリクエストが安全に送信されるようにしました
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            // Nullじゃないかな？を確認
            if (id == null)
            {
                return NotFound();
            }
            // ContextのデータからIDで一致するものを探す
            Resident? resident = await _context.Resident.FirstOrDefaultAsync(item => item.Id == id);
            // residentがないなら、NotFound()
            if (resident == null)
            {
                return NotFound();
            }
            // 一致したら、Contextからそのデータを削除
            // TODO: どうしてResidentを入れるだけでDBから削除できるの？
            _context.Remove(resident);
            // DB自体に変更を反映させる
            await _context.SaveChangesAsync();
            // Indexの画面にリダイレクト。（前回のデータが更新されるのか知りません）
            return RedirectToAction("Index");
            // return "DeleteConfirmが動きました✨️";
        }
        // IDからデータを取得
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var resident = await _context.Resident.FirstOrDefaultAsync(item => item.Id == id);
            if (resident == null)
            {
                return NotFound();
            }
            return View(resident);
        }


        // FormでPostしたデータをDB登録する👉️Createの処理と似ていた。引数は同じでモデルバインディングをする
        // フォームに入力したデータを受け取って、それDBの値を手動で上書き。上書きされたデータをDB保存する。
        // ValidateAntiForgeryTokenをいれることで、クッキーとフォームのPostのトークンが一致するかチェックしてくれる
        [HttpPost]
        [ValidateAntiForgeryToken]
        // モデルバインディングってIDはしないの？
        // TODO: Bind属性と[BindProperty]属性を使ってみよう！
        public async Task<IActionResult> SaveEdit(int? id, [Bind("Name, AnimalType, Job, ResidentSince")] Resident resident)
        {
            // ここで新しい変数に入れてもダメ。参照型だからIDがリセットされない？
            // Resident newResident = new Resident();
            // TODO: DBへの保存処理を追加しよう
            if (id == null)
            {
                return NotFound();
            }
            // IDを取得出来なかった場合はnullの可能性。null許容にしてみた
            Resident? dbresident = await _context.Resident.FirstOrDefaultAsync(item => item.Id == id);
            if (dbresident == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                // Formのデータでモデルバインディングが失敗しているから元データを使う処理
                // residentはまだC#のインスタンス。
                // そのインスタンスのIdプロパティに値を代入しているだけ
                // dbresident.Idは自動採番されたID持ってる
                resident.Id = dbresident.Id;
                // viewName,　resident
                // EditにどうしてresidentIdを入れて渡すの？
                // TODO: 不明。dbresident代入するならそのままdbresident表示してよくない？
                // 👉️元のDBのデータに戻したいわけじゃない。ユーザーが頑張って入力したデータはそのまま表示してあげる優しさでした。
                // TODO: 入力値のエラーだった場合、エラーの入力値がまた入ってしまうから、エラー治らないのでは？
                return View("Edit", resident);
            }

            // TODO: FormのデータでDBを更新したい
            // Bindしたプロパティは4つだから4つしか上書きできない？！
            dbresident.Name = resident.Name;
            dbresident.AnimalType = resident.AnimalType;
            dbresident.Job = resident.Job;
            dbresident.ResidentSince = resident.ResidentSince;
            dbresident.UpdatedAt = DateTime.Now;
            // // モウすでにあるデータへの上書きをしよう！
            // 仮説：同じIDにデータ保存しようとしているのが問題そう？あとは手動でIDを決めようとしているのも問題っぽい。
            // Updateを使うと、IDは変えず、IDをもとに更新してくれる。
            // _context.Update(dbresident);
            // DBへの反映
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}