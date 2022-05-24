using Microsoft.EntityFrameworkCore.Migrations;

namespace CDACommercial.PoC.Infrastructure.Migrations
{
    public partial class JobParameters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Parameters",
                table: "Jobs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Jobs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Parameters",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Jobs");
        }
    }
}
