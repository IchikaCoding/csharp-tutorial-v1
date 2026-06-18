using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;

namespace OopTrainingV2
{
    public class MenuItem
    {
        private double _price;
        public string Name { get; set; }
        // Priceはマイナスを許容しない
        // 条件の書き方がわからない👉️valueとフィールドを使用したら行ける！
        // Priceの値のバリエーションはこのsetに入れるべき？
        public double Price { get { return _price; } set { if (value >= 0) _price = value;  }}
        public bool IsSoldOut { get; set; }

        public virtual void Describe()
        {
            Console.WriteLine("これは商品説明です。");
        }
        
        // コンストラクタが3つの引数を持っている。継承した後はどうする？
        // 👉️baseで親クラスに渡せば取得可能
        public MenuItem(string name, double price, bool isSoledOut)
        {
            Name = name;
            // ここにIf文はいらない。
            // なぜかというと、Priceに値を代入するとsetメソッドが動いて条件を確認してくれるから（？）
            // Priceプロパティに代入されたらValueに入る
            // ValueはC# が set の中だけで自動的に用意してくれます。
            Price = price;
            IsSoldOut = isSoledOut;
        }
    }
}
