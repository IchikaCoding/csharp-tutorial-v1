// これはなに？入出力？
using System.IO;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        String line;
        try
        {
            StreamReader sr = new StreamReader("D:\\Dev\\csharp-tutorial-v1\\FileTraining\\Sample.txt");
            line = sr.ReadLine();
            // nullじゃなかったとき
            while(line != null)
            {
                Console.WriteLine(line);
                // 2回読んでいるのはどうして？
                line = sr.ReadLine();
                // ここでクローズしているから、改行を挟むと例外となるのかな？
                sr.Close();
                // どうしてコンソールの入力を読むの？
                Console.ReadLine();
            }
        }catch(Exception e)
        {
            // 改行を入れたらここに入ったよ
            Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            Console.WriteLine("ここはFinallyブロックです");
        }

        //int? temp = null;
        //int displayTemp = temp ?? -999;
        //Console.WriteLine("displayTemp: " + displayTemp);
        //Console.WriteLine("==============================");

        //string name = "IchikaDon";
        //string? newName = "Ichika";
        //newName = null;
        //Console.WriteLine("name: "+ name);
        //Console.WriteLine("newName: " + newName);

        try
        {
            StreamWriter sw = new StreamWriter("D:\\Dev\\csharp-tutorial-v1\\FileTraining\\Text.txt");
            sw.WriteLine("こんちか, いちかどん");
            sw.WriteLine("From the StreamWriter class");
            sw.Close();
        }catch(Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            Console.WriteLine("ここはFinallyブロックです");
        }

        // これなんだ？64とは？
        Int64 x;
        try
        {
            // 引数1つ目：パス
            // 2つ目：trueなら追加モードで開かれる、falseならファイルの内容を上書きする👉️これやったらうえの処理が消えてしまった
            // 3つ目：エンコード方法を指定できる
            StreamWriter sw = new StreamWriter("D:\\Dev\\csharp-tutorial-v1\\FileTraining\\Text.txt", true, Encoding.UTF8);
            
            for(x=1; x < 11; x++)
            {
                sw.Write(x);
            }
            sw.WriteLine();
            sw.WriteLine("おいしいです");
            sw.Close();
        }catch(Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            Console.WriteLine("ここはFinallyブロックです");
        }
    }
} 