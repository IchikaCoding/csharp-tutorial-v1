using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using AsyncAwaitTraining;
using static System.Net.WebRequestMethods;

internal class Program
{
    private static async Task Main(string[] args)
    {
        //Console.WriteLine("① お湯を沸かし始める");
        //Task boiling = BoilWaterAsync();   // 待たずに先へ進む（券だけ受け取る）

        //Console.WriteLine("② 沸いてる間に豆を挽く");
        //GrindBeans();                      // お湯を待たずに別作業
        //// これどうして動くの？awaitいらないタイプ？
        //Thread.Sleep(5000);
        //// これはBoilWaterAsync();をココで実行している
        //await boiling;                     // ③ ここでお湯が沸くのを待つ
        //Console.WriteLine("④ コーヒー完成！");


        //// Task型を返す
        //static async Task BoilWaterAsync()
        //{
        //    // 3秒遅らせる非同期処理
        //    await Task.Delay(3000);            // 3秒かかる作業のフリ
        //}

        //static void GrindBeans()
        //{
        //    Console.WriteLine("   ガリガリ…（豆を挽いた）");
        //}

        //// 返したいのは数字
        //// 星の数を数えてみよう
        //static async Task<int> CountStars()
        //{
        //    await Task.Delay(500);
        //    return 15;
        //}

        //int num = await CountStars();
        //Console.WriteLine($"星の数は、{num}個あります");

        //// ボタンのイベントハンドラを実装
        ////　Taskにした意味は？→WaitingForIchika()が時間がかかる処理
        //// 時間がかかる処理が入ってる関数もTask型で包まないといけないと思ったの。
        //async void ButtonHandler(object sender, EventArgs e)
        //{
        //    await WaitingForIchika();
        //}

        // 500ms待つ処理
        // async Task WaitingForIchika()
        // {
        //     // この処理は時間がかかるから待つ
        //     // awaitでTask型の値が帰っている？
        //     await Task.Delay(3000);
        //     Console.WriteLine("終わりました");
        // }

        // // ＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝

        // //Thread.Sleep(3000);

        // // これawaitがないと実行できない
        // // なぜなら？WaitingForIchika()自体も3000ms止まるし、
        // // マトリョーシカみたいにawaitを使い続けないといけないような気がしています
        // await WaitingForIchika();
        // // これは実行出来た。
        // Console.WriteLine("最終コード");


        // 実行コード
        // Practice practice = new Practice();
        // practice.StringToInt("ichika");
        // practice.StringToInt("12345");
        // // ここでオーバーフローする予定だった。でもなっていなかった。
        // // TODO: 結果はval: -2147483648となった理由は？
        // practice.StringToInt("2147483648");
        // 文字列もASCIIコードで数値として表すことも一応可能
        // int result = StringToInt("ichika");
        // int result2 = StringToInt("1234");
        
        Practice practice1 = new Practice();
        try
        {
           int result = practice1.StringToInt("2147483648");
        //    int result = practice1.StringToInt("2147483648");
            Console.WriteLine($"result: {result}");
        }
        catch(FormatException e)
        {
            Console.WriteLine("FormatExceptionですぅ");
            Console.WriteLine(e.Message);
            Console.WriteLine($"e: {e}");

        }catch(OverflowException e)
        {
             Console.WriteLine("OverflowExceptionですぅ");
            Console.WriteLine(e.Message);
            Console.WriteLine($"e: {e}");

        }
        finally
        {
            Console.WriteLine("Finallyですぅ");
        }
        Console.WriteLine("=================================================");

        // var x = true ? null : throw new Exception();
        // B関数を作成。引数はobject型で戻り値はstring
        static string B(object obj)
        {
            // objをstring型とする。もしnullなら、ArgumentExceptionの例外を投げる。
            // エラーmessageとして何が変えるのか不明。文字列でmessage書いていた、
            var s = obj as string ?? throw new ArgumentException($"{obj.GetType()}");
            // sの長さが0なら、Emptyを出す。5文字よりちいさいならショート、どっちにも当てはまらないならtoo longを出す
            // これって三項演算子と違う？？条件いっぱい書けるやつ？
            return s.Length == 0 ? "empty" : s.Length < 5 ? "short": throw new InvalidOperationException("too long");
        }

        try
        {
            // string bResult1 = B("ichika"); // too long
            var bResult2 = B(1234); // ArgumentException
            var bResult3 = B(""); // "empty"
            // Console.WriteLine($"bResult1: {bResult1}");
            Console.WriteLine($"bResult2: {bResult2}");
            Console.WriteLine($"bResult3: {bResult3}");
        }catch(ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.Message);
            
        }
        
        Console.WriteLine("=================================================");
        
        // 例外は伝播する
        // NotImplementedException はメソッドが未実装だよという意味
        static void Pochipochi() => throw new NotImplementedException();
        static void PoseiDon() => Pochipochi();
        static void ichikaDon() => PoseiDon();

        try
        {
            ichikaDon();
        }catch(NotImplementedException e)
        {
            Console.WriteLine(e);
        }

        Console.WriteLine("=================================================");
        //try
        //{
        //    Pochipochi();
        //}
        //catch (DirectoryNotFoundException e)
        //{
        //    Console.WriteLine(e);
        //}
        //catch (NotImplementedException e)
        //{
        //    Console.WriteLine(e);
        //}
        try
        {
            Pochipochi();
        }catch(Exception e) when( e is DirectoryNotFoundException || e is NotImplementedException)
        {
            Console.WriteLine("やるじゃん！");
            Console.WriteLine(e);
        }
        finally
        {
            Console.WriteLine("るんるん♪");
        }

        Console.WriteLine("=================================================");

        // TODO: Fがエラーになっちゃった
        // try
        // {
        //    // Fはintパラメーターを受け取るやつじゃないとダメかもしれない？
        //    // Console.WriteLineをFのいちに入れても動くかも？！
        //    // ! MSLearnのParallelクラスの引数の定義を調べる👉️今回使えそうな物を見つける👉️それに当てはまるようにメソッドを作成する
        //    // For(Int32, Int32, Action<Int32>)が定義。メソッドで、引数がint型のものを受け取れるってこと
        //     //  引数3つ目は関数の定義
        //    Parallel.For(0, 10000, F);
        //     // AnyはLINQで調べたら出てきそう！
        //     // InnerExceptionsは複数ある例外の中の例外を見るための物
        // }catch(AggregateException e) when (e.InnerExceptions.Any(i=> i is ArgumentException))
        // {
        //     Console.WriteLine("どうだ？！");
        // }
        // // 
        // static void F(int i) => throw new ArgumentException($"{i}が出ました");
        
        var numbers = new[] {1,2,3,4,5};
        var num = numbers.Any(i => i>4);
        Console.WriteLine($"num：{num}");
        // // null許容型というらしい
        // int? z = null;
        // // zがnullなら、-1をiに代入。（JSのnull合体演算子は、null とundefinedどっちかだったら右の値を使う）
        // int i = z ?? -1;
        // Console.WriteLine($"i: {i}");

        // ArgumentExeptionを例外フィルターする処理を書く
        void Funcy(int i) => throw new ArgumentException($"{i}回目の例外です");
        // 例外フィルターをwhenで書いてみよう
        try
        {
            Parallel.For(0,10,(i) => Funcy(i));
        }catch(AggregateException e) when (e.InnerExceptions.Any(exception => exception is ArgumentException))
        {
            Console.WriteLine($"例外！！！ArgumentExceptionが発生！直ちに、関数さんのお家に入りなさい");
        }
        
    }
}

// 非同期処理なのに、3秒間その場で待っているらしい
// 待っている間を有効活用するってことで言えば、
// await boiling; を待っている間に、
// Console.WriteLine("④ コーヒー完成！");を先に出力したほうがいいのでは？