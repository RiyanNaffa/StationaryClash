using StationaryClash.Data;
using StationaryClash.Interfaces.Services;

namespace StationaryClash.Services
{
    public class GachaService : IGachaService
    {
        private readonly StationaryClashContext _context;
        public GachaService(StationaryClashContext context) => _context = context;
        public async Task<bool> CanPullGacha(int id)
        {
            var user = await _context.Account.FindAsync(id);

            if (user is null)
            {
                Console.WriteLine("Pengguna tidak ditemukan.");
                return false;
            }
            if (user.Token <= 0)
            {
                Console.WriteLine("Token tidak cukup untuk melakukan gacha.");
                return false;
            }
            return true;
        }
    }
}
