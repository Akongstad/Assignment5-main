using System;
using System.Collections.Generic;
namespace GildedRose.Console
{
    public class Program
    {
        public IList<Item> Items;
        public static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new Program()
                          {
                              Items = new List<Item>
                                          {
                                              new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                                              new Item.AgedBrieItem {Name = "Aged Brie", SellIn = 2, Quality = 0},
                                              new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                                              new Item.LegendaryItem {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                                              new Item.BackstagePassItem
                                                  {
                                                      Name = "Backstage passes to a TAFKAL80ETC concert",
                                                      SellIn = 15,
                                                      Quality = 20
                                                  },
                                              new Item.ConjuredItem {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                                          }

                          };

            foreach (var item in app.Items)
            {
                System.Console.WriteLine("Name: " + item.Name + " SellIn: " + item.SellIn +" Quality:" + item.Quality);
                System.Console.WriteLine("");
            } 
            
            app.UpdateQuality();
            
            System.Console.WriteLine("UpdateQuality()");
            foreach (var item in app.Items)
            {
                System.Console.WriteLine("");
                System.Console.WriteLine("Name: " + item.Name + " SellIn: " + item.SellIn + " Quality:" + item.Quality);
            } 

            //System.Console.ReadKey();
        }
        public void UpdateQuality(){
            foreach (var item in Items)
            {
                item.UpdateQuality();
            }
        }
    }
}