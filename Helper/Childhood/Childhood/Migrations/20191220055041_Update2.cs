using Microsoft.EntityFrameworkCore.Migrations;

namespace Childhood.Migrations
{
    public partial class Update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TutorId",
                table: "Groups",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_TutorId",
                table: "Groups",
                column: "TutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Users_TutorId",
                table: "Groups",
                column: "TutorId",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Users_TutorId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_TutorId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "TutorId",
                table: "Groups");
        }
    }
}
