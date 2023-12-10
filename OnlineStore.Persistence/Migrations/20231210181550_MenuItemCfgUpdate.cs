using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class MenuItemCfgUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuItemNestedMenuItem");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "NestedMenuItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_NestedMenuItems_ParentId",
                table: "NestedMenuItems",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_NestedMenuItems_MenuItems_ParentId",
                table: "NestedMenuItems",
                column: "ParentId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NestedMenuItems_MenuItems_ParentId",
                table: "NestedMenuItems");

            migrationBuilder.DropIndex(
                name: "IX_NestedMenuItems_ParentId",
                table: "NestedMenuItems");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "NestedMenuItems");

            migrationBuilder.CreateTable(
                name: "MenuItemNestedMenuItem",
                columns: table => new
                {
                    MenuItemId = table.Column<int>(type: "int", nullable: false),
                    NestedItemsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItemNestedMenuItem", x => new { x.MenuItemId, x.NestedItemsId });
                    table.ForeignKey(
                        name: "FK_MenuItemNestedMenuItem_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuItemNestedMenuItem_NestedMenuItems_NestedItemsId",
                        column: x => x.NestedItemsId,
                        principalTable: "NestedMenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemNestedMenuItem_NestedItemsId",
                table: "MenuItemNestedMenuItem",
                column: "NestedItemsId");
        }
    }
}
