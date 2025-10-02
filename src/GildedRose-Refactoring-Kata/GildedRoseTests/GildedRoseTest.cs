using System.Collections.Generic;
using GildedRoseKata;
using NUnit.Framework;

namespace GildedRoseTests;

public class GildedRoseTest
{
    private static Item UpdateSingle(Item item)
    {
        var app = new GildedRose(new List<Item> { item });
        app.UpdateQuality();
        return item;
    }

    // 1. Normal item before sell date, quality > 0 decreases by 1
    [Test]
    public void NormalItem_BeforeSellDate_QualityDecreasesBy1()
    {
        var item = UpdateSingle(new Item { Name = "foo", SellIn = 10, Quality = 7 });
        Assert.Multiple(() =>
        {
            Assert.That(item.SellIn, Is.EqualTo(9));
            Assert.That(item.Quality, Is.EqualTo(6));
        });
    }

    // 2. Normal item with zero quality does not go negative
    [Test]
    public void NormalItem_QualityZero_DoesNotGoNegative()
    {
        var item = UpdateSingle(new Item { Name = "foo", SellIn = 5, Quality = 0 });
        Assert.Multiple(() =>
        {
            Assert.That(item.SellIn, Is.EqualTo(4));
            Assert.That(item.Quality, Is.EqualTo(0));
        });
    }

    // 3. Normal item on sell date (SellIn=0) loses 2 quality after update
    [Test]
    public void NormalItem_OnSellDate_QualityDecreasesBy2()
    {
        var item = UpdateSingle(new Item { Name = "foo", SellIn = 0, Quality = 5 });
        Assert.Multiple(() =>
        {
            Assert.That(item.SellIn, Is.EqualTo(-1));
            Assert.That(item.Quality, Is.EqualTo(3));
        });
    }

    // 4. Normal item after sell date with zero quality stays at zero
    [Test]
    public void NormalItem_AfterSellDate_ZeroQualityStaysZero()
    {
        var item = UpdateSingle(new Item { Name = "foo", SellIn = 0, Quality = 0 });
        Assert.Multiple(() =>
        {
            Assert.That(item.SellIn, Is.EqualTo(-1));
            Assert.That(item.Quality, Is.EqualTo(0));
        });
    }

    // 5. Aged Brie before sell date increases quality by 1
    [Test]
    public void AgedBrie_BeforeSellDate_IncreasesBy1()
    {
        var item = UpdateSingle(new Item { Name = "Aged Brie", SellIn = 5, Quality = 10 });
        Assert.Multiple(() =>
        {
            Assert.That(item.SellIn, Is.EqualTo(4));
            Assert.That(item.Quality, Is.EqualTo(11));
        });
    }

    // 6. Aged Brie on sell date increases quality by 2 (one normal + one post-expiration)
    [Test]
    public void AgedBrie_OnSellDate_IncreasesBy2()
    {
        var item = UpdateSingle(new Item { Name = "Aged Brie", SellIn = 0, Quality = 10 });
        Assert.Multiple(() =>
        {
            Assert.That(item.SellIn, Is.EqualTo(-1));
            Assert.That(item.Quality, Is.EqualTo(12));
        });
    }

    // 7. Aged Brie after sell date near cap (49) increases only to 50
    [Test]
    public void AgedBrie_OnSellDate_At49_CapsAt50()
    {
        var item = UpdateSingle(new Item { Name = "Aged Brie", SellIn = 0, Quality = 49 });
        Assert.Multiple(() =>
        {
            Assert.That(item.SellIn, Is.EqualTo(-1));
            Assert.That(item.Quality, Is.EqualTo(50));
        });
    }

    // 8. Aged Brie at max quality stays at 50
    [Test]
    public void AgedBrie_AtMaxQuality_DoesNotIncrease()
    {
        var item = UpdateSingle(new Item { Name = "Aged Brie", SellIn = 8, Quality = 50 });
        Assert.Multiple(() =>
        {
            Assert.That(item.SellIn, Is.EqualTo(7));
            Assert.That(item.Quality, Is.EqualTo(50));
        });
    }

    // 9. Backstage passes with SellIn > 10 increases quality by 1
    [Test]
    public void BackstagePasses_MoreThan10Days_IncreaseBy1()
    {
        var item = UpdateSingle(new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 });
        Assert.Multiple(() =>
        {
            Assert.That(item.SellIn, Is.EqualTo(14));
            Assert.That(item.Quality, Is.EqualTo(21));
        });
    }

    // 10. Backstage passes with 10 days left increases by 2
    [Test]
    public void BackstagePasses_TenDaysLeft_IncreaseBy2()
    {
        var item = UpdateSingle(new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 30 });
        Assert.Multiple(() =>
        {
            Assert.That(item.SellIn, Is.EqualTo(9));
            Assert.That(item.Quality, Is.EqualTo(32));
        });
    }

    // 11. Backstage passes with 5 days left increases by 3
    [Test]
    public void BackstagePasses_FiveDaysLeft_IncreaseBy3()
    {
        var item = UpdateSingle(new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 25 });
        Assert.Multiple(() =>
        {
            Assert.That(item.SellIn, Is.EqualTo(4));
            Assert.That(item.Quality, Is.EqualTo(28));
        });
    }

    // 12. Backstage passes quality capped at 50 when near limit (SellIn < 6 path)
    [Test]
    public void BackstagePasses_FiveDaysLeft_NearCap_CapsAt50()
    {
        var item = UpdateSingle(new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 49 });
        Assert.Multiple(() =>
        {
            Assert.That(item.SellIn, Is.EqualTo(4));
            Assert.That(item.Quality, Is.EqualTo(50));
        });
    }

    // 13. Backstage passes at max quality does not increase further
    [Test]
    public void BackstagePasses_AtMaxQuality_NoIncrease()
    {
        var item = UpdateSingle(new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 12, Quality = 50 });
        Assert.Multiple(() =>
        {
            Assert.That(item.SellIn, Is.EqualTo(11));
            Assert.That(item.Quality, Is.EqualTo(50));
        });
    }

    // 14. Backstage passes on sell date drop to zero
    [Test]
    public void BackstagePasses_OnSellDate_DropsToZero()
    {
        var item = UpdateSingle(new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 40 });
        Assert.Multiple(() =>
        {
            Assert.That(item.SellIn, Is.EqualTo(-1));
            Assert.That(item.Quality, Is.EqualTo(0));
        });
    }

    // 15. Sulfuras never decreases quality or sellIn (positive sellIn)
    [Test]
    public void Sulfuras_PositiveSellIn_NoChange()
    {
        var item = UpdateSingle(new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 5, Quality = 80 });
        Assert.Multiple(() =>
        {
            Assert.That(item.SellIn, Is.EqualTo(5));
            Assert.That(item.Quality, Is.EqualTo(80));
        });
    }

    // 16. Sulfuras negative sellIn still unchanged and inner post-expiration branch does not reduce quality
    [Test]
    public void Sulfuras_NegativeSellIn_NoChange()
    {
        var item = UpdateSingle(new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80 });
        Assert.Multiple(() =>
        {
            Assert.That(item.SellIn, Is.EqualTo(-1));
            Assert.That(item.Quality, Is.EqualTo(80));
        });
    }

    // 17. Normal item starting already past sell date (extra coverage distinct from on-sell-date path)
    [Test]
    public void NormalItem_AlreadyPastSellDate_QualityDecreasesBy2()
    {
        var item = UpdateSingle(new Item { Name = "foo", SellIn = -1, Quality = 4 });
        Assert.Multiple(() =>
        {
            Assert.That(item.SellIn, Is.EqualTo(-2));
            Assert.That(item.Quality, Is.EqualTo(2));
        });
    }
}