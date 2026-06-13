using System;
using System.Collections.Generic;
using System.Text;

namespace MyFirstApp
{
    public class Cat
    {
        // 名前のフィールド
        public string name = "まろ";
        // メソッドを2つ書く
        public string GetInfo()
        {
            return $"猫: {name}";
        }
        // ラムダ式体験会
        public string GetInfo2() => $"猫: {name}";
    }
}
