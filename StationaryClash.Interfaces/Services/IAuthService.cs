using StationaryClash.Models;

namespace StationaryClash.Interfaces.Services
{
    public interface IAuthService
    {
        Task<User?> LoginAsync(string username, string password);
        Task<RegisterResponse> RegisterAsync(string username, string password);
    }
}
