using Microsoft.EntityFrameworkCore.Migrations;

namespace CDACommercial.PoC.Infrastructure.Migrations
{
    public partial class Predictions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PredVsPrice",
                table: "Listings",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PredictedRevenue",
                table: "Listings",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                "IX_Listings_ZillowId", 
                "Listings", 
                "ZillowId");

             migrationBuilder.CreateIndex(
                "IX_Cities_State", 
                "Cities", 
                "State");
            //ALTER TABLE Cities MODIFY COLUMN State varchar(10);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PredVsPrice",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "PredictedRevenue",
                table: "Listings");
        }
    }
}
