using Microsoft.EntityFrameworkCore.Migrations;

namespace CardGame.Migrations
{
    public partial class addedwinsandlosses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Losses",
                table: "UserScores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Wins",
                table: "UserScores",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Losses",
                table: "UserScores");

            migrationBuilder.DropColumn(
                name: "Wins",
                table: "UserScores");
        }
    }
}
