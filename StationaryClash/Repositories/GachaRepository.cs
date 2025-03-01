using StationaryClash.Data;
using StationaryClash.Interfaces.Repositories;
using StationaryClash.Models;
using Microsoft.EntityFrameworkCore;

namespace StationaryClash.Repositories
{
    public class GachaRepository : IGachaRepository
    {
        private readonly StationaryClashContext _context;
        // Constructor
        public GachaRepository(StationaryClashContext context) => _context = context;
        public async Task<Character> GetCharacterFromGacha(int rarity)
        {
            Character? character;
            character = await _context.Character
                .Where(c => c.Rarity == rarity)
                .OrderBy(c => Guid.NewGuid())
                .FirstOrDefaultAsync();

            if (character is null)
            {
                return new Character();
            }

            return character;
        }

        public async Task<List<CharacterGacha>?> LoadPullableCharacters()
        {
            List<CharacterGacha>? characterList;
            characterList = await _context.Character
                .OrderByDescending(c => c.Rarity)
                .ThenBy(c => c.Name)
                .Select(c => new CharacterGacha
                {
                    Name = c.Name,
                    Rarity = c.Rarity,
                    Image = c.Image
                })
                .ToListAsync();

            return characterList;
        }
    }
}
