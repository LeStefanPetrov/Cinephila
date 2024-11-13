using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinephila.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ParticipantsProductionsFixes5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParticipantsImages");

            migrationBuilder.AddColumn<string>(
                name: "Character",
                table: "ParticipantsProductions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParticipantID",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Images_ParticipantID",
                table: "Images",
                column: "ParticipantID");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Participants_ParticipantID",
                table: "Images",
                column: "ParticipantID",
                principalTable: "Participants",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Participants_ParticipantID",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_ParticipantID",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Character",
                table: "ParticipantsProductions");

            migrationBuilder.DropColumn(
                name: "ParticipantID",
                table: "Images");

            migrationBuilder.CreateTable(
                name: "ParticipantsImages",
                columns: table => new
                {
                    ParticipantID = table.Column<int>(type: "int", nullable: false),
                    ImageID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantsImages", x => new { x.ParticipantID, x.ImageID });
                    table.ForeignKey(
                        name: "FK_ParticipantsImages_Images_ImageID",
                        column: x => x.ImageID,
                        principalTable: "Images",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParticipantsImages_Participants_ParticipantID",
                        column: x => x.ParticipantID,
                        principalTable: "Participants",
                        principalColumn: "TmdbId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantsImages_ImageID",
                table: "ParticipantsImages",
                column: "ImageID");
        }
    }
}
