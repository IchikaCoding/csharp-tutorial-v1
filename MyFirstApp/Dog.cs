using System;
using System.Collections.Generic;
using System.Text;

namespace MyFirstApp
{
    public class Dog
    {
        // ポチという名前を作成
        // _nameはフィールド。クラス内でしか使わない変数だから_nameにした
        private string _name;
        // TODO: プロパティを書いてみよう！
        private int _age;
        // プロパティ
        public int Age
        {
            // 読むときの処理。Ageを読むと_ageの値が取れる
            get;
            // 書き込む時の処理。valueって何？→Dog.Ageに代入する値
            set {
                if (value >= 0)
                {
                    _age = value;
                }
            }
        }
        // 吠えるメソッド作成
        // this.を使用すると、このクラスのフィールドですよってわかりやすい。省略可能？
        public void Bark()
        {
            Console.WriteLine($"{this._name}さんが吠えた");
        }
        public void Display()
        {
            Console.WriteLine($"年齢は{this._age}さいです！");
        }
        // コンストラクタ。戻り値なし。
        public Dog(string name, int age)
        {
            this._name = name;
            _age = age;

            Console.WriteLine($"こんちか！{_name}({_age}歳)だよ、よろちく✨️");
        }
        // staticメソッドではthisで指名できない！
        // インスタンスを作成しないからです
        public static void StaticMethod()
        {
            //Console.WriteLine($"{this._name}さんが吠えた");
        }
    }
}
