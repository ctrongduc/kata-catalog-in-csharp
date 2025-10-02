using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRoseKata
{
    internal class Normal : ItemProxy
    {
        public Normal(Item item) : base(item)
        {
            if (item.Name == AgedBrie.AgedBrieName 
                || item.Name == Sulfuras.SulfurasName 
                || item.Name == Backstage.BackstageName)
            {
                throw new ArgumentException("Item is not a normal item", nameof(item));
            }
        }
        public override void Update()
        {
            DecreaseSellIn();

            DecrementQuality();

            if (IsExpired)
            {
                DecrementQuality();
            }
        }
    }
}
