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