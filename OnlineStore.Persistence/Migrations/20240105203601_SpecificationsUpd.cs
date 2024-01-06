using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class SpecificationsUpd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpecificationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecificationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FiltersGroupSpecificationType",
                columns: table => new
                {
                    FiltersGroupId = table.Column<int>(type: "int", nullable: false),
                    SpecificationTypesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiltersGroupSpecificationType", x => new { x.FiltersGroupId, x.SpecificationTypesId });
                    table.ForeignKey(
                        name: "FK_FiltersGroupSpecificationType_FilterGoups_FiltersGroupId",
                        column: x => x.FiltersGroupId,
                        principalTable: "FilterGoups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FiltersGroupSpecificationType_SpecificationTypes_SpecificationTypesId",
                        column: x => x.SpecificationTypesId,
                        principalTable: "SpecificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Specifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecificationTypeId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Specifications_SpecificationTypes_SpecificationTypeId",
                        column: x => x.SpecificationTypeId,
                        principalTable: "SpecificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSpecification",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    SpecificationsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSpecification", x => new { x.ProductsId, x.SpecificationsId });
                    table.ForeignKey(
                        name: "FK_ProductSpecification_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSpecification_Specifications_SpecificationsId",
                        column: x => x.SpecificationsId,
                        principalTable: "Specifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FiltersGroupSpecificationType_SpecificationTypesId",
                table: "FiltersGroupSpecificationType",
                column: "SpecificationTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSpecification_SpecificationsId",
                table: "ProductSpecification",
                column: "SpecificationsId");

            migrationBuilder.CreateIndex(
                name: "IX_Specifications_Id",
                table: "Specifications",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Specifications_SpecificationTypeId",
                table: "Specifications",
                column: "SpecificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecificationTypes_Id",
                table: "SpecificationTypes",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FiltersGroupSpecificationType");

            migrationBuilder.DropTable(
                name: "ProductSpecification");

            migrationBuilder.DropTable(
                name: "Specifications");

            migrationBuilder.DropTable(
                name: "SpecificationTypes");
        }
    }
}
