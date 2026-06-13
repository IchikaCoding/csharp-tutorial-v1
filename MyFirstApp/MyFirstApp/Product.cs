using System;
using System.Collections.Generic;
using System.Text;

namespace MyFirstApp
{
    class Product
    {
        public static int Count;

        static Product()
        {
            Count = 100;
            Console.WriteLine("static コンストラクタ");
        }

        public Product()
        {
            Console.WriteLine("普通のコンストラクタ");
        }
    }
}
