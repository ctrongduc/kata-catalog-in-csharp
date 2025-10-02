namespace GildedRoseKata
{
    public abstract class ItemProxy
    {
        protected Item Item { get; }

        protected ItemProxy(Item item)
        {
            Item = item;
        }

        public abstract void Update();

        public void DecreaseSellIn()
        {
            Item.SellIn--;
        }

        public void DecrementQuality()
        {
            if (Item.Quality > 0)
            {
                Item.Quality--;
            }
        }

        public void IncrementQuality()
        {
            if (Item.Quality < 50)
            {
                Item.Quality++;
            }
        }

        public int SellIn => Item.SellIn;
        public int Quality => Item.Quality;

        public string Name => Item.Name;

        public bool IsExpired => Item.SellIn < 0;
    }
}