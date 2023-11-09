using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_MainMenuItems_MainMenuItemId",
                table: "Categories");

            migrationBuilder.DropTable(
                name: "MainMenuItems");

            migrationBuilder.RenameColumn(
                name: "MainMenuItemId",
                table: "Categories",
                newName: "MenuItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_MainMenuItemId",
                table: "Categories",
                newName: "IX_Categories_MenuItemId");

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItems_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_CategoryId",
                table: "MenuItems",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_MenuItems_MenuItemId",
                table: "Categories",
                column: "MenuItemId",
                principalTable: "MenuItems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_MenuItems_MenuItemId",
                table: "Categories");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.RenameColumn(
                name: "MenuItemId",
                table: "Categories",
                newName: "MainMenuItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_MenuItemId",
                table: "Categories",
                newName: "IX_Categories_MainMenuItemId");

            migrationBuilder.CreateTable(
                name: "MainMenuItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainMenuItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MainMenuItems_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MainMenuItems_CategoryId",
                table: "MainMenuItems",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_MainMenuItems_MainMenuItemId",
                table: "Categories",
                column: "MainMenuItemId",
                principalTable: "MainMenuItems",
                principalColumn: "Id");
        }
    }
}
