using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PeopleAnalysis.Migrations
{
    public partial class add_time : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeComplete",
                table: "Requests",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeComplete",
                table: "Requests");
        }
    }
}
