using Microsoft.EntityFrameworkCore.Migrations;

namespace E_learning.Migrations.E_learning
{
    public partial class User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizScores_AspNetUsers_UserId",
                table: "QuizScores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizScores",
                table: "QuizScores");

            migrationBuilder.RenameTable(
                name: "QuizScores",
                newName: "QuizScore");

            migrationBuilder.RenameIndex(
                name: "IX_QuizScores_UserId",
                table: "QuizScore",
                newName: "IX_QuizScore_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizScore",
                table: "QuizScore",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizScore_AspNetUsers_UserId",
                table: "QuizScore",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizScore_AspNetUsers_UserId",
                table: "QuizScore");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizScore",
                table: "QuizScore");

            migrationBuilder.RenameTable(
                name: "QuizScore",
                newName: "QuizScores");

            migrationBuilder.RenameIndex(
                name: "IX_QuizScore_UserId",
                table: "QuizScores",
                newName: "IX_QuizScores_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizScores",
                table: "QuizScores",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizScores_AspNetUsers_UserId",
                table: "QuizScores",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
