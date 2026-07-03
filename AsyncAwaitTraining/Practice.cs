using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AsyncAwaitTraining
{
    public class Practice
    {
        public int CharToInt(char c)
        {
            if (c < '0' || '9' < c)
            {
                Console.WriteLine("失敗処理");
                throw new FormatException();
                
            }
            Console.WriteLine("成功処理が動いてます♡");
             // '0'は10進数で48の意味っぽいかも 
            return c - '0';
        }
        // 2026-07-03-try-catchを実装してみよう
        // TODO: これってtry-catchをStringToInt()に書くのはミス？
        public int StringToInt(string str)
        {
            try
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
                Console.WriteLine($"val: {val}");
                return val;
            }
            catch(FormatException)
            {
                // TODO: エラーのmessageを表示したい
                Console.WriteLine("FormatExceptionですよ～");
                return -1;
            }
            // TODO: これってどんなとき？
            catch (OverflowException)
            {
                Console.WriteLine("OverflowException(桁あふれ)です");
                return -1;
            }
            // ここからの処理はreturn しなくてOK
            // 戻り値が返ったあとにfinallyが実行されているから？
            // usingステートメントでtry-finallyを省略して書くことができるらしい
            finally
            {
                Console.WriteLine("例外があってもなくて動く場所");
            }
        }
        
        //char numString = (char)48;
        //Debug.WriteLine($"numString:{numString}");

        // Debug.WriteLine($"result: {result}"); // result: 6272339
        // Debug.WriteLine($"result2: {result2}");

    }
}
