using Microsoft.EntityFrameworkCore.Migrations;

namespace RockShop.Migrations
{
    public partial class migrate_add_admin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Login", "PasswordHash" },
                values: new object[] { 1L, "admin", "21232F297A57A5A743894A0E4A801FC3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
