
using Microsoft.EntityFrameworkCore;

namespace MvcMovie.Models
{
    // 初期データ用のクラス
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            // usingはStreamReaderとかと同じで、使い終わったら閉じるってやつ？
            // DBContextとかは、使い終わったらDisposeしないといけない。だからusing を使って自動で廃棄してもらおう！
            using (var context = new MvcMovieContext(
                // GetRequiredServiceメソッドは、たぶんDBContextのOptionsを返してくれる。
                // だから、MvcMovieContextの設定がここで渡される
                serviceProvider.GetRequiredService<DbContextOptions<MvcMovieContext>>()
                ))
            {
                // もともとテーブルがあったら、早期リターン
                // Any()はboolが返ってくる。一件でもあったらtrue
                if (context.Movie.Any())
                {
                    return;
                }
                // Contextの中身はAddRangeで追加するっぽい
                // DBContextにDB追加予定のデータを記録する。まだSQL ServerのDBには未反映
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
                // TODO: これは何をしている？
                // DBontextに登録されたシードデータをSQL Serverに保存する処理を実行
                context.SaveChanges();
            }
        }
    }
}
