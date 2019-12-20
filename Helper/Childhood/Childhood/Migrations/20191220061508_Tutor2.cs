using Microsoft.EntityFrameworkCore.Migrations;

namespace Childhood.Migrations
{
    public partial class Tutor2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TutorId",
                table: "Groups",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TutorId",
                table: "Groups",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
