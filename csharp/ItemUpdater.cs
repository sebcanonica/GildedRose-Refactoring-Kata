namespace csharp
{
    class ItemUpdaterFactory
    {
        private const string AGED_BRIE = "Aged Brie";
        private const string SULFURAS = "Sulfuras, Hand of Ragnaros";
        private const string BACKSTAGE_PASS = "Backstage passes to a TAFKAL80ETC concert";
        private const string CONJURED_PREFIX = "Conjured";

        internal static ItemUpdater Create(Item item)
        {
            switch (item.Name)
            {
                case AGED_BRIE:
                    return new AgedBrieUpdater(item);
                case SULFURAS:
                    return new SulfurasUpdater(item);
                case BACKSTAGE_PASS:
                    return new BackstagePassUpdater(item);
                default:
                    if (item.Name != null && item.Name.StartsWith(CONJURED_PREFIX))
                        return new ConjuredUpdater(item);
                    else
                        return new ItemUpdater(item);
            }
        }
    }

    class ItemUpdater
    {
        protected Item _item;

        internal ItemUpdater(Item item)
        {
            _item = item;
        }

        public void UpdateItem()
        {
            UpdateItemQuality();

            UpdateItemSellIn();

            if (_item.SellIn < 0)
            {
                AdjustOutdatedItemQuality();
            }
        }

        protected virtual void UpdateItemQuality()
        {
            LowerQuality();
        }

        protected virtual void UpdateItemSellIn()
        {
            _item.SellIn = _item.SellIn - 1;
        }

        protected virtual void AdjustOutdatedItemQuality()
        {
            LowerQuality();
        }                

        protected void LowerQuality()
        {
            if (_item.Quality > 0)
            {
                _item.Quality = _item.Quality - 1;
            }
        }

        protected void RaiseQuality()
        {
            if (_item.Quality < 50)
            {
                _item.Quality = _item.Quality + 1;
            }
        }
    }

    class AgedBrieUpdater : ItemUpdater
    {
        internal AgedBrieUpdater(Item item) : base(item) { }

        protected override void UpdateItemQuality()
        {
            RaiseQuality();
        }

        protected override void AdjustOutdatedItemQuality()
        {
            RaiseQuality();
        }
    }

    class SulfurasUpdater : ItemUpdater
    {
        internal SulfurasUpdater(Item item) : base(item) { }

        protected override void UpdateItemQuality() { }

        protected override void UpdateItemSellIn() { }

        protected override void AdjustOutdatedItemQuality() { }
    }

    class BackstagePassUpdater : ItemUpdater
    {
        internal BackstagePassUpdater(Item item) : base(item) { }

        protected override void UpdateItemQuality() {
            RaiseQuality();

            if (_item.SellIn < 11)
            {
                RaiseQuality();
            }

            if (_item.SellIn < 6)
            {
                RaiseQuality();
            }
        }

        protected override void AdjustOutdatedItemQuality() {
            _item.Quality = 0;
        }
    }

    internal class ConjuredUpdater : ItemUpdater
    {
        public ConjuredUpdater(Item item) : base(item) { }

        protected override void UpdateItemQuality()
        {
            base.UpdateItemQuality();
            base.UpdateItemQuality();
        }

        protected override void AdjustOutdatedItemQuality()
        {
            base.AdjustOutdatedItemQuality();
            base.AdjustOutdatedItemQuality();
        }
    }
}