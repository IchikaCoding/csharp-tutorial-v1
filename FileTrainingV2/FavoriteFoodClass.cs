using System;
using System.Collections.Generic;
using System.Text;

namespace FileTrainingV2
{
    // クラスを作成、食べ物の名前、更新した時刻
    public class FavoriteFoodClass
    {
        public List<string> foods { get; set; } = new List<string>();
        public DateTime upDateTime { get; set; }
    }
}