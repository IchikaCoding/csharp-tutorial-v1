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
        async Task WaitingForIchika()
        {
            // この処理は時間がかかるから待つ
            // awaitでTask型の値が帰っている？
            await Task.Delay(3000);
            Console.WriteLine("終わりました");
        }

        // ＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝

        //Thread.Sleep(3000);

        // これawaitがないと実行できない
        // なぜなら？WaitingForIchika()自体も3000ms止まるし、
        // マトリョーシカみたいにawaitを使い続けないといけないような気がしています
        await WaitingForIchika();
        // これは実行出来た。
        Console.WriteLine("最終コード");
    }
}

// 非同期処理なのに、3秒間その場で待っているらしい
// 待っている間を有効活用するってことで言えば、
// await boiling; を待っている間に、
// Console.WriteLine("④ コーヒー完成！");を先に出力したほうがいいのでは？