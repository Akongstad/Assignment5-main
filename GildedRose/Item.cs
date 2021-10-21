
public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }

        public virtual void UpdateQuality(){
            if(Quality > 0){
            Quality --;
            }
            
            SellIn--;

            if (SellIn < 0 && Quality > 0)
            {
                Quality --;
            }
    }

public class ConjuredItem : Item{
        public override void UpdateQuality(){
            if(Quality == 1){Quality--;}
            else if (Quality > 0)
            {
                Quality -= 2;
            }
            SellIn--;
            if(SellIn < 0)
            {
                if(Quality == 1){Quality--;}
                else if (Quality > 0)
                {
                    Quality -= 2;
                }
            }
        }
}
public class LegendaryItem : Item{
        public override void UpdateQuality(){
            SellIn = 0;
        }
}
public class BackstagePassItem : Item{
        public override void UpdateQuality(){
            SellIn--;
            switch (SellIn)
            {
                case < 0:
                    Quality = 0;
                    break;
                case < 6:
                    Quality += 3;
                    break;
                case < 11:
                    Quality += 2;
                    break;
                default:
                    Quality++;
                    break;
            }
            if(Quality > 50) Quality = 50;
        }
}
public class AgedBrieItem : Item{
        public override void UpdateQuality(){
            if(Quality < 50){
                Quality++;
            }
            SellIn--;
        }
    }
}
