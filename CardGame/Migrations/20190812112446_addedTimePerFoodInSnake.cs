using Microsoft.EntityFrameworkCore.Migrations;

namespace CardGame.Migrations
{
    public partial class addedTimePerFoodInSnake : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimePerFood",
                table: "UserScores",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimePerFood",
                table: "UserScores");
        }
    }
}
