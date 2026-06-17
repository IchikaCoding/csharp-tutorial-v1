using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;

namespace OopTrainingV2
{
    public class MenuItem
    {
        public string Name { get; set; }
        // Priceはマイナスを許容しない
        public double Price { get; set; }
        public bool IsSoldOut { get; set; }

        public virtual void Discribe()
        {
            Console.WriteLine("これは商品説明です。");
        }
        public double Discount()
        {
            // 50円引き
            var result = Price - 50;
            if(result < 0) { Console.WriteLine("0円未満となるため割引出来ません"); }
            return result;
        }
        // コンストラクタが3つの引数を持っている。継承した後はどうする？
        public MenuItem(string name, double price, bool isSoledOut)
        {
            Name = name;
            if(price >= 0)
            {
                Price = price;
            }
            IsSoldOut = isSoledOut;
        }
    }
}
