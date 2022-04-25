using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QASite.Data.Migrations
{
    public partial class answernameanddate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateSubmitted",
                table: "Answers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Answers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateSubmitted",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Answers");
        }
    }
}
