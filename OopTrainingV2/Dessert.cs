using System;
using System.Collections.Generic;
using System.Text;

namespace OopTrainingV2
{
    public class Dessert : MenuItem
    {
        public override void Discribe()
        {
            Console.WriteLine("これはデザートです");
        }
        public Dessert(string name, double price, bool isSoldOut) : base(name, price, isSoldOut) { }
    }
}
