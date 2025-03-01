using StationaryClash.Models;

namespace StationaryClash.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByID(int id);
        Task<User?> GetUserbyUsername(string username);
        Task<int> GetUserTokenAsync(int id);
        Task DecrementUserToken(int id);
        Task UpdateDescription(int id, string description);
    }
}
