﻿using Cinephila.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cinephila.DataAccess
{
    public class CinephilaDbContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }

        public DbSet<Production> Productions { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<TVShow> TVShows { get; set; }

        public DbSet<Participant> Participants { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<CountryProduction> CountriesProductions { get; set; }

        public DbSet<ParticipantProduction> ParticipantsProductions { get; set; }

        public DbSet<ReviewProduction> ReviewsProductions { get; set; }

        public CinephilaDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ReviewProduction>(builder =>
            {
                builder.HasNoKey();
            });

            modelBuilder.Entity<ParticipantProduction>()
                .HasKey(x => new { x.ProductionID, x.ParticipantID, x.RoleID });

            modelBuilder.Entity<CountryProduction>()
                .HasKey(x => new { x.CountryID, x.ProductionID });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}