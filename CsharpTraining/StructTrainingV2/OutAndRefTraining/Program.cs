

//void AddTen(ref int x)
//{
//    Console.WriteLine($"AddTenのメソッド内前：{x}");
//    x += 10;
//    Console.WriteLine($"AddTenのメソッド内後：{x}");

//}

//int num = 5;
//Console.WriteLine($"5を代入した後すぐのnum：{num}");
//AddTen(ref num);
//Console.WriteLine($"AddTenを実行した後すぐのnum：{num}");


//// 割り算の商とあまりを同時に返す

//void Divide(int a, int b, out int quotient, out int remainder)
//{
//    quotient = a / b;
//    remainder = a % b;
//}

//// 代入
//// 自分が指定した変数に結果をいれる事ができる？
//// outを使用しないで核としたらどうなる？
//// outを書いたら、処理をし終わったときの結果を入れておくことができるイメージ？
//Divide(10, 3, out int q, out int r);
//Console.WriteLine($"商:{q}, あまり:{r}");  // → 商:3, あまり:1


//// "123"を用意
//// tryParseをして、結果を表示
//// 失敗したらエラー表示

//string numString = "123";
////string numString = "いちか🍠";


//if (int.TryParse(numString, out int result))
//{
//    Console.WriteLine($"変換結果:{result}");
//}
//else{
//    Console.WriteLine("変換失敗");
//}



void AddFive(int x)
{
    x += 5;
}

int num = 10;
AddFive(num);

Console.WriteLine(num); // 10が出力される。値型だからです

// =========================================================


int hp = 100;
TakeDamage(ref hp, 30);
Console.WriteLine($"hp: {hp}"); // 70。メソッド実行後、元の変数も更新したいからrefにしました。

void TakeDamage(ref int hp, int damage)
{
    hp -= damage;
};


// =========================================================

// 文字列を受け取って、数値に変換して、変換した値を表示する処理
// TryParseをしてBool値を返す

// まず入力する
// その結果を受け取って、inputに代入
// もし、TryParseがエラーだったら失敗と表示する


// 引数の変数は最初から定義しなくて、引数の場所で型を定義する

bool TryParseSize(string text, out int width, out int height)
{
    // 
    width = 0;
    height = 0;

    string[] parts = text.Split(',');

    if (parts.Length != 2)
    {
        return false;
    }

    if (!int.TryParse(parts[0], out width))
    {
        return false;
    }

    if (!int.TryParse(parts[1], out height))
    {
        return false;
    }

    return true;
}

// ======================================================

void DoubleValue(ref int x)
{
    x *= 2;
}

int a = 3;
int b = a; // 3はコピーされている

DoubleValue(ref b); // xは6

Console.WriteLine(a); // 3　aは値型だから3のまま
Console.WriteLine(b); // 6  bは値型だけど、refによってDoubleValueが実行されたらbは更新されるから


void GetUserName(out string name)
{
    name = "Taro";
}

//この場合、out を使う必要は高い？👉️低い。
//return string のほうが自然？👉️自然
//理由も答えてください。👉️返したい値が一つだから。複数ならreturn出来ないからoutを使うといいけども。


void Swap(ref int a, ref int b)
{
    int temp = a; // aは10
    a = b;  // aは20
    b = temp; // tempとbは10
}

int x = 10;
int y = 20;

Swap(ref x, ref y);

Console.WriteLine(x); // 20
Console.WriteLine(y); // 10


//??? に入るものは？　👉️ref
//なぜ out ではなく ref？👉️xとyがSwapを実行したあとに更新されているから。










