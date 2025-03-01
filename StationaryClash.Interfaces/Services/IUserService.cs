using StationaryClash.Models;

namespace StationaryClash.Interfaces.Services
{
    public interface IUserService
    {
        Task<User?> AuthAsync(string username, string password);
        Task<bool> RegisterAsync(string username, string password);
    }
}
