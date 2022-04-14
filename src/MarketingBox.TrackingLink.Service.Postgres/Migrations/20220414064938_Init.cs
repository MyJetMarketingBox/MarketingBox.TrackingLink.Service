using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MarketingBox.TrackingLink.Service.Postgres.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "trackinglink-service");

            migrationBuilder.CreateTable(
                name: "trackinglinks",
                schema: "trackinglink-service",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClickId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BrandId = table.Column<long>(type: "bigint", nullable: false),
                    AffiliateId = table.Column<long>(type: "bigint", nullable: false),
                    Link = table.Column<string>(type: "text", nullable: false),
                    LinkParameterValues_Language = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    LinkParameterValues_MPC_1 = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    LinkParameterValues_MPC_2 = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    LinkParameterValues_MPC_3 = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    LinkParameterValues_MPC_4 = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    LinkParameterNames_ClickId = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    LinkParameterNames_Language = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    LinkParameterNames_MPC_1 = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    LinkParameterNames_MPC_2 = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    LinkParameterNames_MPC_3 = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    LinkParameterNames_MPC_4 = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    UniqueId = table.Column<string>(type: "text", nullable: false),
                    RegistrationId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trackinglinks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_trackinglinks_ClickId",
                schema: "trackinglink-service",
                table: "trackinglinks",
                column: "ClickId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_trackinglinks_UniqueId",
                schema: "trackinglink-service",
                table: "trackinglinks",
                column: "UniqueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "trackinglinks",
                schema: "trackinglink-service");
        }
    }
}
