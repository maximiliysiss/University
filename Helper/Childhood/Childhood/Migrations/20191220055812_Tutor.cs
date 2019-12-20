using Microsoft.EntityFrameworkCore.Migrations;

namespace Childhood.Migrations
{
    public partial class Tutor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "FIO", "Login", "PasswordHash", "Phone", "UserType" },
                values: new object[] { 2, null, "Tutor", "2EC7805E0CCE36C07750D04F60857CD9", "000", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 2);
        }
    }
}
