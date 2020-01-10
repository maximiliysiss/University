using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkerPluginAPI.Migrations
{
    public partial class updateadmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Workers",
                columns: new[] { "ID", "Login", "PasswordHash", "Token", "WorkerType" },
                values: new object[] { 2, "Admin", "E3AFED0047B08059D0FADA10F400C1E5", null, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Workers",
                keyColumn: "ID",
                keyValue: 2);
        }
    }
}
