namespace StationaryClash.Models
{
    public class CollectionItem
    {
        public int UserID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Rarity { get; set; }
        public int Duplicates { get; set; }
        public string Image { get; set; } = string.Empty;
    }
}
