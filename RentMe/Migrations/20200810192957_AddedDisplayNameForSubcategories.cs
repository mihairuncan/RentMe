using Microsoft.EntityFrameworkCore.Migrations;

namespace RentMe.Migrations
{
    public partial class AddedDisplayNameForSubcategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "Subcategories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "Subcategories");
        }
    }
}
