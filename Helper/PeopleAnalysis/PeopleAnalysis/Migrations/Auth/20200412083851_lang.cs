using Microsoft.EntityFrameworkCore.Migrations;

namespace PeopleAnalysis.Migrations.Auth
{
    public partial class lang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "AspNetUsers",
                nullable: true,
                defaultValue: "ru-RU");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                table: "AspNetUsers");
        }
    }
}
