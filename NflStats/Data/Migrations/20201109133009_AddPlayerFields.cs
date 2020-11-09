using Microsoft.EntityFrameworkCore.Migrations;

namespace NflStats.Migrations
{
    public partial class AddPlayerFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Fumbles",
                table: "Players",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Games",
                table: "Players",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "PassInt",
                table: "Players",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "PassTds",
                table: "Players",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "PassYds",
                table: "Players",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "RecTds",
                table: "Players",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "RecYds",
                table: "Players",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "RushTds",
                table: "Players",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "RushYds",
                table: "Players",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fumbles",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Games",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "PassInt",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "PassTds",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "PassYds",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "RecTds",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "RecYds",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "RushTds",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "RushYds",
                table: "Players");
        }
    }
}
