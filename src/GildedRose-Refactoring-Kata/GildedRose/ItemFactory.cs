using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRoseKata
{
    internal static class ItemFactory
    {
        internal static ItemProxy Create(Item item)
        {
            return item.Name switch
            {
                AgedBrie.AgedBrieName => new AgedBrie(item),
                Sulfuras.SulfurasName => new Sulfuras(item),
                Backstage.BackstageName => new Backstage(item),
                _ => new Normal(item),
            };
        }
    }
}
