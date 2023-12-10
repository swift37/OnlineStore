using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class MenuItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_MenuItems_MenuItemId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Categories_CategoryId",
                table: "MenuItems");

            migrationBuilder.DropIndex(
                name: "IX_MenuItems_CategoryId",
                table: "MenuItems");

            migrationBuilder.DropIndex(
                name: "IX_Categories_MenuItemId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "MenuItemId",
                table: "Categories");

            migrationBuilder.AddColumn<bool>(
                name: "IsMegaMenu",
                table: "MenuItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "MenuItems",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "NestedMenuItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HasTwoColumns = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NestedMenuItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryNestedMenuItem",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    NestedMenuItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryNestedMenuItem", x => new { x.CategoriesId, x.NestedMenuItemId });
                    table.ForeignKey(
                        name: "FK_CategoryNestedMenuItem_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryNestedMenuItem_NestedMenuItems_NestedMenuItemId",
                        column: x => x.NestedMenuItemId,
                        principalTable: "NestedMenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_MenuItems_CategoryId",
                table: "MenuItems",
                column: "CategoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_Id",
                table: "MenuItems",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryNestedMenuItem_NestedMenuItemId",
                table: "CategoryNestedMenuItem",
                column: "NestedMenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemNestedMenuItem_NestedItemsId",
                table: "MenuItemNestedMenuItem",
                column: "NestedItemsId");

            migrationBuilder.CreateIndex(
                name: "IX_NestedMenuItems_Id",
                table: "NestedMenuItems",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Categories_CategoryId",
                table: "MenuItems",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Categories_CategoryId",
                table: "MenuItems");

            migrationBuilder.DropTable(
                name: "CategoryNestedMenuItem");

            migrationBuilder.DropTable(
                name: "MenuItemNestedMenuItem");

            migrationBuilder.DropTable(
                name: "NestedMenuItems");

            migrationBuilder.DropIndex(
                name: "IX_MenuItems_CategoryId",
                table: "MenuItems");

            migrationBuilder.DropIndex(
                name: "IX_MenuItems_Id",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "IsMegaMenu",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "MenuItems");

            migrationBuilder.AddColumn<int>(
                name: "MenuItemId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_CategoryId",
                table: "MenuItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_MenuItemId",
                table: "Categories",
                column: "MenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_MenuItems_MenuItemId",
                table: "Categories",
                column: "MenuItemId",
                principalTable: "MenuItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Categories_CategoryId",
                table: "MenuItems",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
