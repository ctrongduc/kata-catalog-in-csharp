using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRoseKata
{
    public sealed class AgedBrie : ItemProxy
    {
        public AgedBrie(Item item) : base(item)
        {
            if (item.Name != "Aged Brie")
            {
                throw new ArgumentException("Item is not Aged Brie", nameof(item));
            }
        }

        public override void Update()
        {
            DecreaseSellIn();
            IncrementQuality();
            if (IsExpired)
            {
                IncrementQuality();
            }
        }
    }
}
