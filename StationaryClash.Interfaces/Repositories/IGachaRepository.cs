using StationaryClash.Models;

namespace StationaryClash.Interfaces.Repositories
{
    public interface IGachaRepository
    {
        Task<Character> GetCharacterFromGacha(int rarity);
        Task<List<CharacterGacha>?> LoadPullableCharacters();
    }
}
