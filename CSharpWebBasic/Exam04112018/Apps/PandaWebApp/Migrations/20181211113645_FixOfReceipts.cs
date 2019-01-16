using Microsoft.EntityFrameworkCore.Migrations;

namespace PandaWebApp.Migrations
{
    public partial class FixOfReceipts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Fee",
                table: "Receipts",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fee",
                table: "Receipts");
        }
    }
}
