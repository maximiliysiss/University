using Microsoft.EntityFrameworkCore.Migrations;

namespace PeopleAnalysis.Migrations
{
    public partial class UpdateState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Count",
                table: "ResultObjects",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Count",
                table: "ResultObjects",
                type: "integer",
                nullable: false,
                oldClrType: typeof(float));
        }
    }
}
