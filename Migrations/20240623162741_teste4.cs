using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseAPI.Migrations
{
    /// <inheritdoc />
    public partial class teste4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstitutionalAssets_AssetType_AssetTypeId",
                table: "InstitutionalAssets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssetType",
                table: "AssetType");

            migrationBuilder.RenameTable(
                name: "AssetType",
                newName: "AssetTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssetTypes",
                table: "AssetTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InstitutionalAssets_AssetTypes_AssetTypeId",
                table: "InstitutionalAssets",
                column: "AssetTypeId",
                principalTable: "AssetTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstitutionalAssets_AssetTypes_AssetTypeId",
                table: "InstitutionalAssets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssetTypes",
                table: "AssetTypes");

            migrationBuilder.RenameTable(
                name: "AssetTypes",
                newName: "AssetType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssetType",
                table: "AssetType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InstitutionalAssets_AssetType_AssetTypeId",
                table: "InstitutionalAssets",
                column: "AssetTypeId",
                principalTable: "AssetType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
