using MyFirstApp;
using System.ComponentModel;
// 改行あり
// Consoleはクラス、WriteLine()はクラスのメソッド？
// ""の中身はLiteral文字列

Console.WriteLine("This is the second line.");

// 改行なし
Console.Write("Hello");
Console.Write("Ichika");
Console.Write("｡°(°¯᷄◠¯᷅°)°｡");


// 演習

Console.WriteLine("b");
Console.WriteLine(123);
// floatリテラルを作成するためにF追加
Console.WriteLine(0.25f); // 0.25
// doubleLiteral
Console.WriteLine(3.141592); // 3.141592
// decimal 型
Console.WriteLine(3.141592m); // 3.141592

// boolLiteral
Console.WriteLine(true); // True
Console.WriteLine(false); // Flase



// Output: 123
Console.WriteLine("123");
// Output: 123
Console.WriteLine(123);

// Output: true
Console.WriteLine("true");
// Output: True
Console.WriteLine(true);

// 値を代入しているから型推論が働くのでvarでOK
var firstName = "ichika";

// 変数名にデータ型をいれると良くないらしいけどこれはどうなの？
string stringName = "name";



char userOption;

int gameScore;
// decimalっぽさある？
decimal particlesPerMillion;
// これってboolっぽさある？
bool processedCustomer;


// =============2026/06/04=================

// 値の代入はset操作というらしい
//string LastName = "ichika";

string LastName;
LastName = "ichika";
Console.WriteLine($"LastName: {LastName}");

string Name = "Bob";
Console.WriteLine($"Name: {Name}");

// messageってインスタンスなの？これは変数じゃないの？
// 型推論ができるときのみ使用可能→初期化しないなら使えない
//varはどうして使うの？
// あとから型を決めたい時。
// 初期化するなら型がわかりきっているからvarで省略してかけたらラク
var message = "Hello, Ichika";


// ---------------------------------

// ボブと3と34.4を格納
// 正しいデータ型を選択
// 出力：Hello, Bob! You have 3 messages in your inbox. The temperature is 34.4 celsius.

string bob = "Bob";
int count = 3;
double temperature = 34.4;

// 後ろにスペースいれると見やすいよ
Console.Write($"Hello, {bob}! ");
Console.Write($"You have {count} messages in your inbox. ");
Console.Write($"The temperature is {temperature} celsius.");

// =========文字エスケープ シーケンス============

Console.WriteLine("Hello\nWorld!");
// 間にタブのスペースが入る
Console.WriteLine("Hello\tWorld!");
Console.WriteLine("Hello \"World\"");

Console.WriteLine("c:\\source\\repos");


Console.WriteLine("Generating invoices for customer \"Contoso Corp\" ... \n");
Console.WriteLine("Invoice: 1021\t\tComplete!");
// WriteLine()は出力の前に改行をいれる
Console.WriteLine("Invoice: 1022\t\tComplete!");
// 1行空行を作成したいから先頭に\n
// コロンのあとにスペースが欲しいから\tをいれる
Console.Write("\nOutput Directory:\t");


// =========逐語的文字列リテラル============

// @ を使用すると、内側の文字列でエスケープされなくなる
Console.WriteLine(@"    c:\source\repos    
        (this is where your code goes)");

#region いちがどんのひとりごと

Console.WriteLine("こんちか✨️(´;ω;｀)");

#endregion

#region 2026-06-06

// クラスを作成してみよう
var dog = new Dog("ぽち",22); // コンストラクタで表示される年齢は22歳
dog.Bark();
// 代入？Ageのセット、書き込む処理をしている
dog.Age = 25;
// ここで表示するメソッドを使用
dog.Display(); // 25歳って表示される



// intとint[]で値が変化するかやってみよう！

// 静的メソッドの利点はなに？
static int Additon(int num)
{
    num = num + 10;
    return num;
}

int n = 15;
Console.WriteLine(Additon(n));  // 25
Console.WriteLine(n);  // 15

// 参照型
static int[] ChangeFirst(int[] array)
{
    array[0] = 15;
    return array;
}

int[] hairetsu = { 1, 2, 3 };
//  ChangeFirstを実行した後に表示が変わるのかを調べる
Console.WriteLine(ChangeFirst(hairetsu));　// System.Int32[]
// for文で回したらこれをまとめて書けるかも！
Console.WriteLine(hairetsu[0]); // 15
Console.WriteLine(hairetsu[1]); // 2
Console.WriteLine(hairetsu[2]); // 3


int x = 5;
int y = x; // yは5
y = 100; // yは100
Console.WriteLine(x); // 5。どうして？値型だから

// ------------------------------コンストラクタ---------------------------



#endregion

#region 2026-06-07

Console.WriteLine("===================2026-06-07==================="); 
double result = MathHelper.Double(2);
Console.WriteLine(result); // 4
// インスタンスを作成してメソッドを実行するのが難しい
var doubleInstance = new MathHelper();
// staticメソッドだからインスタンスを作成出来ない
//double resultTow = doubleInstance.Double(2);

// ---------------staticの練習------------------

// インスタンス不要。
// countの中身をみてみよう
int countNum = Sample.count;
string appName = Sample.AppName;
Console.WriteLine($"countNum: {countNum}, appName: {appName}"); // countNumは15、"MyApp"
Sample.Hello();
Sample.AppName = "Ichika's App";
Console.WriteLine($"appName: {Sample.AppName}"); // "Ichika's App" →"MyApp"になった。なぜ？

// これはエラー
// 理由は、public や private、static などのアクセス修飾子はクラスのメンバー（フィールド・プロパティ・メソッド）に使うもの
//static string name = "ichika";

#endregion


#region 2026-06-08(ラムダ式)

//// 2倍してリターン
//// 足し算を書いてみる
//n => n * 2;

//// ラムダ式は、複数の引数も行けます
//(a, b) => a + b;

//// 2倍にして返す
//n => {
//    int result = n * 2;
//    return result;
//}

// Func<引数の型, 戻り値の型>
Func<int, int> twice = n => n * 2;
Console.WriteLine(twice(5));  // → 10

// Func<入力1, 入力2, 戻り値>
Func<int, int, int> add = (a, b) => a + b;
Console.WriteLine(add(3, 4));  // → 7

// Action（戻り値なし）
// nameにはどうやって値を代入するの？
// →この name には、greet を呼び出すときに渡した値が入ります。
Action<string> greet = name => Console.WriteLine($"Hello, {name}");
greet("いちか");  // → Hello, いちか


#endregion


#region 2026-06-09(LINQ)

// モックデータをList形式で作成
var mockData = new List<int> { 1, 2, 3, 4 };

// 偶数と一致したらその数を代入する条件フィルター
var evenNum = mockData.Where(n => n % 2 == 0);
// 出力：evenNum: System.Linq.Enumerable+ListWhereIterator`1[System.Int32]
Console.WriteLine($"evenNum: {evenNum}");
// 元のlistの要素を2倍ずつする
// JSでいうと、mapみたいなもの。一つずつの要素に対して処理するやつ。
var twice2 = mockData.Select(n => n * 2);
// 出力：twice2: System.Linq.Enumerable+ListSelectIterator`2[System.Int32,System.Int32]

// 値を出力してみよう！
// evenNumのListからコンソール出力
foreach (var num in evenNum)
{
    Console.WriteLine(num);
}

// ToList()によって即時実行されるはずだからtempListをコンソールに表示するとリストの中身が見えると思ったのです。見えなかった。Why？
var tempList = twice2.ToList();
// 遅延実行だからそれをToList()で実行→その結果をカンマ区切りで表示した
Console.WriteLine($"twice2: {string.Join(", ", tempList)}"); // twice2: 2, 4, 6, 8


#endregion


#region 2026-06-10(LINQ)

// 条件は一つでも当てはまったらtrueになるの？
var mockData2 = new List<int> { 1, 2, 3, 4 };
bool hasLarge = mockData2.Any(n => n > 4); // false
bool hasLarge2 = mockData2.Any(n => n > 1); // true
Console.WriteLine($"hasLarge: {hasLarge}, hasLarge2: {hasLarge2}");


// ＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
Console.WriteLine("======================================");
// リスト作成
// 昇順sort（キーにしたいものをセット）
// 犬の年齢でもsortしてみよう

List<string> aiNames = new List<string> { "Chappy", "Gemini", "Claude" };
// TODO: new List<string>を省略してもいいらしい？！
// varで宣言した時はnew を省略不可。だけど、型で宣言したときはオブジェクトで書けるよ！
List<string> aiNames2 = ["Chappy", "Gemini", "Claude"];

// => のうしろのnは、sortで条件をセットする
var sorted = aiNames.OrderBy(n => n);
foreach (var item in sorted)
{
    Console.WriteLine(item);
}
Console.WriteLine("======================================");
// 犬の年齢もあるリスト
List<Ai> aiList = new List<Ai> { new Ai { Name = "Chappy", Age = 44 }, new Ai { Name = "Gemini", Age = 10}, new Ai { Name = "Claude", Age = 20 } };

// 昇順sort（年齢順にする）.まだ実行されていない
var sortedAi = aiList.OrderBy(n => n.Age);
foreach (var item in sortedAi)
{
    // インスタンスからNameを表示する
    Console.WriteLine(item.Name);
}

#endregion


#region Ai要素のリストを作る方法を探す

List<Ai> aiList2 = new List<Ai>
{ 
    new Ai { Name = "Chappy", Age = 44 },
    new Ai { Name = "Gemini", Age = 10 },
    new Ai { Name = "Claude", Age = 20 }
};

List<Ai> aiInfoList = new List<Ai>();
Ai chappy = new Ai();
chappy.Name = "Chappy";
chappy.Age = 44;
aiInfoList.Add(chappy);

var chappy1 = aiInfoList[0];
Console.WriteLine(chappy1.Name);
#endregion