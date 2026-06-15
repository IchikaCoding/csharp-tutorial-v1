
using OopTraining;

internal class Program
{
    private static void Main(string[] args)
    {
        var dog = new Dog();
        dog.Name = "いちかどん";
        dog.Eat();
        dog.Bark();

        // 繰り返す処理のためにListを作成してみる
        // そのListを使用してぐるぐる回す

        // このインスタンス作成時の括弧って必要？
        List<Animal> animals = new() { new Dog(), new Cat() };
        foreach (var animal in animals)
        {
            animal.Speak();
        }


        // カレーを食べます

        Food food = new Food();
        food.Name = "カレー";
        food.Price = 900;
        Console.WriteLine(food.Price); // 900
        food.Eat();

        food.Price = -100;
        Console.WriteLine(food.Price); // 900
    }
}