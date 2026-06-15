using System;
using System.Collections.Generic;
using System.Text;

// namespaceは囲わなくても、以下の書き方でOKらしい
namespace OopTraining;

public class Cat : Animal
{
    public override void Speak()
    {
        Console.WriteLine("ぷにぷに");
    }
}

