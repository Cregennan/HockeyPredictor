using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MegaHockey.Migrations
{
    public partial class AddResultToRecords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GameResult",
                table: "HistoryRecord",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameResult",
                table: "HistoryRecord");
        }
    }
}
