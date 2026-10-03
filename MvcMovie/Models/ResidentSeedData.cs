using Microsoft.EntityFrameworkCore;
namespace MvcMovie.Models;

public class ResidentSeedData
{
    // 初期化メソッドを作成する
    // そのなかで、もしDBのデータが存在していたら、return
    // データがなかった場合は、Residentクラスのインスタンスを作る
    public static void Initialize(IServiceProvider serviceProvider)
    {
        // TODO: ここのエラーを解消する！
        // optionsをどこからか、取ってきたら治りそう！
        using (var context = new MvcMovieContext(serviceProvider.GetRequiredService<DbContextOptions<MvcMovieContext>>()))
        {
            // context.Residentがあったら早期リターン
            if (context.Resident.Any())
            {
                return;
            }
            // あった場合は、DBにリスナーさんたちをお泊りしてもらう
            // contextにインスタンスをいれる！！
            context.Resident.AddRange(
                new Resident()
                {
                    Name = "アルパカどん🦙",
                    AnimalType = "Animal",
                    Job = "窓から人類観察",
                    ResidentSince = new DateTime(1600, 9, 26)
                },
                new Resident()
                {
                    Name = "さつまいもパンまん🍠",
                    AnimalType = "Human",
                    Job = "パン屋さん",
                    ResidentSince = DateTime.Now
                },
                new Resident()
                {
                    Name = "いちか丼",
                    AnimalType = "Human",
                    Job = null,
                    ResidentSince = DateTime.Now
                }
            );
            // DBに反映させる
            context.SaveChanges();
        }
    }
    // テーブルのデータを一括削除したい時のメソッド
    public static async Task DeleteTable(IServiceProvider serviceProvider)
    {
        using (var context = new MvcMovieContext(serviceProvider.GetRequiredService<DbContextOptions<MvcMovieContext>>()))
        {
            // ExecuteDeleteAsync()で一括削除
            int deleteRows = await context.Resident.ExecuteDeleteAsync();
        }
    }
}