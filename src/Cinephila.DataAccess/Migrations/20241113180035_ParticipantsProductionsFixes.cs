using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinephila.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ParticipantsProductionsFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantsProductions_Participants_ParticipantID",
                table: "ParticipantsProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantsProductions_Productions_ProductionID",
                table: "ParticipantsProductions");

            migrationBuilder.AddColumn<string>(
                name: "Biography",
                table: "Participants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KnowForDepartment",
                table: "Participants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Popularity",
                table: "Participants",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "TmdbId",
                table: "Participants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Productions_TmdbID",
                table: "Productions",
                column: "TmdbID");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Participants_TmdbId",
                table: "Participants",
                column: "TmdbId");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VoteAverage = table.Column<double>(type: "float", nullable: false),
                    VoteCount = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ParticipantImageEntity",
                columns: table => new
                {
                    ParticipantID = table.Column<int>(type: "int", nullable: false),
                    ImageID = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantImageEntity", x => new { x.ParticipantID, x.ImageID });
                    table.ForeignKey(
                        name: "FK_ParticipantImageEntity_Images_ImageID",
                        column: x => x.ImageID,
                        principalTable: "Images",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParticipantImageEntity_Participants_ParticipantID",
                        column: x => x.ParticipantID,
                        principalTable: "Participants",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantImageEntity_ImageID",
                table: "ParticipantImageEntity",
                column: "ImageID");

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantsProductions_Participants_ParticipantID",
                table: "ParticipantsProductions",
                column: "ParticipantID",
                principalTable: "Participants",
                principalColumn: "TmdbId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantsProductions_Productions_ProductionID",
                table: "ParticipantsProductions",
                column: "ProductionID",
                principalTable: "Productions",
                principalColumn: "TmdbID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantsProductions_Participants_ParticipantID",
                table: "ParticipantsProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantsProductions_Productions_ProductionID",
                table: "ParticipantsProductions");

            migrationBuilder.DropTable(
                name: "ParticipantImageEntity");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Productions_TmdbID",
                table: "Productions");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Participants_TmdbId",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "Biography",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "KnowForDepartment",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "Popularity",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "TmdbId",
                table: "Participants");

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantsProductions_Participants_ParticipantID",
                table: "ParticipantsProductions",
                column: "ParticipantID",
                principalTable: "Participants",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantsProductions_Productions_ProductionID",
                table: "ParticipantsProductions",
                column: "ProductionID",
                principalTable: "Productions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
