
using enum_training;

Console.WriteLine("Hello, World!");




// 使ってみよう
Direction d = Direction.Up;

// 文字として出力 
Console.WriteLine(d); // up
// 数字で出力
Console.WriteLine((int)d); // 0


// upと一致しているなら上へを表示

// dがupのほうなら上を表示
// dがdownなら下を表示


switch (d)
{
    // 式の結果と値1が一致したときの処理
    case Direction.Up: Console.WriteLine("上です");
        break;
    case Direction.Down: Console.WriteLine("下です");
        break;
    case Direction.left:
        Console.WriteLine("左です");
        break;
}

// ProductStatusの型のProduct.StatusにInStockを代入してみる
// まずProductインスタンスを作成
// その次にStatusにアクセスする
// 代入する
Product product = new Product();
product.Status = ProductStatus.InStock;
//product.Status = ProductStatus.OutOfIchika;




// 型列挙は使用したあと？
// どうして実行コードのうしろにかくの？
enum Direction
{
    Up,
    Down,
    Right,
    left
}

// ======================================

public enum ProductStatus
{
    InStock,      // 在庫あり
    OutOfStock,  // 在庫なし
    Discontinued // 販売終了
}

