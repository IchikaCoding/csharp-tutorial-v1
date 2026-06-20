using System.ComponentModel.Design;
using System.Diagnostics;
Debug.WriteLine("Hello World");

// int[行数, 列数]
// int[,]で2次元配列？int[,,]で3次元配列？
// これってからの配列ができるの？
int[,] grid = new int[3, 4];  // 3行4列


// 値を入れる：grid[行, 列]
grid[0, 0] = 1;  // 0行0列
grid[1, 2] = 5;  // 1行2列

// 初期値と一緒に作る
// 直で値を代入していくタイプ？行と列は指定しないで値を最初からいれるってこと？
int[,] matrix = {
    { 1, 2, 3 },
    { 4, 5, 6 },
    { 7, 8, 9 }
};

// ================================================================
// 出力:
// 1 2 3
// 4 5 6

int[,] m =
{
    {1,2,3 },
    {4,5,6 },
};

// 行をぐるぐる
// 列をぐるぐる

//for(int row = 0; row < m.GetLength(0); row++)
//{
//    for (int col = 0; col < m.GetLength(1); col++)
//    {
//        Console.Write(m[row, col] + " ");
//    }
//    Console.WriteLine();
//}

// 全部の値を取得したい時はforeachでできるよん
foreach (int element in m)
{
    Console.WriteLine(element);
}

// =================================================================

// 3 は外側の配列の要素数。ジャグ配列では行の数と考えてOK
int[][] scores = new int[3][];

// scores は配列そのものなので、Debug.WriteLine しても中身は自動表示されない
// ToString() の結果として型名っぽいものが出る
Debug.WriteLine(scores);

// scores[0], scores[1], scores[2] は外側の配列のインデックス
// つまり何行目かを指定している
scores[0] = new int[] { 80, 90 };
scores[1] = new int[] { 70, 85, 95 };
scores[2] = new int[] { 100 };

// scores[0][1] は、0行目の1番目の値
// つまり 90
Debug.WriteLine(scores[0][1]);

// scores[0] は int[] 型、つまり配列そのもの
// だから中身ではなく型名っぽいものが表示される
Debug.WriteLine(scores[0]);

// ===================================================================
string[][] classes =
{
    new string[]{"伊藤","Bob"},
    new string[]{"ロイド","アーニャ", "ヨル"},
    new string[]{"いろは","ヤチヨ","かぐや"},
    new string[]{ "大変恐縮なのですが、息の根止めさせて頂いてもよろしいでしょうか？" }
};

// かぐやちゃん、おはよ～☀

Debug.WriteLine($"{classes[2][2]}ちゃん、おはよ～☀");

// ヨルさん：大変恐縮なのですが、息の根止めさせて頂いてもよろしいでしょうか？

Debug.WriteLine($"{classes[1][2]}さん : {classes[3][0]}");

Debug.WriteLine("==========================================");

// 全部一つずつ表示してみよう！
// クラスの中から、一人ずつ出す
// 行を回す、列を中で回す
for (int row = 0; row < classes.Length; row++)
{
    for(int col= 0; col < classes[row].Length; col++)
    {
        Debug.Write(classes[row][col]+ " ");
    }
    Debug.WriteLine("");
}
Debug.WriteLine("==========================================");

// ほぼ一分でかけた(*´σｰ｀)ｴﾍﾍ
// これ何間違えたら見直す
// varを書いた時点で型の意識うすそうint[]
foreach (var row in classes)
{
    // ここも型の意識うすそう
    foreach (var col in row)
    {
        Debug.Write(col+ " ");
    }
    Debug.WriteLine(" ");
}

// =====================================================

int[][] scores1 = new int[3][];

// 0行目を指定して表示してSystem.Int32[]
Debug.WriteLine("これ表示されている？", scores1[0]); // nullだった
// Debug.WriteLine(scores1[0][0]);だとNullReferenceExceptionが出る
// 理由は？👉️配列がnullでまだないのにnullの「0番目をください」と言っている

var array = new string[] { null, null };
Debug.WriteLine("例外？", array[0]);

Debug.WriteLine("=================練習問題01=====================");



int[][] numbers =
{
    new int[] { 10, 20 },
    new int[] { 30, 40, 50 },
    new int[] { 60 }
};

Console.WriteLine(numbers[1][0]); // 30


Debug.WriteLine("=================練習問題02=====================");

// 全部の点数の合計を出すコードを書いてみましょう。
// 👉️一つずつ取り出す処理だと思って間違えた。問題はしっかり読みましょう

int[][] scores2 =
{
    new int[] { 80, 90 },
    new int[] { 70, 85, 95 },
    new int[] { 100 }
};

// 合計結果を代入する先を用意
int sum = 0;

foreach (int[] row in scores2)
{
    // 配列から一つずつ要素取り出す
    foreach (int item in row)
    {
        sum += item;
    }
}
Debug.WriteLine($"合計：{sum}");