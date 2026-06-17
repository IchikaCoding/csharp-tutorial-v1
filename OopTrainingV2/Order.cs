using System;
using System.Collections.Generic;
using System.Text;

namespace OopTrainingV2
{
    public class Order
    {
        //private MenuItem _meneItem;

        // 売り切れの商品は注文に追加されないように修正
        public void Add(MenuItem menuItem)
        {
            List<MenuItem> menuItems = new List<MenuItem>();
            menuItems.Add(menuItem);
            Console.WriteLine($"{menuItem.Name}を注文しました");
        }
       //public Order(MenuItem menuItem)
       // {
       //     _meneItem = menuItem;
       // }
       
    }
}
