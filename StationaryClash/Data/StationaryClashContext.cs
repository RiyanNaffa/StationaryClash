using Microsoft.EntityFrameworkCore;
using StationaryClash.Models;

namespace StationaryClash.Data
{
    public class StationaryClashContext : DbContext
    {
        public StationaryClashContext (DbContextOptions<StationaryClashContext> options)
            : base(options)
        {
        }

        public DbSet<Character> Character { get; set; } = default!;
        public DbSet<User> Account { get; set; } = default!;
        public DbSet<Inventory> Inventory { get; set; } = default!;
    }
}
