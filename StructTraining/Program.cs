

Point p1 = new Point();
p1.X = 10; p1.Y = 20;
// p2の住所はnew Point();の手前と違うの？
// もしこえが手前なら、参照型になってしまうかも。
Point p2 = p1;  // コピーされる（値型！）
p2.X = 99;
Console.WriteLine(p1.X);  // → 10（変わらない）

// =================================


// インスタンスを生成するたびに住所変わるから値型っぽいと思ってしまう！
var pc1 = new PointClass();
pc1.X = 10; pc1.Y = 20;

var pc2 = pc1;  // 同じ場所を指す（参照型！）
pc2.X = 99;
Console.WriteLine(pc1.X);  // → 99（変わった！）

// int, doubleとかはstructで定義されている→だから値型（？）
// structと書いてあったらとりあえず値型だよねって思っておけばいいの？
struct Point
{
    public int X;
    public int Y;
}


class PointClass
{
    public int X;
    public int Y;
}
