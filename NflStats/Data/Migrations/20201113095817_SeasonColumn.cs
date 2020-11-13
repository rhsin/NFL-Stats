using Microsoft.EntityFrameworkCore.Migrations;

namespace NflStats.Migrations
{
    public partial class SeasonColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Season",
                table: "Players",
                nullable: true,
                defaultValue: 2019);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Season",
                table: "Players");
        }
    }
}
