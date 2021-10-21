using System.Collections.Generic;

namespace GildedRose{
    public class GildedRoseShop{

        public IList<Item> Items{get;set;}
        
        public GildedRoseShop(IList<Item> Items = null){
            this.Items = Items;
        }
        
        public void addItem(Item item){
            Items.Add(item);
        }

        public void UpdateQuality(){
            foreach (var item in Items)
            {
                item.UpdateQuality();
            }
        }
        public void PrintList()
        {
            System.Console.WriteLine("name, sellIn, quality");
            for (var j = 0; j < Items.Count; j++)
            {
                System.Console.WriteLine(Items[j].Name + ", " + Items[j].SellIn + ", " + Items[j].Quality);
            }
            System.Console.WriteLine("");
             
        }
    }

}
