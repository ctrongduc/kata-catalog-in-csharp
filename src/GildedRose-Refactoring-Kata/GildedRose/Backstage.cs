using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRoseKata
{
    public class Backstage : ItemProxy
    {
        public const string BackstageName = "Backstage passes to a TAFKAL80ETC concert";
        public Backstage(Item item) : base(item)
        {
            if (item.Name != BackstageName)
            {
                throw new ArgumentException($"Item is not {BackstageName}", nameof(item));
            }
        }
        public override void Update()
        {
            DecreaseSellIn();
            if (SellIn < 0)
            {
                Item.Quality = 0;
                return;
            }
            IncrementQuality();
            if (SellIn < 10)
            {
                IncrementQuality();
            }
            if (SellIn < 6)
            {
                IncrementQuality();
            }
        }
    }
}
