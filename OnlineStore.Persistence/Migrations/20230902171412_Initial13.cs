using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MainMenuItemId",
                table: "Categories",
                type: "int",
                nullable: true);

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
                name: "IX_Categories_MainMenuItemId",
                table: "Categories",
                column: "MainMenuItemId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_MainMenuItems_MainMenuItemId",
                table: "Categories");

            migrationBuilder.DropTable(
                name: "MainMenuItems");

            migrationBuilder.DropIndex(
                name: "IX_Categories_MainMenuItemId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "MainMenuItemId",
                table: "Categories");
        }
    }
}
