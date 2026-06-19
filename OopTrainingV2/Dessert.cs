using System;
using System.Collections.Generic;
using System.Text;

namespace OopTrainingV2
{
    public class Dessert : MenuItem
    {
        public override string Describe()
        {
            return "これはデザートです";
        }
        public Dessert(string name, double price, bool isSoldOut) : base(name, price, isSoldOut) { }
    }

}
