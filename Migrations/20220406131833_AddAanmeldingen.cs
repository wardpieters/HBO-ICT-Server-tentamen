using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamenGamen.Migrations
{
    public partial class AddAanmeldingen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aanmelding",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Aanmelddatum = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeelnemerId = table.Column<int>(type: "INTEGER", nullable: false),
                    GamevoorstelId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aanmelding", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aanmelding_Deelnemer_DeelnemerId",
                        column: x => x.DeelnemerId,
                        principalTable: "Deelnemer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Aanmelding_Gamevoorstel_GamevoorstelId",
                        column: x => x.GamevoorstelId,
                        principalTable: "Gamevoorstel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aanmelding_DeelnemerId",
                table: "Aanmelding",
                column: "DeelnemerId");

            migrationBuilder.CreateIndex(
                name: "IX_Aanmelding_GamevoorstelId",
                table: "Aanmelding",
                column: "GamevoorstelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aanmelding");
        }
    }
}
