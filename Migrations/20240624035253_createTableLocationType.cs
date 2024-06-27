using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WarehouseAPI.Migrations
{
    /// <inheritdoc />
    public partial class createTableLocationType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TypeOfEnvironment",
                table: "Locations",
                newName: "LocationTypeId");

            migrationBuilder.CreateTable(
                name: "LocationType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locations_LocationTypeId",
                table: "Locations",
                column: "LocationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_LocationType_LocationTypeId",
                table: "Locations",
                column: "LocationTypeId",
                principalTable: "LocationType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_LocationType_LocationTypeId",
                table: "Locations");

            migrationBuilder.DropTable(
                name: "LocationType");

            migrationBuilder.DropIndex(
                name: "IX_Locations_LocationTypeId",
                table: "Locations");

            migrationBuilder.RenameColumn(
                name: "LocationTypeId",
                table: "Locations",
                newName: "TypeOfEnvironment");
        }
    }
}
