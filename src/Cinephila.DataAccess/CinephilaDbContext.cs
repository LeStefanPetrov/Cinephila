using Cinephila.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cinephila.DataAccess
{
    public class CinephilaDbContext : DbContext
    {
        public DbSet<CountryEntity> Countries { get; set; }

        public DbSet<ProductionEntity> Productions { get; set; }

        public DbSet<MovieEntity> Movies { get; set; }

        public DbSet<TVShowEntity> TVShows { get; set; }

        public DbSet<ParticipantEntity> Participants { get; set; }

        public DbSet<RoleEntity> Roles { get; set; }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<CountryProductionEntity> CountriesProductions { get; set; }

        public DbSet<ParticipantProductionEntity> ParticipantsProductions { get; set; }

        public DbSet<ReviewProductionEntity> ReviewsProductions { get; set; }

        public CinephilaDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ReviewProductionEntity>(builder =>
            {
                builder.HasNoKey();
            });

            modelBuilder.Entity<ParticipantProductionEntity>()
                .HasKey(x => new { x.ProductionID, x.ParticipantID });

            modelBuilder.Entity<CountryProductionEntity>()
                .HasKey(x => new { x.CountryID, x.ProductionID });

            modelBuilder.Entity<MovieEntity>()
                .HasKey(x => x.ProductionID);

            modelBuilder.Entity<TVShowEntity>()
                .HasKey(x => x.ProductionID);

            modelBuilder.Entity<ReviewProductionEntity>()
                .HasKey(x => x.ID);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
