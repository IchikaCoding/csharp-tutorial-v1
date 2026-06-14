# メモ φ(. . ) ﾒﾓﾒﾓ

```cs
class Animal
{
    // virtual → 子クラスで上書きできる
    public virtual void Speak() => Console.WriteLine("...");
}

class Dog : Animal
{
    // override → 親の Speak を上書き
    public override void Speak() => Console.WriteLine("ワン！");
}

class Cat : Animal
{
    public override void Speak() => Console.WriteLine("ニャン！");
}

// 同じ型のリストに入れて同じメソッドを呼べる
// Animalのリストが作られている
// new()ってなに？👉️new List<Animal>()
// なんでインスタンスのリストを作ったの？
// これ初期化子っていうらしい
List<Animal> animals = new() { new Dog(), new Cat() };
// aはインスタンス
// DogのインスタンスのSpeak()が実行される
// CatのインスタンスのSpeak()が実行される
// インスタンスごとのSpeakを実行したかったからインスタンスごとListを作成してforeachで回せるようにしたのかな。
foreach (var a in animals)
    a.Speak();  // → ワン！, ニャン！

```
