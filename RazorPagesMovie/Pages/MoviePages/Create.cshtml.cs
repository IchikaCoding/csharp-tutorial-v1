using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.MoviePages;

public class CreateModel : PageModel
{
    private readonly RazorPagesMovieContext _context;

    public CreateModel(RazorPagesMovieContext context)
    {
        _context = context;
    }

    // Page()でページに必要な情報を初期化するらしい
    public IActionResult OnGet()
    {
        return Page();
    }

    // 画面側の入力欄が、Movie の各プロパティにBindされます
    [BindProperty]
    public Movie Movie { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    // このメソッドが実行される前に入力された値がMovieモデルにバインドされる
    public async Task<IActionResult> OnPostAsync()
    {
        // 入力値に問題があった場合は、同じページをエラーメッセージ付きで再表示する
        if (!ModelState.IsValid)
        {
            // 再表示される
            return Page();
        }
        // Entity Framework Coreに「このMovieを追加予定として管理してください」
        _context.Movie.Add(Movie);
        // DBにインサートする
        await _context.SaveChangesAsync();
        // 一覧ページにリダイレクトしている。更新結果を返すイメージはちょっと違うらしい。
        return RedirectToPage("./Index");
    }
}
