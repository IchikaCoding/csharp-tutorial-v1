using System;
using System.Collections.Generic;
using System.Text;

namespace MyFirstApp
{
    public class Sample
    {
        // フィールド、プロパティ、メソッド、コンストラクタで静的なものを作る
        public static int count = 0;
        public static string AppName { get; set; } = "MyApp";
        public static void Hello() => Console.WriteLine("Hello");
        // コンストラクタってアクセス修飾子不要？
        static Sample()
        {
            count = 15;
        }
    }
}
