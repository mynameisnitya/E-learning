using Microsoft.EntityFrameworkCore.Migrations;

namespace E_learning.Migrations
{
    public partial class oijdoia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProgrammingLanguage",
                table: "Tutorials",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProgrammingLanguage",
                table: "Tutorials");
        }
    }
}
