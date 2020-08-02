using Microsoft.EntityFrameworkCore.Migrations;

namespace RentMe.Migrations
{
    public partial class AddedRentPeriod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RentPeriod",
                table: "Announcements",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentPeriod",
                table: "Announcements");
        }
    }
}
