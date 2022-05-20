using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketingBox.TrackingLink.Service.Postgres.Migrations
{
    public partial class MultiTenancy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "trackinglink-service",
                table: "trackinglinks",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "trackinglink-service",
                table: "trackinglinks");
        }
    }
}
