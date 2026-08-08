using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace FileTraining
{
    static public class Practice1
    {
        static public void DisposeChallenge()
        {
            // これってnull許容参照型？
            StreamReader? reader = null;
            try
            {
                reader = new StreamReader("D:\\Dev\\csharp-tutorial-v1\\FileTraining\\Sample.txt");
                string text = reader.ReadToEnd();
                Console.WriteLine("DisposeChallenge()の結果↓");
                Console.WriteLine(text);
            }catch(IOException e)
            {
                Console.WriteLine("ファイルの読み取りができませんでした" + e.Message);
            }
            finally
            {
                Console.WriteLine("reader?.Dispose()が動いているはず・・・！");
                reader?.Dispose();
                
            }
        }
        static public void UsingChallenge()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("D:\\Dev\\csharp-tutorial-v1\\FileTraining\\IchikaDon.txt"))
                {
                    writer.WriteLine("さつまいも、とうもろこし、いちご、えび天");
                } // ここで自動でDisposeされる
            }
            catch (IOException e)
            {
                Console.WriteLine("ファイルの読み取りができませんでした" + e.Message);
            }
        }

        static public void UsingReadChallenge()
        {
            try
            {
                using (StreamReader reader = new StreamReader("D:\\Dev\\csharp-tutorial-v1\\FileTraining\\IchikaDon.txt"))
                {
                    string textAll = reader.ReadToEnd();
                    Console.WriteLine("textAllの中身これ：" + textAll);
                } // Disposeされる
            }catch(IOException e)
            {
                Console.WriteLine("ファイルの中身読めず、、、");
                Console.WriteLine(e.Message);
            }
        }

        static public void Challenge()
        {
            try
            {
                using StreamReader reader = new StreamReader("D:\\Dev\\csharp-tutorial-v1\\FileTraining\\IchikaDon.txt");
                string textAll = reader.ReadToEnd();
                Console.WriteLine("usingで括弧を使わないバージョン");
                Console.WriteLine("textAll:" + textAll);

            }catch(IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
