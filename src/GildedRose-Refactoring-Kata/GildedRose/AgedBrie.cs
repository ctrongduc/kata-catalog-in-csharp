using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRoseKata
{
    public sealed class AgedBrie : ItemProxy
    {
        public const string AgedBrieName = "Aged Brie";
        public AgedBrie(Item item) : base(item)
        {
            if (item.Name != AgedBrieName)
            {
                throw new ArgumentException($"Item is not {AgedBrieName}", nameof(item));
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
