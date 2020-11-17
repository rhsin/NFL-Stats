using Microsoft.EntityFrameworkCore.Migrations;

namespace NflStats.Migrations
{
    public partial class TeamStatModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeamStats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamName = table.Column<string>(nullable: true),
                    Season = table.Column<int>(nullable: true),
                    Points = table.Column<float>(nullable: false),
                    TotalYds = table.Column<float>(nullable: false),
                    Turnovers = table.Column<float>(nullable: false),
                    PassYds = table.Column<float>(nullable: false),
                    PassTds = table.Column<float>(nullable: false),
                    RushYds = table.Column<float>(nullable: false),
                    RushTds = table.Column<float>(nullable: false),
                    YdsPerAtt = table.Column<float>(nullable: false),
                    PenYds = table.Column<float>(nullable: false),
                    TeamId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamStats_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamStats_TeamId",
                table: "TeamStats",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamStats");
        }
    }
}
