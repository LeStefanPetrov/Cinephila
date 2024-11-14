﻿// <auto-generated />
using System;
using Cinephila.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cinephila.DataAccess.Migrations
{
    [DbContext(typeof(CinephilaDbContext))]
    [Migration("20241113183053_ParticipantsProductionsFixes3")]
    partial class ParticipantsProductionsFixes3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Cinephila.DataAccess.Entities.CountryEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

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

            modelBuilder.Entity("Cinephila.DataAccess.Entities.GenreEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TmdbId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("TmdbId")
                        .IsUnique()
                        .HasDatabaseName("IX_Genres_TmdbId");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.GenreProductionEntity", b =>
                {
                    b.Property<int>("ProductionID")
                        .HasColumnType("int");

                    b.Property<int>("GenreID")
                        .HasColumnType("int");

                    b.HasKey("ProductionID", "GenreID");

                    b.HasIndex("GenreID");

                    b.ToTable("GenresProductions");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.ImageEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("VoteAverage")
                        .HasColumnType("float");

                    b.Property<int>("VoteCount")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.MovieEntity", b =>
                {
                    b.Property<int>("ProductionID")
                        .HasColumnType("int");

                    b.Property<int>("Runtime")
                        .HasColumnType("int");

                    b.HasKey("ProductionID");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.ParticipantEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Biography")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeathDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("KnownForDepartment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlaceOfBirth")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Popularity")
                        .HasColumnType("float");

                    b.Property<int>("TmdbId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.ParticipantImageEntity", b =>
                {
                    b.Property<int>("ParticipantID")
                        .HasColumnType("int");

                    b.Property<int>("ImageID")
                        .HasColumnType("int");

                    b.HasKey("ParticipantID", "ImageID");

                    b.HasIndex("ImageID");

                    b.ToTable("ParticipantsImages");
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

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("Budget")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OriginalTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Popularity")
                        .HasColumnType("float");

                    b.Property<string>("PosterPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("Revenue")
                        .HasColumnType("bigint");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TmdbID")
                        .HasColumnType("int");

                    b.Property<double>("VoteAverage")
                        .HasColumnType("float");

                    b.Property<int>("VoteCount")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("TmdbID")
                        .IsUnique()
                        .HasDatabaseName("IX_Production_TmdbId");

                    b.ToTable("Productions");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.ReviewProductionEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

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

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

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

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

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

            modelBuilder.Entity("Cinephila.DataAccess.Entities.GenreProductionEntity", b =>
                {
                    b.HasOne("Cinephila.DataAccess.Entities.GenreEntity", "Genre")
                        .WithMany("GenreProductions")
                        .HasForeignKey("GenreID")
                        .HasPrincipalKey("TmdbId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cinephila.DataAccess.Entities.ProductionEntity", "Production")
                        .WithMany("GenresProductions")
                        .HasForeignKey("ProductionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

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

            modelBuilder.Entity("Cinephila.DataAccess.Entities.ParticipantImageEntity", b =>
                {
                    b.HasOne("Cinephila.DataAccess.Entities.ImageEntity", "Image")
                        .WithMany()
                        .HasForeignKey("ImageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cinephila.DataAccess.Entities.ParticipantEntity", "Participant")
                        .WithMany("ParticipantImages")
                        .HasForeignKey("ParticipantID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Image");

                    b.Navigation("Participant");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.ParticipantProductionEntity", b =>
                {
                    b.HasOne("Cinephila.DataAccess.Entities.ParticipantEntity", "Participant")
                        .WithMany("ParticipantsProductions")
                        .HasForeignKey("ParticipantID")
                        .HasPrincipalKey("TmdbId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cinephila.DataAccess.Entities.ProductionEntity", "Production")
                        .WithMany("ParticipantsProductions")
                        .HasForeignKey("ProductionID")
                        .HasPrincipalKey("TmdbID")
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

            modelBuilder.Entity("Cinephila.DataAccess.Entities.GenreEntity", b =>
                {
                    b.Navigation("GenreProductions");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.ParticipantEntity", b =>
                {
                    b.Navigation("ParticipantImages");

                    b.Navigation("ParticipantsProductions");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.ProductionEntity", b =>
                {
                    b.Navigation("Countries");

                    b.Navigation("GenresProductions");

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