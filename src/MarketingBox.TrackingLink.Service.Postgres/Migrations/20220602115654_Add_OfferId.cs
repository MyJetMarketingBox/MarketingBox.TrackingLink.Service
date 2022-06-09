using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketingBox.TrackingLink.Service.Postgres.Migrations
{
    public partial class Add_OfferId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OfferId",
                schema: "trackinglink-service",
                table: "trackinglinks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfferId",
                schema: "trackinglink-service",
                table: "trackinglinks");
        }
    }
}
