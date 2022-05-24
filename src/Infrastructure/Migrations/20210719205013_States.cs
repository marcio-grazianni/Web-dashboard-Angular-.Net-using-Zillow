using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CDACommercial.PoC.Infrastructure.Migrations
{
    public partial class States : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        
            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.InsertData("States", new string[] {"Code", "Name", "CreatedAt"}, new object[] {"AZ", "Arizona", DateTime.UtcNow});

            migrationBuilder.AddColumn<double>(
                name: "PredictedRevenueOptimized",
                table: "Listings",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "StateId",
                table: "Cities",
                nullable: false,
                defaultValue: 1L);
            
            migrationBuilder.CreateIndex(
                name: "IX_Cities_StateId",
                table: "Cities",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_States_StateId",
                table: "Cities",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_States_StateId",
                table: "Cities");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropIndex(
                name: "IX_Cities_StateId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "PredictedRevenueOptimized",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Cities");
        }
    }
}
