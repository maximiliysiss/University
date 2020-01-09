using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkerPluginAPI.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Workers",
                keyColumn: "ID",
                keyValue: 1,
                column: "PasswordHash",
                value: "62EFB9EC331E364B96EFE68C8B03CA20");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Workers",
                keyColumn: "ID",
                keyValue: 1,
                column: "PasswordHash",
                value: "");
        }
    }
}
