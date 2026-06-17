using System;
using System.Collections.Generic;
using System.Text;

namespace OopTrainingV2
{
    public class Drink : MenuItem
    {
        public override void Discribe()
        {
            Console.WriteLine("これは飲み物です");
        }
        public Drink(string name, double price, bool isSoldOut) : base(name, price, isSoldOut) { }
    }
}
