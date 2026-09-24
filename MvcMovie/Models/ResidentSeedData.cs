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
        var context = new MvcMovieContext(DbContextOptions < MvcMovieContext > options);
    }
}