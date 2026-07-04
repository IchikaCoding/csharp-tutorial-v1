using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.VisualBasic;

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
        // public int StringToInt(string str)
        // {
        //     try
        //     {
        //         // checked式を使うとオーバーフローを検出可能
        //         checked{
        //         int val = 0;
        //         foreach (char c in str)
        //         {
        //             int i = CharToInt(c);
        //             if (i == -1)
        //             {
        //                 return -1;
        //             }
        //             val = val * 10 + i;
        //         }
        //         // コンソールはここじゃなくて実行するところじゃない？
        //         Console.WriteLine($"val: {val}");
        //         return val;
        //         }
        //     }
        //     catch(FormatException)
        //     {
        //         // TODO: エラーのmessageを表示したい
        //         Console.WriteLine("FormatExceptionですよ～");
        //         return -1;
        //     }
        //     // TODO: これってどんなとき？
        //     catch (OverflowException)
        //     {
        //         Console.WriteLine("OverflowException(桁あふれ)です");
        //         return -1;
        //     }
        //     // ここからの処理はreturn しなくてOK
        //     // 戻り値が返ったあとにfinallyが実行されているから？
        //     // usingステートメントでtry-finallyを省略して書くことができるらしい
        //     finally
        //     {
        //         Console.WriteLine("例外があってもなくて動く場所");
        //     }
        // }
        
        //char numString = (char)48;
        //Debug.WriteLine($"numString:{numString}");

        // Debug.WriteLine($"result: {result}"); // result: 6272339
        // Debug.WriteLine($"result2: {result2}");

// TODO:　例外を投げた時の戻り値って何になるの？
        public int StringToInt(string str)
        {
            // checked式を使うとオーバーフローを検出可能
            checked{
            int val = 0;
            foreach (char c in str)
            {
                int i = CharToInt(c);
                if (i == -1)
                {
                    A();
                }
                val = val * 10 + i;
            }
            return val;
            }
        }

        static void A() => throw new FormatException();
        
    }
}
