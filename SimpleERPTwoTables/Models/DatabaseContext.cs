using Microsoft.EntityFrameworkCore;

namespace SimpleERPTwoTables.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) 
        {

        }

        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<City> Cities { get; set; } = null!;    
        public DbSet<Language> Languages { get; set; } = null!;    
        public DbSet<CityLanguage> CityLanguages { get; set; } = null!;    
    }
}
