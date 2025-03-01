namespace StationaryClash.Utils
{
    public class RandomRarity
    {
        // Random generation
        private readonly Random _rnd;
        public RandomRarity()
        {
            _rnd = new Random();
        }
        public int GenerateRarity()
        {
            int rarity;
            int[] rarityList = new int[20];

            int ctr = 0;
            for (; ctr < 15; ctr++) rarityList[ctr] = 1;
            for (; ctr < 19; ctr++) rarityList[ctr] = 2;
            for (; ctr < 20; ctr++) rarityList[ctr] = 3;
            int rarityIndex = _rnd.Next(rarityList.Length);
            rarity = rarityList[rarityIndex];

            return rarity;
        }
    }
}
