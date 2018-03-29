using NFluent;
using NUnit.Framework;
using System.Collections.Generic;

namespace csharp
{
    [TestFixture]
    public class GildedRoseTest
    {
        [Test]
        public void Should_degrade_quality_after_each_day()
        {
            var items = new List<Item> { new Item { SellIn = 10, Quality = 20 } };
            var app = new GildedRose(items);
            app.UpdateQuality();
            Check.That(items[0].Quality).IsEqualTo(19);
            Check.That(items[0].SellIn).IsEqualTo(9);
        }

        [Test]
        public void Should_degrade_quality_faster_after_sell_in_date()
        {
            var items = new List<Item> { new Item { SellIn = 0, Quality = 20 } };
            var app = new GildedRose(items);
            app.UpdateQuality();
            Check.That(items[0].Quality).IsEqualTo(18);
            Check.That(items[0].SellIn).IsEqualTo(-1);
        }

        [TestCase(10)]
        [TestCase(-1)]
        public void Should_no_degrade_quality_lower_that_zero(int daysLeft)
        {
            var items = new List<Item> { new Item { SellIn = daysLeft, Quality = 0 } };
            var app = new GildedRose(items);
            app.UpdateQuality();
            Check.That(items[0].Quality).IsEqualTo(0);
        }

        [Test]
        public void Should_increase_aged_brie_quality_over_time()
        {
            var items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 10, Quality = 20 } };
            var app = new GildedRose(items);
            app.UpdateQuality();
            Check.That(items[0].Quality).IsEqualTo(21);
        }

        [Test]
        public void Should_increase_aged_brie_quality_by_2_adter_sellin_date()
        {
            var items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 0, Quality = 20 } };
            var app = new GildedRose(items);
            app.UpdateQuality();
            Check.That(items[0].Quality).IsEqualTo(22);
        }

        [TestCase("Aged Brie", 10, 50)]
        [TestCase("Aged Brie", -1, 50)]
        [TestCase("Backstage passes to a TAFKAL80ETC concert", 20, 50)]
        [TestCase("Backstage passes to a TAFKAL80ETC concert", 10, 49)]
        [TestCase("Backstage passes to a TAFKAL80ETC concert", 5, 48)]
        public void Should_not_increase_quality_past_50(string itemName, int daysLeft, int startQuality)
        {
            var items = new List<Item> { new Item { Name = itemName, SellIn = daysLeft, Quality = startQuality } };
            var app = new GildedRose(items);
            app.UpdateQuality();
            Check.That(items[0].Quality).IsEqualTo(50);
        }

        [TestCase(10)]
        [TestCase(-1)]
        public void Should_not_degrade_Sulfuras(int daysLeft)
        {
            var items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = daysLeft, Quality = 20 } };
            var app = new GildedRose(items);
            app.UpdateQuality();
            Check.That(items[0].Quality).IsEqualTo(20);
        }

        [Test]
        public void Should_increase_Backstage_Passes_quality_by_2_when_there_are_more_than_10_days_left()
        {
            var items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 11, Quality = 20 } };
            var app = new GildedRose(items);
            app.UpdateQuality();
            Check.That(items[0].Quality).IsEqualTo(21);
        }

        [TestCase(10)]
        [TestCase(9)]
        [TestCase(8)]
        [TestCase(7)]
        [TestCase(6)]
        public void Should_increase_Backstage_Passes_quality_by_2_when_there_are_between_10_or_6_days_left(int daysLeft)
        {
            var items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = daysLeft, Quality = 20 } };
            var app = new GildedRose(items);
            app.UpdateQuality();
            Check.That(items[0].Quality).IsEqualTo(22);
        }

        [TestCase(5)]
        [TestCase(4)]
        [TestCase(3)]
        [TestCase(2)]
        [TestCase(1)]
        public void Should_increase_Backstage_Passes_quality_by_3_when_there_are_5_days_left_or_less(int daysLeft)
        {
            var items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = daysLeft, Quality = 20 } };
            var app = new GildedRose(items);
            app.UpdateQuality();
            Check.That(items[0].Quality).IsEqualTo(23);
        }

        [Test]
        public void Should_drop_Backstage_Passes_quality_to_0_after_sellin_date()
        {
            var items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 20 } };
            var app = new GildedRose(items);
            app.UpdateQuality();
            Check.That(items[0].Quality).IsEqualTo(0);
        }
    }
}
