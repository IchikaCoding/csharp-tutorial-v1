using Microsoft.AspNetCore.Mvc;

namespace MvcMovie.Controllers
{
    // このControllerクラスのControllerの手前までの名前がURLのセグメント（？）になるらしい
    public class IchikaController : Controller
    {
        // 既定のメソッドだから、indexと書かなくても呼び出さるメソッド
        // GET: Ichika/index か　ichikaだけでもアクセスできる
        public string Index()
        {
            return "本当にこれでアクセスできたのかい？";
        }
        // GET: Ichika/pochipochi
        public string PochiPochi()
        {
            return "毎日お勉強✨️";
        }
    }
}