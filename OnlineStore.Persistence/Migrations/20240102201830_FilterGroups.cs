using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class FilterGroups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSpecification_Products_ProductId",
                table: "ProductSpecification");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductSpecification",
                newName: "ProductsId");

            migrationBuilder.CreateTable(
                name: "FilterGoups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterGoups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterGoups_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FiltersGroupSpecification",
                columns: table => new
                {
                    FiltersGroupId = table.Column<int>(type: "int", nullable: false),
                    SpecificationsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiltersGroupSpecification", x => new { x.FiltersGroupId, x.SpecificationsId });
                    table.ForeignKey(
                        name: "FK_FiltersGroupSpecification_FilterGoups_FiltersGroupId",
                        column: x => x.FiltersGroupId,
                        principalTable: "FilterGoups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FiltersGroupSpecification_Specifications_SpecificationsId",
                        column: x => x.SpecificationsId,
                        principalTable: "Specifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilterGoups_CategoryId",
                table: "FilterGoups",
                column: "CategoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilterGoups_Id",
                table: "FilterGoups",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FiltersGroupSpecification_SpecificationsId",
                table: "FiltersGroupSpecification",
                column: "SpecificationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSpecification_Products_ProductsId",
                table: "ProductSpecification",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSpecification_Products_ProductsId",
                table: "ProductSpecification");

            migrationBuilder.DropTable(
                name: "FiltersGroupSpecification");

            migrationBuilder.DropTable(
                name: "FilterGoups");

            migrationBuilder.RenameColumn(
                name: "ProductsId",
                table: "ProductSpecification",
                newName: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSpecification_Products_ProductId",
                table: "ProductSpecification",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
