using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CDACommercial.PoC.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    MarketId = table.Column<long>(nullable: false),
                    MarketName = table.Column<string>(nullable: true),
                    MarketCode = table.Column<string>(nullable: true),
                    Stats = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoryRequests",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    CityId = table.Column<long>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Bedrooms = table.Column<int>(nullable: false),
                    Data = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryRequests_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Listings",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    CityId = table.Column<long>(nullable: false),
                    ZillowId = table.Column<long>(nullable: false),
                    DaysOnZillow = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    LowCapRate = table.Column<double>(nullable: false),
                    MiddleCapRate = table.Column<double>(nullable: false),
                    HighCapRate = table.Column<double>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    LastTaxPaid = table.Column<double>(nullable: false),
                    TaxHistory = table.Column<string>(nullable: true),
                    PriceHistory = table.Column<string>(nullable: true),
                    Stats = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Zipcode = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    SquareFootage = table.Column<float>(nullable: false),
                    Bedrooms = table.Column<float>(nullable: false),
                    Bathrooms = table.Column<float>(nullable: false),
                    RentalizerData = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Listings_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonthlyRecords",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    CityId = table.Column<long>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Bedrooms = table.Column<int>(nullable: false),
                    Percentiles = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonthlyRecords_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryRequests_CityId",
                table: "HistoryRequests",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_CityId",
                table: "Listings",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyRecords_CityId",
                table: "MonthlyRecords",
                column: "CityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryRequests");

            migrationBuilder.DropTable(
                name: "Listings");

            migrationBuilder.DropTable(
                name: "MonthlyRecords");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
