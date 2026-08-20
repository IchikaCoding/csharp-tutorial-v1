using Microsoft.EntityFrameworkCore;

// ここでOptionを渡して、Program.csで設定したconnectionStringとか接続文字列を渡している
public class RazorPagesMovieContext(DbContextOptions<RazorPagesMovieContext> options) : DbContext(options)
{
    // DBのテーブルに対応するもの
    public DbSet<RazorPagesMovie.Models.Movie> Movie { get; set; } = default!;
}
