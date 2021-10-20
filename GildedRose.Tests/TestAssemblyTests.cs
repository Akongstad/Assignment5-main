using Xunit;
using GildedRose.Console;
using System.Collections.Generic;
using System.IO;
using System;

namespace GildedRose.Tests
{
    public class TestAssemblyTests
    {
        private readonly Program app;


        public TestAssemblyTests(){
            app = new Program()
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
        }

        [Fact]
        public void TestTheTruth()
        {
            Assert.True(true);
        }

        [Fact]
        public void Quality_Degrade_Twice_As_Fast_Expired()
        {
            //Arrange
            var expected = 4;
            var item = new Item {Name = "+10 Dexterity Vest", SellIn = 0, Quality = 6};
            app.Items.Add(item);
            
            //Act
            app.UpdateQuality();

            //Assert
            Assert.Equal(expected, app.Items[6].Quality);
        }

        [Fact]
        public void Main_output_before_refactor()
        {
            var expected = File.ReadAllText("../../..//output.txt").Trim();
            var writer = new StringWriter();
            System.Console.SetOut(writer);

            Program.Main(new string[0]);
            var output = writer.GetStringBuilder().ToString().Trim();

            Assert.Equal(expected, output);
        }

        [Fact]
        public void Quality_Never_Negative_by_updatingQuality_multiple_times()
        {
            for(int i = 0; i < 20; i++){
                app.UpdateQuality();
            }
            
            Assert.Equal(0, app.Items[0].Quality);
            Assert.Equal(0, app.Items[2].Quality);
            Assert.Equal(0, app.Items[5].Quality);

        }
        
        [Fact]
        public void Quality_Never_Negative_by_updatingQuality()
        {
            //Arrange
            var expected = 0;
            var item = new Item {Name = "+10 Dexterity Vest", SellIn = 0, Quality = 0};
            app.Items.Add(item);
            
            //Act
            app.UpdateQuality();

            //Assert
            Assert.Equal(expected, app.Items[6].Quality);
        }

        [Fact]
        public void Conjured_item_degrade_twice_as_fast_from_2()
        {
            //Arrange
            var expected = 0;
            var item = new Item.ConjuredItem {Name = "Conjured Mana Cake", SellIn = 5, Quality = 2};
            app.Items.Add(item);
            
            //Act
            app.UpdateQuality();

            //Assert
            Assert.Equal(expected, app.Items[6].Quality);
        }

        [Fact]
        public void Conjured_item_degrade_twice_as_fast_from_1()
        {
            //Arrange
            var expected = 0;
            var item = new Item.ConjuredItem {Name = "Conjured Mana Cake", SellIn = 5, Quality = 1};
            app.Items.Add(item);
            
            //Act
            app.UpdateQuality();

            //Assert
            Assert.Equal(expected, app.Items[6].Quality);
        }

        [Fact]
        public void Aged_Brie_Increase_In_Quality_Older()
        {
            var qualityDayOne = app.Items[1].Quality;
            
            for(int i = 0 ; i <10 ; i++){
                app.UpdateQuality();    
            }
            
            Assert.True(qualityDayOne < app.Items[1].Quality);
            

        }

        [Fact]
        public void Quality_Never_More_Than_Fifty()
        {            
            for(int i = 0 ; i <50 ; i++){
                app.UpdateQuality();    
            }
            
            Assert.True(app.Items[1].Quality <= 50);
        }

        [Fact]
        public void Sulfuras_Never_Has_To_Be_Sold()
        {
            var expected = 0;
            var sulfurasSellIn = app.Items[3].SellIn;
            Assert.Equal(expected, sulfurasSellIn);
            app.UpdateQuality();
            Assert.Equal(expected, sulfurasSellIn);
        }

        [Fact]
        public void Sulfuras_Never_Decrease_In_Quality()
        {
            var qualityStart = app.Items[3].Quality;
            
            for(int i = 0 ; i<20 ; i++){
                app.UpdateQuality();
            }
            
            Assert.True(qualityStart == app.Items[3].Quality);
        }

        [Fact]
        public void BackStagePasses_Increase_Quality_As_Sellin_Approaches()
        {
            //Arrange
            var expected = 1;
            var item = new Item.BackstagePassItem {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 12, Quality = 0};
            app.Items.Add(item);
            
            //Act
            app.UpdateQuality();

            //Assert
            Assert.Equal(expected, app.Items[6].Quality);
        }

        [Fact]
        public void BackStagePasses_Quality_Increase_By_2_On_10_Days_Or_Less()
        {
            //Arrange
            var expected = 2;
            var item = new Item.BackstagePassItem {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 0};
            app.Items.Add(item);
            
            //Act
            app.UpdateQuality();

            //Assert
            Assert.Equal(expected, app.Items[6].Quality);
        }
        
        [Fact]
        public void BackStagePasses_Quality_Increase_By_3_On_5_Days_Or_Less()
        {
            //Arrange
            var expected = 3;
            var item = new Item.BackstagePassItem {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 0};
            app.Items.Add(item);
            
            //Act
            app.UpdateQuality();

            //Assert
            Assert.Equal(expected, app.Items[6].Quality);
        }

        [Fact]
        public void BackStagePasses_Quality_To_0_After_Concert()
        {
            //Arrange
            var expected = 0;
            var item = new Item.BackstagePassItem {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 10};
            app.Items.Add(item);
            
            //Act
            app.UpdateQuality();

            //Assert
            Assert.Equal(expected, app.Items[6].Quality);
        }
        [Fact]
        public void Legendary_quality_80()
        {
           //Arrange
            var expected = 80;
            var item = new Item.LegendaryItem {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80};
            app.Items.Add(item);
            
            //Act
            app.UpdateQuality();

            //Assert
            Assert.Equal(expected, app.Items[6].Quality);
        }
    }
}