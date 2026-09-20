
using Microsoft.EntityFrameworkCore;

namespace MvcMovie.Models
{
    // 初期データ用のクラス
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcMovieContext(
                serviceProvider.GetRequiredService<DbContextOptions<MvcMovieContext>>()
                ))
            {
                // もともとテーブルがあったら、早期リターン
                if (context.Movie.Any())
                {
                    return;
                }
            }
        }
    }
}
