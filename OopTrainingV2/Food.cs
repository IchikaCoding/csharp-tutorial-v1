using System;
using System.Collections.Generic;
using System.Text;

namespace OopTrainingV2
{
    public class Food : MenuItem
    {
        public override void Describe()
        {
            Console.WriteLine("これは食べ物です");
        }
        // baseのうしろって親クラスの値を使いますよってこと？
        // Foodインスタンス作成時に渡された値が`MenuItem` コンストラクタへ渡される
        // `MenuItem` 側で `Name`, `Price`, `IsSoldOut` がセットされる
        // そのあと `Food` の `{ }` の中身が実行される
        public Food(string name, double price, bool isSoldOut): base(name, price, isSoldOut) { }
    }
}
