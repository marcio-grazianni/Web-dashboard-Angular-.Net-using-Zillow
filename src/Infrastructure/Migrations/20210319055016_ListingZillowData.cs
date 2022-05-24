using Microsoft.EntityFrameworkCore.Migrations;

namespace CDACommercial.PoC.Infrastructure.Migrations
{
    public partial class ListingZillowData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceHistory",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "TaxHistory",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "HighCapRate",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "LowCapRate",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "MiddleCapRate",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "RentalizerData",
                table: "Listings");

            migrationBuilder.AddColumn<string>(
                name: "HouseType",
                table: "Listings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZillowData",
                table: "Listings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PriceHistory",
                table: "Listings",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxHistory",
                table: "Listings",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.DropColumn(
                name: "HouseType",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "ZillowData",
                table: "Listings");

            migrationBuilder.AddColumn<double>(
                name: "HighCapRate",
                table: "Listings",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LowCapRate",
                table: "Listings",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MiddleCapRate",
                table: "Listings",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "RentalizerData",
                table: "Listings",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
