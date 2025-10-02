using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRoseKata
{
    internal class Sulfuras : ItemProxy
    {
        public const string SulfurasName = "Sulfuras, Hand of Ragnaros";
        public Sulfuras(Item item) : base(item)
        {
            if (item.Name != SulfurasName)
            {
                throw new ArgumentException($"Item is not {SulfurasName}", nameof(item));
            }
        }
        public override void Update()
        {
            // Legendary item, do nothing
        }
    }
}
