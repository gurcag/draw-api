using Draw.API.Entites;
using Microsoft.EntityFrameworkCore;

namespace Draw.API.DbContexts
{
    public class DrawDbContext : DbContext
    {
        public DbSet<CountryEntity> Countries { get; set; }
        public DbSet<TeamEntity> Teams { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<DrawOptionsEntity> DrawOptions { get; set; }
        public DbSet<DrawEntity> Draws { get; set; }

        public DrawDbContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DrawOptionsEntity>().HasData(
                new DrawOptionsEntity { Id = 1, NumberOfGroups = 4 },
                new DrawOptionsEntity { Id = 2, NumberOfGroups = 8 }
                );

            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity { Id = 1, Name = "Gurcag Yaman", Username = "gurcag", Password = "yaman" }
                );

            modelBuilder.Entity<CountryEntity>().HasData(
                new CountryEntity { Id = 1, Name = "Türkiye" },
                new CountryEntity { Id = 2, Name = "Almanya" },
                new CountryEntity { Id = 3, Name = "Fransa" },
                new CountryEntity { Id = 4, Name = "Hollanda" },
                new CountryEntity { Id = 5, Name = "Portekiz" },
                new CountryEntity { Id = 6, Name = "İtalya" },
                new CountryEntity { Id = 7, Name = "İspanya" },
                new CountryEntity { Id = 8, Name = "Belçika" }
                );

            modelBuilder.Entity<TeamEntity>().HasData(
                    new TeamEntity { Id = 1, CountryId = 1, Name = "Adesso İstanbul" },
                    new TeamEntity { Id = 2, CountryId = 1, Name = "Adesso Ankara" },
                    new TeamEntity { Id = 3, CountryId = 1, Name = "Adesso İzmir" },
                    new TeamEntity { Id = 4, CountryId = 1, Name = "Adesso Antalya" },
                    new TeamEntity { Id = 5, CountryId = 2, Name = "Adesso Berlin" },
                    new TeamEntity { Id = 6, CountryId = 2, Name = "Adesso Frankfurt" },
                    new TeamEntity { Id = 7, CountryId = 2, Name = "Adesso Münih" },
                    new TeamEntity { Id = 8, CountryId = 2, Name = "Adesso Dortmund" },
                    new TeamEntity { Id = 9, CountryId = 3, Name = "Adesso Paris" },
                    new TeamEntity { Id = 10, CountryId = 3, Name = "Adesso Marsilya" },
                    new TeamEntity { Id = 11, CountryId = 3, Name = "Adesso Nice" },
                    new TeamEntity { Id = 12, CountryId = 3, Name = "Adesso Lyon" },
                    new TeamEntity { Id = 13, CountryId = 4, Name = "Adesso Amsterdam" },
                    new TeamEntity { Id = 14, CountryId = 4, Name = "Adesso Rotterdam" },
                    new TeamEntity { Id = 15, CountryId = 4, Name = "Adesso Lahey" },
                    new TeamEntity { Id = 16, CountryId = 4, Name = "Adesso Eindhoven" },
                    new TeamEntity { Id = 17, CountryId = 5, Name = "Adesso Lisbon" },
                    new TeamEntity { Id = 18, CountryId = 5, Name = "Adesso Porto" },
                    new TeamEntity { Id = 19, CountryId = 5, Name = "Adesso Braga" },
                    new TeamEntity { Id = 20, CountryId = 5, Name = "Adesso Coimbra" },
                    new TeamEntity { Id = 21, CountryId = 6, Name = "Adesso Roma" },
                    new TeamEntity { Id = 22, CountryId = 6, Name = "Adesso Milano" },
                    new TeamEntity { Id = 23, CountryId = 6, Name = "Adesso Venedik" },
                    new TeamEntity { Id = 24, CountryId = 6, Name = "Adesso Napoli" },
                    new TeamEntity { Id = 25, CountryId = 7, Name = "Adesso Sevilla" },
                    new TeamEntity { Id = 26, CountryId = 7, Name = "Adesso Madrid" },
                    new TeamEntity { Id = 27, CountryId = 7, Name = "Adesso Barselona" },
                    new TeamEntity { Id = 28, CountryId = 7, Name = "Adesso Granada" },
                    new TeamEntity { Id = 29, CountryId = 8, Name = "Adesso Brüksel" },
                    new TeamEntity { Id = 30, CountryId = 8, Name = "Adesso Brugge" },
                    new TeamEntity { Id = 31, CountryId = 8, Name = "Adesso Gent" },
                    new TeamEntity { Id = 32, CountryId = 8, Name = "Adesso Anvers" }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
