using StationaryClash.Models;

namespace StationaryClash.Interfaces.Repositories
{
    public interface IInventoryRepository
    {
        Task AddCharacterToInventory(int userID, int charID);
        Task<List<CollectionItem>?> LoadInventory(int userID);
    }
}
