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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Slowo>().HasData(
            //     new Slowo 
            //     {
            //         ID = 1,
            //         PoziomSlowa = "podstawowy",
            //         SlowoPol = "Witam",
            //         SlowoAng = "Hello",
            //         SlowoAra = "أهلا",
            //         SlowoUkr = "привіт"
            //     },
            //     new Slowo 
            //     {
            //         ID = 2,
            //         PoziomSlowa = "podstawowy",
            //         SlowoPol = "wiek",
            //         SlowoAng = "age",
            //         SlowoAra = "سن",
            //         SlowoUkr = "вік"
            //     },
            //     new Slowo 
            //     {
            //         ID = 3,
            //         PoziomSlowa = "podstawowy",
            //         SlowoPol = "jeleń",
            //         SlowoAng = "deer",
            //         SlowoAra = "عزيزي",
            //         SlowoUkr = "олень"
            //     },
            //     new Slowo 
            //     {
            //         ID = 4,
            //         PoziomSlowa = "podstawowy",
            //         SlowoPol = "herbata",
            //         SlowoAng = "tea",
            //         SlowoAra = "شاي",
            //         SlowoUkr = "чай"
            //     },
            //     new Slowo 
            //     {
            //         ID = 5,
            //         PoziomSlowa = "podstawowy",
            //         SlowoPol = "park",
            //         SlowoAng = "park",
            //         SlowoAra = "منتزه",
            //         SlowoUkr = "парк"
            //     }
            // );
        }
    }
}
