
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
                context.Movie.AddRange(
                    new Movie
                    {
                        Title = "名探偵いちかどん！~真実はいつも1つとは限らない~",
                        // Parseはちょっとよくわからない
                        ReleaseDate = DateTime.Parse("2020-02-02"),
                        Genre = "教育系",
                        Price = 20000,
                    },
                    new Movie
                    {
                        Title = "キャラメルポップコーンが進む映画",
                        ReleaseDate = DateTime.Parse("2026-09-22"),
                        Genre = "食事系",
                        Price = 1500,
                    },
                    new Movie
                    {
                        Title = "シードデータってなに？~学ぶもののみぞ知る~",
                        ReleaseDate = DateTime.Parse("2000-09-22"),
                        Genre = "IT教育系",
                        Price = 20000,
                    },
                    new Movie
                    {
                        Title = "仕事が遅い！！！！",
                        ReleaseDate = DateTime.Parse("1600-09-22"),
                        Genre = "歴史系",
                        Price = 200000,
                    }
                    );
            }
        }
    }
}
