using Microsoft.EntityFrameworkCore.Migrations;

namespace E_learning.Migrations.E_learning
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizScore_AspNetUsers_UserId",
                table: "QuizScore");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizScore",
                table: "QuizScore");

            migrationBuilder.DropIndex(
                name: "IX_QuizScore_UserId",
                table: "QuizScore");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "QuizScore");

            migrationBuilder.RenameTable(
                name: "QuizScore",
                newName: "QuizScores");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "QuizScores",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizScores",
                table: "QuizScores",
                columns: new[] { "UserId", "QuizId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizScores",
                table: "QuizScores");

            migrationBuilder.RenameTable(
                name: "QuizScores",
                newName: "QuizScore");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "QuizScore",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "QuizScore",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizScore",
                table: "QuizScore",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_QuizScore_UserId",
                table: "QuizScore",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizScore_AspNetUsers_UserId",
                table: "QuizScore",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
