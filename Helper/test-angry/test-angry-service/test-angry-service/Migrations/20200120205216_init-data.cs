using Microsoft.EntityFrameworkCore.Migrations;

namespace test_angry_service.Migrations
{
    public partial class initdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Content" },
                values: new object[] { 1, "Вопрос 1" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Content" },
                values: new object[] { 2, "Вопрос 2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
