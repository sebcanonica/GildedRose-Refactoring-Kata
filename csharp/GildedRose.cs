using System.Collections.Generic;

namespace csharp
{
    public class GildedRose
    {
        IList<Item> Items;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            foreach (var item in Items )
            {
                UpdateItem(item);
            }
        }

        private static void UpdateItem(Item item)
        {
            var itemUpdater = ItemUpdaterFactory.Create(item);
            itemUpdater.UpdateItemQuality();

            itemUpdater.UpdateItemSellIn();

            if (item.SellIn < 0)
            {
                itemUpdater.AdjustOutdatedItemQuality();
            }
        }        
    }
}
