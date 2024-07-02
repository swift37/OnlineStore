using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class FieldNamesUpd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Wishlists",
                newName: "CreationDate");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Orders",
                newName: "CreationDate");

            migrationBuilder.AddColumn<bool>(
                name: "IsConsidered",
                table: "ContactRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConsidered",
                table: "ContactRequests");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Wishlists",
                newName: "CreateDate");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Orders",
                newName: "CreatedDate");
        }
    }
}
