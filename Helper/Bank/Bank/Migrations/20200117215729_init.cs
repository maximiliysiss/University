using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bank.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<int>(nullable: false),
                    FIO = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    DocumentCode = table.Column<string>(nullable: true),
                    Login = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConvertCurrencies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyFromId = table.Column<int>(nullable: false),
                    CurrencyToId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConvertCurrencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConvertCurrencies_Currencies_CurrencyFromId",
                        column: x => x.CurrencyFromId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConvertCurrencies_Currencies_CurrencyToId",
                        column: x => x.CurrencyToId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PrivateAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Guid = table.Column<string>(nullable: true),
                    Sum = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrivateAccounts_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrivateAccounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrivateAccountFromId1 = table.Column<int>(nullable: true),
                    PrivateAccountToId1 = table.Column<int>(nullable: true),
                    Sum = table.Column<float>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operations_PrivateAccounts_PrivateAccountFromId1",
                        column: x => x.PrivateAccountFromId1,
                        principalTable: "PrivateAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Operations_PrivateAccounts_PrivateAccountToId1",
                        column: x => x.PrivateAccountToId1,
                        principalTable: "PrivateAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "DocumentCode", "FIO", "Login", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { 1, "Admin", "Admin", "Admin", "Admin", "E3AFED0047B08059D0FADA10F400C1E5", 0 },
                    { 2, "Client", "Client", "Client", "Client", "577D7068826DE925EA2AEC01DBADF5E4", 1 },
                    { 3, "Director", "Director", "Director", "Director", "7C5BA892645AF8D7DBA520E3978C726F", 2 },
                    { 4, "Worker", "Worker", "Worker", "Worker", "62EFB9EC331E364B96EFE68C8B03CA20", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConvertCurrencies_CurrencyFromId",
                table: "ConvertCurrencies",
                column: "CurrencyFromId");

            migrationBuilder.CreateIndex(
                name: "IX_ConvertCurrencies_CurrencyToId",
                table: "ConvertCurrencies",
                column: "CurrencyToId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_PrivateAccountFromId1",
                table: "Operations",
                column: "PrivateAccountFromId1");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_PrivateAccountToId1",
                table: "Operations",
                column: "PrivateAccountToId1");

            migrationBuilder.CreateIndex(
                name: "IX_PrivateAccounts_CurrencyId",
                table: "PrivateAccounts",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_PrivateAccounts_UserId",
                table: "PrivateAccounts",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConvertCurrencies");

            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropTable(
                name: "PrivateAccounts");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
