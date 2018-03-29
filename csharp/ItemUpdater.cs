namespace csharp
{
    interface IItemUpdater
    {
        void UpdateItemQuality();

        void UpdateItemSellIn();

        void AdjustOutdatedItemQuality();
    }

    class ItemUpdaterFactory
    {
        private const string AGED_BRIE = "Aged Brie";
        private const string SULFURAS = "Sulfuras, Hand of Ragnaros";
        private const string BACKSTAGE_PASS = "Backstage passes to a TAFKAL80ETC concert";

        internal static IItemUpdater Create(Item item)
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
                    return new StandardUpdater(item);
            }
        }
    }

    class StandardUpdater : IItemUpdater
    {
        protected Item _item;

        internal StandardUpdater(Item item)
        {
            _item = item;
        }

        public virtual void UpdateItemQuality()
        {
            LowerQuality();
        }

        public virtual void UpdateItemSellIn()
        {
            _item.SellIn = _item.SellIn - 1;
        }

        public virtual void AdjustOutdatedItemQuality()
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

    class AgedBrieUpdater : StandardUpdater
    {
        internal AgedBrieUpdater(Item item) : base(item) { }

        public override void UpdateItemQuality()
        {
            RaiseQuality();
        }

        public override void AdjustOutdatedItemQuality()
        {
            RaiseQuality();
        }
    }

    class SulfurasUpdater : StandardUpdater
    {
        internal SulfurasUpdater(Item item) : base(item) { }

        public override void UpdateItemQuality() { }

        public override void UpdateItemSellIn() { }

        public override void AdjustOutdatedItemQuality() { }
    }

    class BackstagePassUpdater : StandardUpdater
    {
        internal BackstagePassUpdater(Item item) : base(item) { }

        public override void UpdateItemQuality() {
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

        public override void AdjustOutdatedItemQuality() {
            _item.Quality = 0;
        }
    }
}