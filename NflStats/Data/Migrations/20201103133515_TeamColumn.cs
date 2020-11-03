using Microsoft.EntityFrameworkCore.Migrations;

namespace NflStats.Migrations
{
    public partial class TeamColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Team",
                table: "Rosters",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Team",
                table: "Rosters");
        }
    }
}
