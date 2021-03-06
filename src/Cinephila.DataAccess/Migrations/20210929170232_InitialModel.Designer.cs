// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cinephila.DataAccess.Migrations
{
    [DbContext(typeof(CinephilaDbContext))]
    [Migration("20210929170232_InitialModel")]
    partial class InitialModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Cinephila.DataAccess.Entities.Country", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.CountryProduction", b =>
                {
                    b.Property<int>("CountryID")
                        .HasColumnType("int");

                    b.Property<int>("ProductionID")
                        .HasColumnType("int");

                    b.HasIndex("CountryID");

                    b.HasIndex("ProductionID");

                    b.ToTable("CountriesProductions");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.Movie", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProductionID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ProductionID");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.Participant", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("Cinephila.DataAccess.Entities.ParticipantProduction", b =>
                {
                    b.Property<int>("ParticipantID")
                        .HasColumnType("int");

                    b.Property<int>("ProductionID")
                        .HasColumnType("int");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.HasIndex("ParticipantID");

                    b.HasIndex("ProductionID");

                    b.HasIndex("RoleID");

                    b.ToTable("ParticipantsProductions");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.Production", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("YearOfCreation")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Productions");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.ReviewProduction", b =>
                {
                    b.Property<int>("ProductionID")
                        .HasColumnType("int");

                    b.Property<int?>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Review")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasIndex("ProductionID");

                    b.HasIndex("UserID");

                    b.ToTable("ReviewsProductions");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.Role", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.Show", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("EndOfProduction")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductionID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ProductionID");

                    b.ToTable("Shows");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.CountryProduction", b =>
                {
                    b.HasOne("Cinephila.DataAccess.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cinephila.DataAccess.Entities.Production", "Production")
                        .WithMany()
                        .HasForeignKey("ProductionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("Production");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.Movie", b =>
                {
                    b.HasOne("Cinephila.DataAccess.Entities.Production", "Production")
                        .WithMany()
                        .HasForeignKey("ProductionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Production");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.ParticipantProduction", b =>
                {
                    b.HasOne("Cinephila.DataAccess.Entities.Participant", "Participant")
                        .WithMany()
                        .HasForeignKey("ParticipantID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cinephila.DataAccess.Entities.Production", "Production")
                        .WithMany()
                        .HasForeignKey("ProductionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cinephila.DataAccess.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Participant");

                    b.Navigation("Production");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.ReviewProduction", b =>
                {
                    b.HasOne("Cinephila.DataAccess.Entities.Production", "Production")
                        .WithMany()
                        .HasForeignKey("ProductionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cinephila.DataAccess.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Production");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Cinephila.DataAccess.Entities.Show", b =>
                {
                    b.HasOne("Cinephila.DataAccess.Entities.Production", "Production")
                        .WithMany()
                        .HasForeignKey("ProductionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Production");
                });
#pragma warning restore 612, 618
        }
    }
}
