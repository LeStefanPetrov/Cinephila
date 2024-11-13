using Cinephila.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cinephila.DataAccess
{
    public class CinephilaDbContext : DbContext
    {
        public DbSet<CountryEntity> Countries { get; set; }

        public DbSet<GenreEntity> Genres { get; set; }

        public DbSet<ProductionEntity> Productions { get; set; }

        public DbSet<MovieEntity> Movies { get; set; }

        public DbSet<TVShowEntity> TVShows { get; set; }

        public DbSet<ParticipantEntity> Participants { get; set; }

        public DbSet<RoleEntity> Roles { get; set; }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<ImageEntity> Images { get; set; }

        public DbSet<CountryProductionEntity> CountriesProductions { get; set; }

        public DbSet<ParticipantProductionEntity> ParticipantsProductions { get; set; }

        public DbSet<ReviewProductionEntity> ReviewsProductions { get; set; }

        public DbSet<GenreProductionEntity> GenresProductions { get; set; }

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

            modelBuilder.Entity<GenreProductionEntity>()
                .HasKey(x => new { x.ProductionID, x.GenreID });

            modelBuilder.Entity<MovieEntity>()
                .HasKey(x => x.ProductionID);

            modelBuilder.Entity<TVShowEntity>()
                .HasKey(x => x.ProductionID);

            modelBuilder.Entity<ReviewProductionEntity>()
                .HasKey(x => x.ID);

            // Configure relationship between Genre and GenreProduction
            modelBuilder.Entity<GenreProductionEntity>()
                .HasOne(gp => gp.Genre)
                .WithMany(g => g.GenreProductions)
                .HasForeignKey(gp => gp.GenreID)
                .HasPrincipalKey(g => g.TmdbId); // Maps to TmdbId, not PK Id of Genre

            // Configure relationship between Production and ParticipantProductionEntity
            modelBuilder.Entity<ParticipantProductionEntity>()
                .HasOne(pp => pp.Production)
                .WithMany(p => p.ParticipantsProductions)
                .HasForeignKey(pp => pp.ProductionID)
                .HasPrincipalKey(p => p.TmdbID); // Maps to TmdbId, not PK Id of Production

            // Configure relationship between Participant and ParticipantProductionEntity
            modelBuilder.Entity<ParticipantProductionEntity>()
                .HasOne(pp => pp.Participant)
                .WithMany(p => p.ParticipantsProductions)
                .HasForeignKey(pp => pp.ParticipantID)
                .HasPrincipalKey(p => p.TmdbId); // Maps to TmdbId, not PK Id of Participant

            modelBuilder.Entity<ImageEntity>()
                   .HasOne(i => i.Participant)
                   .WithMany(p => p.ParticipantImages) 
                   .HasForeignKey(i => i.ParticipantID)
                   .OnDelete(DeleteBehavior.Cascade);

            //Indexes
            modelBuilder.Entity<GenreEntity>()
                .HasIndex(x => x.TmdbId)
                .HasDatabaseName("IX_Genres_TmdbId")
                .IsUnique();

            modelBuilder.Entity<ProductionEntity>()
                .HasIndex(x => x.TmdbID)
                .HasDatabaseName("IX_Production_TmdbId")
                .IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
