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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CityLanguage>()
                .HasKey(cl => new
                {
                    cl.CityId,
                    cl.LanguageId
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
