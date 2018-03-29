using System;

namespace csharp
{
    class ItemUpdaterFactory
    {
        private const string AGED_BRIE = "Aged Brie";
        private const string SULFURAS = "Sulfuras, Hand of Ragnaros";
        private const string BACKSTAGE_PASS = "Backstage passes to a TAFKAL80ETC concert";
        private const string CONJURED_PREFIX = "Conjured";

        internal static AbstractItemUpdater Create(Item item)
        {
            if (item.Name != null)
            {
                if (item.Name == AGED_BRIE)
                    return new AgedBrieUpdater(item);
                else if (item.Name == SULFURAS)
                    return new SulfurasUpdater(item);
                else if (item.Name == BACKSTAGE_PASS)
                    return new BackstagePassUpdater(item);
                else if (item.Name.StartsWith(CONJURED_PREFIX))
                    return new ConjuredUpdater(item);
            }
            return new StandardItemUpdater(item);
        }
    }

    abstract class AbstractItemUpdater
    {
        protected Item _item;

        internal AbstractItemUpdater(Item item)
        {
            _item = item;
        }

        public void UpdateItem()
        {
            if (_item.SellIn > 0 )
            {
                UpdateFreshItemQuality();
            }
            else
            {
                UpdateObsoleteItemQuality();
            }

            UpdateItemSellIn();            
        }

        protected abstract void UpdateFreshItemQuality();

        protected abstract void UpdateObsoleteItemQuality();

        protected abstract void UpdateItemSellIn();

        protected void LowerQuality(int loss)
        {
            _item.Quality = Math.Max(0, _item.Quality - loss);
            
        }

        protected void RaiseQuality(int gain)
        {
            _item.Quality = Math.Min(50, _item.Quality + gain);            
        }
    }


    class StandardItemUpdater : AbstractItemUpdater
    {
        internal StandardItemUpdater(Item item) : base(item) { }        

        protected override void UpdateFreshItemQuality()
        {
            LowerQuality(1);
        }

        protected override void UpdateObsoleteItemQuality()
        {
            LowerQuality(2);
        }

        protected override void UpdateItemSellIn()
        {
            _item.SellIn = _item.SellIn - 1;
        }              
    }

    class AgedBrieUpdater : StandardItemUpdater
    {
        internal AgedBrieUpdater(Item item) : base(item) { }

        protected override void UpdateFreshItemQuality()
        {
            RaiseQuality(1);
        }

        protected override void UpdateObsoleteItemQuality()
        {
            RaiseQuality(2);
        }
    }

    class SulfurasUpdater : AbstractItemUpdater
    {
        internal SulfurasUpdater(Item item) : base(item) { }

        protected override void UpdateFreshItemQuality() { }

        protected override void UpdateObsoleteItemQuality() { }

        protected override void UpdateItemSellIn() { }
    }

    class BackstagePassUpdater : StandardItemUpdater
    {
        internal BackstagePassUpdater(Item item) : base(item) { }

        protected override void UpdateFreshItemQuality() {
            if (_item.SellIn < 6)
                RaiseQuality(3);
            else if (_item.SellIn < 11)
                RaiseQuality(2);
            else
                RaiseQuality(1);            
        }

        protected override void UpdateObsoleteItemQuality() {
            _item.Quality = 0;
        }
    }

    class ConjuredUpdater : StandardItemUpdater
    {
        internal ConjuredUpdater(Item item) : base(item) { }

        protected override void UpdateFreshItemQuality()
        {
            LowerQuality(2);
        }

        protected override void UpdateObsoleteItemQuality()
        {
            LowerQuality(4);
        }
    }
}