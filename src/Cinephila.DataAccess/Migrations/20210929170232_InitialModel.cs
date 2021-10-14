using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cinephila.DataAccess.Migrations
{
    public partial class InitialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeathDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlaceOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Productions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearOfCreation = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CountriesProductions",
                columns: table => new
                {
                    ProductionID = table.Column<int>(type: "int", nullable: false),
                    CountryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_CountriesProductions_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountriesProductions_Productions_ProductionID",
                        column: x => x.ProductionID,
                        principalTable: "Productions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Movies_Productions_ProductionID",
                        column: x => x.ProductionID,
                        principalTable: "Productions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shows",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductionID = table.Column<int>(type: "int", nullable: false),
                    EndOfProduction = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shows", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Shows_Productions_ProductionID",
                        column: x => x.ProductionID,
                        principalTable: "Productions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParticipantsProductions",
                columns: table => new
                {
                    ProductionID = table.Column<int>(type: "int", nullable: false),
                    ParticipantID = table.Column<int>(type: "int", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_ParticipantsProductions_Participants_ParticipantID",
                        column: x => x.ParticipantID,
                        principalTable: "Participants",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParticipantsProductions_Productions_ProductionID",
                        column: x => x.ProductionID,
                        principalTable: "Productions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParticipantsProductions_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReviewsProductions",
                columns: table => new
                {
                    ProductionID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Review = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_ReviewsProductions_Productions_ProductionID",
                        column: x => x.ProductionID,
                        principalTable: "Productions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReviewsProductions_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CountriesProductions_CountryID",
                table: "CountriesProductions",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_CountriesProductions_ProductionID",
                table: "CountriesProductions",
                column: "ProductionID");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_ProductionID",
                table: "Movies",
                column: "ProductionID");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantsProductions_ParticipantID",
                table: "ParticipantsProductions",
                column: "ParticipantID");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantsProductions_ProductionID",
                table: "ParticipantsProductions",
                column: "ProductionID");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantsProductions_RoleID",
                table: "ParticipantsProductions",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewsProductions_ProductionID",
                table: "ReviewsProductions",
                column: "ProductionID");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewsProductions_UserID",
                table: "ReviewsProductions",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Shows_ProductionID",
                table: "Shows",
                column: "ProductionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CountriesProductions");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "ParticipantsProductions");

            migrationBuilder.DropTable(
                name: "ReviewsProductions");

            migrationBuilder.DropTable(
                name: "Shows");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Productions");
        }
    }
}
