using Microsoft.EntityFrameworkCore.Migrations;

namespace CardGame.Migrations
{
    public partial class updatedToDecimalTimePerFood : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TimePerFood",
                table: "UserScores",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TimePerFood",
                table: "UserScores",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
