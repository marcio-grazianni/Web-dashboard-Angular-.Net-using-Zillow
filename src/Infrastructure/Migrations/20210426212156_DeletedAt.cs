using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CDACommercial.PoC.Infrastructure.Migrations
{
    public partial class DeletedAt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "MonthlyRecords",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Listings",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Likes",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Jobs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "HistoryRequests",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Cities",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ApifyRuns",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "MonthlyRecords");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "HistoryRequests");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ApifyRuns");
        }
    }
}
