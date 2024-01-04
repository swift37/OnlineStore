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
            migrationBuilder.DropTable(
                name: "FiltersGroupSpecification");

            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "Specifications");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Specifications");

            migrationBuilder.AddColumn<int>(
                name: "SpecificationTypeId",
                table: "Specifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Specifications_SpecificationTypeId",
                table: "Specifications",
                column: "SpecificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FiltersGroupSpecificationType_SpecificationTypesId",
                table: "FiltersGroupSpecificationType",
                column: "SpecificationTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecificationTypes_Id",
                table: "SpecificationTypes",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Specifications_SpecificationTypes_SpecificationTypeId",
                table: "Specifications",
                column: "SpecificationTypeId",
                principalTable: "SpecificationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specifications_SpecificationTypes_SpecificationTypeId",
                table: "Specifications");

            migrationBuilder.DropTable(
                name: "FiltersGroupSpecificationType");

            migrationBuilder.DropTable(
                name: "SpecificationTypes");

            migrationBuilder.DropIndex(
                name: "IX_Specifications_SpecificationTypeId",
                table: "Specifications");

            migrationBuilder.DropColumn(
                name: "SpecificationTypeId",
                table: "Specifications");

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "Specifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Specifications",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

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
                name: "IX_FiltersGroupSpecification_SpecificationsId",
                table: "FiltersGroupSpecification",
                column: "SpecificationsId");
        }
    }
}
