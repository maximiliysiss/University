using Microsoft.EntityFrameworkCore.Migrations;

namespace Bank.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Convert",
                table: "ConvertCurrencies",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Convert",
                table: "ConvertCurrencies");
        }
    }
}
