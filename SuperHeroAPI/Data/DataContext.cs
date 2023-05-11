using Microsoft.EntityFrameworkCore;

namespace SuperHeroAPI.Data
{
    // i wrote in Package Manager Console next text " dotnet tool install --global dotnet-ef " for idk, if i want check if it instal need write dotnet ef
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { } //constructor 
        public DbSet<SuperHero> SuperHeroes { get; set; } // it prop - property 
    }
}
