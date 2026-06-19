using System;
using System.Collections.Generic;
using System.Text;

namespace OopTrainingV2
{
    public class Order
    {
        private double _totalAmount;
        private List<MenuItem> _menuItems = new List<MenuItem>();

        // 売り切れの商品は注文に追加されないように修正
        public void Add(MenuItem menuItem)
        {
            // これだと、毎回初期化されて、Listが常に一人
            // もしかしたら、変数をプロパティにして、そこに代入していく形ならいいかも！

            if (menuItem.IsSoldOut)
            {
                Console.WriteLine($"{menuItem.Name}は売り切れです");
                return;
            }   

            _menuItems.Add(menuItem);
            Console.WriteLine($"{menuItem.Name}を注文しました");
        }
       //public Order(MenuItem menuItem)
       // {
       //     _meneItem = menuItem;
       // }
       public void DisplayList()
        {
            Console.WriteLine("======リストの中身=====");
            foreach (var item in _menuItems)
            {
                Console.WriteLine($"{item.Name}");
            }
            
        }
       public double Sum()
        {
            foreach (var item in _menuItems)
            {
                _totalAmount += item.GetValue();
            }
            return _totalAmount;
        }
    }
}
