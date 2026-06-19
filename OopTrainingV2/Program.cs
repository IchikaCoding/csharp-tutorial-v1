using OopTrainingV2;


// DesertとDrinkがほしい

MenuItem dessert = new Dessert("いちごタルト", 700, false);
MenuItem drink = new Drink("カフェラテ", 10, false);

List<MenuItem> menuItems = new List<MenuItem>{ dessert, drink };

foreach (var item in menuItems)
{
    Console.WriteLine(item.Name);
}


// Orderクラスを作成して、注文を簡単にしたい！
Order order = new Order();
order.Add(drink);
order.Add(dessert);


order.DisplayList();

Console.WriteLine(order.Sum());
Console.WriteLine(order.Sum());
Console.WriteLine(order.Sum());

order.Display();

//var order1 = new Order();

//order.Add(new Food ("カレー", 900, true);
//order.Add(new Drink { Name = "コーラ", Price = 250 });
//order.Add(new Dessert { Name = "プリン", Price = 400 });
//order.Add(new Food { Name = "ハンバーグ", Price = 1000, IsSoledOut = true });

//order.Display();