using Microsoft.EntityFrameworkCore.Migrations;

namespace Bank.Migrations
{
    public partial class updatedecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Sum",
                table: "PrivateAccounts",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "Convert",
                table: "ConvertCurrencies",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Sum",
                table: "PrivateAccounts",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "Convert",
                table: "ConvertCurrencies",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
