using Microsoft.EntityFrameworkCore.Migrations;

namespace TicTacToe.Data.Migrations
{
    public partial class Added_Index_In_Scores_By_UserId_Status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Scores_UserId",
                table: "Scores");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_UserId_Status",
                table: "Scores",
                columns: new[] { "UserId", "Status" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Scores_UserId_Status",
                table: "Scores");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_UserId",
                table: "Scores",
                column: "UserId");
        }
    }
}
