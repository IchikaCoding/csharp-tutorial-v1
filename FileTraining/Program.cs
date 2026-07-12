using System.IO;

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

        int? temp = null;
        int displayTemp = temp ?? -999;
        Console.WriteLine("displayTemp: " + displayTemp);
        Console.WriteLine("==============================");

        string name = "IchikaDon";
        string? newName = "Ichika";
        newName = null;
        Console.WriteLine("name: "+ name);
        Console.WriteLine("newName: " + newName);
    }
} 