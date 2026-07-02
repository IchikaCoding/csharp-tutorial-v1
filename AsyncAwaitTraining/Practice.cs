using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AsyncAwaitTraining
{
    public class Practice
    {
        static int CharToInt(char c)
        {
            if (c < '0' || '9' < c)
            {
                Console.WriteLine("失敗処理");
                throw new FormatException();
                
            }
            Console.WriteLine("成功処理が動いてます♡");
            return c - '0';
        }

        // 2026-07-03-try-catchを実装してみよう
        static int StringToInt(string str)
        {
            int val = 0;
            foreach (char c in str)
            {
                int i = CharToInt(c);
                if (i == -1)
                {
                    return -1;
                }
                val = val * 10 + i;
            }
            return val;
        }
        // 文字列もASCIIコードで数値として表すことも一応可能
        int result = StringToInt("ichika");
        int result2 = StringToInt("1234");
        //char numString = (char)48;
        //Debug.WriteLine($"numString:{numString}");

        Debug.WriteLine($"result: {result}"); // result: 6272339
        Debug.WriteLine($"result2: {result2}");

    }
}
