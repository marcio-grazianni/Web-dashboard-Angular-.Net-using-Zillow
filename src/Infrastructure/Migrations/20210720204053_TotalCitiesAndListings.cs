using Microsoft.EntityFrameworkCore.Migrations;

namespace CDACommercial.PoC.Infrastructure.Migrations
{
    public partial class TotalCitiesAndListings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalCities",
                table: "States",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalListings",
                table: "States",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalListings",
                table: "Cities",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCities",
                table: "States");

            migrationBuilder.DropColumn(
                name: "TotalListings",
                table: "States");

            migrationBuilder.DropColumn(
                name: "TotalListings",
                table: "Cities");
        }
    }
}
