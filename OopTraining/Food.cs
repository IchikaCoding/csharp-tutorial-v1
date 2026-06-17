using System;
using System.Collections.Generic;
using System.Text;

namespace OopTraining
{
    public class Food
    {
        // Nullを含みますっていう連絡が来るから空文字入れてみた
        public string Name { get; set; } = "";
        private int _price; 
        public int Price { get { return _price; } set { if (value >= 0) _price = value;  } }
        public void Eat()
        {
            Console.WriteLine($"{Name}を食べました。");
        }
        public virtual void Describe()
        {
            Console.WriteLine("これは食べ物です");
        }
    }
}
