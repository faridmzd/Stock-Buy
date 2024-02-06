using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock_Buy.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bundles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bundles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssociatedBundles",
                columns: table => new
                {
                    ParentBundleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChildBundleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuantityNeeded = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssociatedBundles", x => new { x.ParentBundleId, x.ChildBundleId });
                    table.ForeignKey(
                        name: "FK_AssociatedBundles_Bundles_ChildBundleId",
                        column: x => x.ChildBundleId,
                        principalTable: "Bundles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssociatedBundles_Bundles_ParentBundleId",
                        column: x => x.ParentBundleId,
                        principalTable: "Bundles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AssociatedParts",
                columns: table => new
                {
                    BundleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuantityNeeded = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssociatedParts", x => new { x.BundleId, x.PartId });
                    table.ForeignKey(
                        name: "FK_AssociatedParts_Bundles_BundleId",
                        column: x => x.BundleId,
                        principalTable: "Bundles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssociatedParts_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssociatedBundles_ChildBundleId",
                table: "AssociatedBundles",
                column: "ChildBundleId");

            migrationBuilder.CreateIndex(
                name: "IX_AssociatedParts_PartId",
                table: "AssociatedParts",
                column: "PartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssociatedBundles");

            migrationBuilder.DropTable(
                name: "AssociatedParts");

            migrationBuilder.DropTable(
                name: "Bundles");

            migrationBuilder.DropTable(
                name: "Parts");
        }
    }
}
