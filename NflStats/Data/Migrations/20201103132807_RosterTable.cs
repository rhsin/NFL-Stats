using Microsoft.EntityFrameworkCore.Migrations;

namespace NflStats.Migrations
{
    public partial class RosterTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RosterId",
                table: "Players",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Rosters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rosters", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_RosterId",
                table: "Players",
                column: "RosterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Rosters_RosterId",
                table: "Players",
                column: "RosterId",
                principalTable: "Rosters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Rosters_RosterId",
                table: "Players");

            migrationBuilder.DropTable(
                name: "Rosters");

            migrationBuilder.DropIndex(
                name: "IX_Players_RosterId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "RosterId",
                table: "Players");
        }
    }
}
