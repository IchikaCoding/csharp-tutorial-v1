using System;
using System.Collections.Generic;
using System.Text;

namespace MyFirstApp
{
    public class MathHelper
    {
        // 2倍する静的メソッド作成
        public static double Double(int num)
        {
            num = num * 2;
            return num;
        }
    }
}
