
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

public class MoviesController : Controller
{
    // readonlyって最初は代入できるのか＿
    private readonly MvcMovieContext _context;

    // TODO: このContextってどこで渡しているのでしょうか？
    // TODO: MS learn が終わってもまだわからないならここの点を学習
    // ASP.NET CoreのDIという仕組みが自動でわたしている。
    // これコンストラクターだった。コンストラクターインジェクションというらしい。
    public MoviesController(MvcMovieContext context)
    {
        _context = context;
    }

    // GET: MOVIES
    public async Task<IActionResult> Index()
    {
        return View(await _context.Movie.ToListAsync());
    }

    // GET: MOVIES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie
            .FirstOrDefaultAsync(m => m.Id == id);
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // GET: MOVIES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: MOVIES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price")] Movie movie)
    {
        // ModelStateってなに？
        if (ModelState.IsValid)
        {
            // どうして同期処理でいいのか？
            _context.Add(movie);
            // SQLサーバーに保存するとなるとちょっと時間が掛かりそう。
            await _context.SaveChangesAsync();
            // DB保存できたら、Indexへ画面遷移します
            // どうしてnameof?👉️これを使うとアクションメソッドへのリダイレクトが作れる。
            // 文字列で書いちゃったら、Indexっていう文字になってしまう。アクションメソッドと連携しないといけない。
            return RedirectToAction(nameof(Index));
        }
        // どうして引数？
        return View(movie);
    }

    // GET: MOVIES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie.FindAsync(id);
        if (movie == null)
        {
            return NotFound();
        }
        return View(movie);
    }

    // POST: MOVIES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Title,ReleaseDate,Genre,Price")] Movie movie)
    {
        if (id != movie.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(movie);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(movie.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(movie);
    }

    // GET: MOVIES/Delete/5
    // TODO: どうやって引数に入れるのか？
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        // _context.MovieはDbSet<Movie>型
        // movieの型を調べたい👉️Resident?型！
        var movie = await _context.Movie
            .FirstOrDefaultAsync(m => m.Id == id);
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // POST: MOVIES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var movie = await _context.Movie.FindAsync(id);
        if (movie != null)
        {
            _context.Movie.Remove(movie);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MovieExists(int? id)
    {
        return _context.Movie.Any(e => e.Id == id);
    }
}
