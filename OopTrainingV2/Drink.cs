using System;
using System.Collections.Generic;
using System.Text;

namespace OopTrainingV2
{
    public class Drink : MenuItem
    {
        public override void Describe()
        {
            Console.WriteLine("これは飲み物です");
        }
        public Drink(string name, double price, bool isSoldOut) : base(name, price, isSoldOut) { }

        public double Discount()
        {
            // 50円引き
            var result = Price - 50;
            if (result < 0) { Console.WriteLine("0円未満となるため割引出来ません"); }
            return result;
        }

    }
}
