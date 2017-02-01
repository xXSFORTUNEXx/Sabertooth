using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Classes
{
    class Shop
    {
        public ShopItem[] shopItem = new ShopItem[25];

        public string Name { get; set; }

        public Shop()
        {
            for (int i = 0; i < 25; i++)
            {
                shopItem[i] = new ShopItem("None", 1, 0);
            }
        }

        public Shop(string name)
        {
            Name = name;

            for (int i = 0; i < 25; i++)
            {
                shopItem[i] = new ShopItem("None", 1, 0);
            }
        }
    }

    class ShopItem
    {
        public string Name { get; set; }
        public int ItemNum { get; set; }
        public int Cost { get; set; }

        public ShopItem() { }

        public ShopItem(string name, int cost, int itemnum)
        {
            Name = name;
            Cost = cost;
            ItemNum = itemnum;
        }
    }
}
