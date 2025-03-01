using StationaryClash.Data;
using StationaryClash.Interfaces.Repositories;
using StationaryClash.Models;
using Microsoft.EntityFrameworkCore;

namespace StationaryClash.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly StationaryClashContext _context;
        public InventoryRepository(StationaryClashContext context) => _context = context;
        public async Task AddCharacterToInventory(int userID, int charID)
        {
            var existingInventory = await _context.Inventory
                    .FirstOrDefaultAsync(i => i.User_Id == userID && i.Char_Id == charID);

            if (existingInventory is null)
            {
                // Tambahkan karakter baru ke inventaris
                var inventory = new Inventory()
                {
                    User_Id = userID,
                    Char_Id = charID,
                    Duplicates = 0
                };
                _context.Inventory.Add(inventory);
            }
            else
            {
                // Tambahkan duplikasi jika karakter sudah ada
                existingInventory.Duplicates += 1;
            }
            await _context.SaveChangesAsync();
        }
        public async Task<List<CollectionItem>?> LoadInventory(int userID)
        {
            List<CollectionItem>? itemList;
            itemList = await _context.Inventory
                .Where(i => i.User_Id == userID)
                .Join(_context.Character,
                    inventory => inventory.Char_Id,
                    character => character.ID,
                    (inventory, character) => new CollectionItem
                    {
                        Name = character.Name,
                        Rarity = character.Rarity,
                        Duplicates = inventory.Duplicates,
                        Image = character.Image // Asumsikan kolom image menyimpan path atau URL
                    })
                .ToListAsync();
            return itemList;
        }
    }
}
