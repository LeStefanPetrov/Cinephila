﻿// <auto-generated />
using System;
using Cinephila.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cinephila.DataAccess.Migrations
{
    [DbContext(typeof(CinephilaDbContext))]
    partial class CinephilaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Cinephila.DataAccess.Entities.CountryEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.CountryProductionEntity", b =>
                {
                    b.Property<int>("CountryID")
                        .HasColumnType("int");

                    b.Property<int>("ProductionID")
                        .HasColumnType("int");

                    b.HasKey("CountryID", "ProductionID");

                    b.HasIndex("ProductionID");

                    b.ToTable("CountriesProductions");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.MovieEntity", b =>
                {
                    b.Property<int>("ProductionID")
                        .HasColumnType("int");

                    b.Property<int>("LengthInMinutes")
                        .HasColumnType("int");

                    b.HasKey("ProductionID");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.ParticipantEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeathDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlaceOfBirth")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.ParticipantProductionEntity", b =>
                {
                    b.Property<int>("ProductionID")
                        .HasColumnType("int");

                    b.Property<int>("ParticipantID")
                        .HasColumnType("int");

                    b.Property<int?>("RoleID")
                        .HasColumnType("int");

                    b.HasKey("ProductionID", "ParticipantID");

                    b.HasIndex("ParticipantID");

                    b.HasIndex("RoleID");

                    b.ToTable("ParticipantsProductions");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.ProductionEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("ApiID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PosterPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("YearOfCreation")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Productions");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.ReviewProductionEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("ProductionID")
                        .HasColumnType("int");

                    b.Property<int?>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Review")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ProductionID");

                    b.HasIndex("UserID");

                    b.ToTable("ReviewsProductions");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.RoleEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.TVShowEntity", b =>
                {
                    b.Property<int>("ProductionID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndOfProduction")
                        .HasColumnType("datetime2");

                    b.HasKey("ProductionID");

                    b.ToTable("TVShows");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.UserEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.CountryProductionEntity", b =>
                {
                    b.HasOne("Cinephila.DataAccess.Entities.CountryEntity", "Country")
                        .WithMany("Productions")
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cinephila.DataAccess.Entities.ProductionEntity", "Production")
                        .WithMany("Countries")
                        .HasForeignKey("ProductionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("Production");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.MovieEntity", b =>
                {
                    b.HasOne("Cinephila.DataAccess.Entities.ProductionEntity", "Production")
                        .WithOne("Movie")
                        .HasForeignKey("Cinephila.DataAccess.Entities.MovieEntity", "ProductionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Production");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.ParticipantProductionEntity", b =>
                {
                    b.HasOne("Cinephila.DataAccess.Entities.ParticipantEntity", "Participant")
                        .WithMany("ParticipantsProductions")
                        .HasForeignKey("ParticipantID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cinephila.DataAccess.Entities.ProductionEntity", "Production")
                        .WithMany("ParticipantsProductions")
                        .HasForeignKey("ProductionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cinephila.DataAccess.Entities.RoleEntity", "Role")
                        .WithMany("ParticipantsProductions")
                        .HasForeignKey("RoleID");

                    b.Navigation("Participant");

                    b.Navigation("Production");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.ReviewProductionEntity", b =>
                {
                    b.HasOne("Cinephila.DataAccess.Entities.ProductionEntity", "Production")
                        .WithMany()
                        .HasForeignKey("ProductionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cinephila.DataAccess.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Production");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.TVShowEntity", b =>
                {
                    b.HasOne("Cinephila.DataAccess.Entities.ProductionEntity", "Production")
                        .WithOne("TVShow")
                        .HasForeignKey("Cinephila.DataAccess.Entities.TVShowEntity", "ProductionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Production");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.CountryEntity", b =>
                {
                    b.Navigation("Productions");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.ParticipantEntity", b =>
                {
                    b.Navigation("ParticipantsProductions");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.ProductionEntity", b =>
                {
                    b.Navigation("Countries");

                    b.Navigation("Movie");

                    b.Navigation("ParticipantsProductions");

                    b.Navigation("TVShow");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.RoleEntity", b =>
                {
                    b.Navigation("ParticipantsProductions");
                });
#pragma warning restore 612, 618
        }
    }
}
