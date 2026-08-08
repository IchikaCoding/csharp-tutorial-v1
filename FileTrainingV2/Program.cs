// お気に入りのファイルを実行場所と同じ場所に作成する処理
// 確認のためにパスも表示したい

// TODO: カレントディレクトリでもできるかも？
string dirPath = AppContext.BaseDirectory;
Console.WriteLine("dirPath" + dirPath);

string favPath = Path.Combine(dirPath, "favorites.json");
Console.WriteLine("favPath👉️" + favPath);
//// TODO: これだとフォルダを作っている事になっているかも？
//DirectoryInfo directoryInfo = Directory.CreateDirectory(favPath);

// JSONの中にテキストを入れてみる

async Task CreateJsonFileAsync()
{
    string message = "きゅうり";
    await File.WriteAllTextAsync(favPath, message);
}
// これはどうしてawait できるの？
await CreateJsonFileAsync();

Console.WriteLine("保存に成功しました");

// クラスを作成、食べ物の名前、更新した時刻
// インスタンスを作成、初期化して
// クラスからJSONにする、見やすくしたいらしい。
// JSONをファイルに書き込む