using System.Text.Json;
using FileTrainingV2;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.CompilerServices;
using System.Security;
// お気に入りのファイルを実行場所と同じ場所に作成する処理
// 確認のためにパスも表示したい

// TODO: カレントディレクトリでもできるかも？


string dirPath = AppContext.BaseDirectory;
Console.WriteLine("dirPath" + dirPath);

string favPath = Path.Combine(dirPath, "favorites.json");
string favTempPath = Path.Combine(dirPath, "favorites.temp");

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
// await CreateJsonFileAsync();

Console.WriteLine("保存に成功しました");

DateTime now = DateTime.Now;

// クラスを作成、食べ物の名前、更新した時刻
// インスタンスを作成、初期化して
// クラスからJSONにする、見やすくしたいらしい。
// JSONをファイルに書き込む 
// TODO: 絵文字もJSONに表示したい
FavoriteFoodClass favoriteFood = new FavoriteFoodClass
{
    // TODO: Listの中に文字列を入れる
    foods = new List<string> { "はちみつ" },
    upDateTime = now
};

// List<string> stringList = new List<string>();
// stringList.Add("おいも");

// favoriteFood.foods.Add("さつまいも");
Console.WriteLine("favoriteFood.foods: " + favoriteFood.foods);
Console.WriteLine("favoriteFood.upDateTime: " + favoriteFood.upDateTime);

// JsonSerializerOptionsはクラスだよ
var option = new JsonSerializerOptions
{
    WriteIndented = true,
    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
};

try
{
    // クラスからJSONにするだけならインスタンスとオプションだけでOK
    string json = JsonSerializer.Serialize(favoriteFood, option);
    Console.WriteLine("json:" + json);
    // JSONに直すのが成功したのか確認する？
    // 中身をファイルに書き込み成功したら、

    // throw new Exception("テスト用の例外です✨️");
    // ここで失敗して例外になるなら、、、
    // 失敗したら例外が発生するから、File.Exists()は不要。成功していたら次の行へ行ける
    await File.WriteAllTextAsync(favTempPath, json);
    if (!File.Exists(favTempPath))
    {
        throw new Exception("保存失敗");
    }
    File.Move(favTempPath, favPath, true);
    Console.WriteLine("保存成功！！");
}
catch (Exception error)
{
    Console.WriteLine("保存失敗した");
    Console.WriteLine("エラー内容：" + error.Message);
}
finally
{
    // 一時ファイルを削除
    if (File.Exists(favTempPath))
    {
        File.Delete(favTempPath);
    }
}

// ＝＝＝＝＝＝＝＝＝オブジェクト初期化子の練習＝＝＝＝＝＝＝＝＝＝＝＝
// キャラクターの名前、Level
void PracticeClass()
{

    CharacterClass characterClass1 = new CharacterClass
    {
        CharacterName = "ichikaDon",
        Level = 200
    };
    Console.WriteLine("name: " + characterClass1.CharacterName);
    Console.WriteLine("level: " + characterClass1.Level);
    CharacterClass characterClass2 = new CharacterClass
    {
        CharacterName = "pochipochiFriends",
    };
    Console.WriteLine($"name: {characterClass2.CharacterName}");
    Console.WriteLine($"level: {characterClass2.Level}");
    characterClass2.Level = 1200;
    Console.WriteLine($"level: {characterClass2.Level}");
}


async Task CreateTextFile()
{
    // テキストで「はちみつ」と書く処理
    string rootPath = AppContext.BaseDirectory;
    string honeyPath = Path.Combine(rootPath, "favorite-food.txt");
    // await は不要でした！
    await using FileStream stream = File.Create(honeyPath);
    // stream.Write()ではbyte[]が必要。だから事前に準備している。
    byte[] foodBytes = Encoding.UTF8.GetBytes("はちみつ");
    // var option = new JsonSerializerOptions
    // {
    //     WriteIndented = true,
    //     Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
    // };
    // await JsonSerializer.SerializeAsync(stream, foodBytes, option);
    stream.Write(foodBytes);
    Console.WriteLine("CreateTextFile()動いたよ🎉");
}

await CreateTextFile();

// ファイル名、poseidon.txt
// キャラクタークラスのインスタンスを作って、その中身をファイルに入れてみよう
// いまいる場所はどこ？
string rootPath = AppContext.BaseDirectory;
string poseidonPath = Path.Combine(rootPath, "poseidon.txt");
async Task CreatePoseidonFileAsync()
{

    CharacterClass mainCharacter = new CharacterClass()
    {
        CharacterName = "Ichika",
        Level = 1200
    };
    CharacterClass supportCharacter = new CharacterClass()
    {
        CharacterName = "pochipochiFriends",
        Level = 12000
    };
    Console.WriteLine(mainCharacter.CharacterName);
    // 文字列を入れ込みたい
    // byte[]に入れる
    // stream.Write(byte[] )
    byte[] mainCharBytes = Encoding.UTF8.GetBytes($"名前は、{mainCharacter.CharacterName}です！");
    byte[] supportCharBytes = Encoding.UTF8.GetBytes($"名前は、{supportCharacter.CharacterName}です！");
    //  FileMode.Createが上書き。だから、FileStreamを作成した時点で中身0になるらしい
    await using FileStream fileStream = new FileStream(poseidonPath, FileMode.Create, FileAccess.Write);
    fileStream.Write(mainCharBytes);
    fileStream.Write(supportCharBytes);
}

await CreatePoseidonFileAsync();

// ＝＝＝＝＝＝＝FileStreamで読みたいじゃんプロジェクト＝＝＝＝＝＝＝

// async Task ReadPoseidonFile()
// {
//     // FileStreamを読み取りように作成
//     // FileMode.Open→ファイルが存在するなら開く、ないならFileNotFoundExceptionが投げられる
//     await using FileStream fileStream = new FileStream(poseidonPath, FileMode.Open, FileAccess.Read);
//     // TODO: fileStream.Lengthとはなんだろう？
//     // ファイルの中身を読むために必要なバイト配列を用意。中身まだ空？
//     byte[] bytes = new byte[fileStream.Length];
//     // ファイルからByte配列を読み取る
//     // 引数1つ目は、読み取ったバイトをいれる空の配列。ReadAsyncの引数2つ目はなに？第2引数は読み取ったデータをいれ始めるインデックスを指定、読み取る最大のインデックスは第三引数
//     // 戻り値は読み取れたbyteの数
//     int readCountNum = await fileStream.ReadAsync(bytes, 0, bytes.Length);
//     // 読み取った内容をUTF8の文字列に戻す
//     // bytesの中身を、インデックス0からbytesの最後まで読み取りますよの意味？
//     // どうしてfileStream.LengthじゃなくてreadCountNumを使うの？👉️ReadAsyncで読めた分だけ文字列に変換したほうが安全だから
//     string text = Encoding.UTF8.GetString(bytes, 0, readCountNum);
//     Console.WriteLine($"読み取れた内容：{text}");
//     Console.WriteLine($"読み取れたByteの数？：{readCountNum}");
// }

// await ReadPoseidonFile();


// ファイル読み取りFileStreamバージョン再練習！

async Task ReadPoseidonFileAsync()
{
    // File.ReadAsync(bytes, 0, ファイルのbyte数)
    await using FileStream fileStream = new FileStream(poseidonPath, FileMode.Open, FileAccess.Read);
    // 配列の長さを指定するときは角括弧を使うんだよ`[]`
    byte[] bytes = new byte[fileStream.Length];
    // 文字列に直す時はGetString(読み取ったbyte[],スタートしたいインデックス, 読み取れたbyteの数 )
    int readCountNum = await fileStream.ReadAsync(bytes, 0, bytes.Length);
    string text = Encoding.UTF8.GetString(bytes, 0, readCountNum);
    Console.WriteLine($"受け取れたテキスト：{text}");
}

await ReadPoseidonFileAsync();