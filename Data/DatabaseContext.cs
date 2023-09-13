using AplikacjaDoNaukiJęzyków.Models;
using Microsoft.EntityFrameworkCore;

namespace AplikacjaDoNaukiJęzyków.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Slowo> Slowa { get; set; }
    }
}
