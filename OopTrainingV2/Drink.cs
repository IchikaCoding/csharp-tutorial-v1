using System;
using System.Collections.Generic;
using System.Text;

namespace OopTrainingV2
{
    public class Drink : MenuItem
    {
        // これでDiscount()を実行した時しかセットできないようなったのかな？
        public double DiscountValue { get; private set; }
        public override void Describe()
        {
            Console.WriteLine("これは飲み物です");
        }
        public Drink(string name, double price, bool isSoldOut) : base(name, price, isSoldOut) { }

        //public bool Discount()
        //{
        //    // 50円引き
        //    var result = Price - 50;
        //    if (result >= 0)
        //    {
        //        DiscountValue = result;
        //        // プロパティって返り値になったりする？初めてみた！
        //        return true;
        //    }
        //    else
        //    {
        //        Console.WriteLine("0円未満となるため割引出来ません");
        //        // ここにリターンを書かないといけないけど何も返すものがない
        //        // boolにして解決
        //        return false;
        //    }
        //}
        public override double GetDiscountedPrice()
        {
            var result = Price - 50;
            if (result >= 0)
            {
                DiscountValue = result;
                return DiscountValue;
            }
            else
            {
                // ここにリターンを書かないといけないけど何も返すものがない
                // boolにして解決
                Console.WriteLine("0円未満となるため割引出来ません");
                return 0;
            }
        }

    }
}
