using Microsoft.EntityFrameworkCore.Migrations;

namespace E_learning.Migrations.E_learning
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizScores",
                table: "QuizScores");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "QuizScores",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "QuizScores",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizScores",
                table: "QuizScores",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_QuizScores_UserId",
                table: "QuizScores",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizScores_AspNetUsers_UserId",
                table: "QuizScores",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizScores_AspNetUsers_UserId",
                table: "QuizScores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizScores",
                table: "QuizScores");

            migrationBuilder.DropIndex(
                name: "IX_QuizScores_UserId",
                table: "QuizScores");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "QuizScores");

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
    }
}
