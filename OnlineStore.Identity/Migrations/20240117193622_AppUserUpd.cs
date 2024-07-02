using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStore.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AppUserUpd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfRegistration",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0AD36A86-F6CA-4AE5-AC56-C24C2D8DBFCC",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "DateOfRegistration", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f4323f88-5026-40e4-a80b-3ffa7dd78a85", null, new DateTime(2024, 1, 17, 20, 36, 21, 405, DateTimeKind.Local).AddTicks(4166), "AQAAAAIAAYagAAAAEKkd5+k6o42R6WDAMnh5/9IaQUX8Yxa7jgiWmP35ttFD8iFh4Im0jcHaPHKN+nd5kA==", "4a148524-aafc-4c8f-b374-bcbaa8de5365" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "A9127F04-292B-4A13-BFD9-F510BC2E2769",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "DateOfRegistration", "PasswordHash", "SecurityStamp" },
                values: new object[] { "901a622e-0dd0-4088-832f-6516e4311396", null, new DateTime(2024, 1, 17, 20, 36, 21, 451, DateTimeKind.Local).AddTicks(1560), "AQAAAAIAAYagAAAAEK9P9avR+S7jVxdyNpc6xKk7rE0ZFDzoimI/l8aVHbMeQY5UGZX+v1rQp4wXDBYnlg==", "25fdd092-ee45-433d-a929-b3718ff01726" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "E301AF60-A2CD-457B-A337-3B5BB73208DA",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "DateOfRegistration", "PasswordHash", "SecurityStamp" },
                values: new object[] { "975fdd5a-0025-42c9-9b13-691ce9775c1b", null, new DateTime(2024, 1, 17, 20, 36, 21, 359, DateTimeKind.Local).AddTicks(7785), "AQAAAAIAAYagAAAAEEZPZocjRUWpDAtCZ5swA+fD/VSS8xfqTeLDSUY66QP8V6scqMgPQCSPgD1lEc2qlg==", "eb825911-dfb2-4c05-95b6-8cbc46b6862c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "F457DC2A-9480-43C3-8136-288098C87117",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "DateOfRegistration", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7b0bd9d1-2c89-4d38-8d8e-4948bb0b5c94", null, new DateTime(2024, 1, 17, 20, 36, 21, 497, DateTimeKind.Local).AddTicks(2373), "AQAAAAIAAYagAAAAEHBRlOHK9zBnOY45GSCSKbqxXIFkuQk/Th83Gkl94xUkGFyekbz539QEdFVNSL+XBg==", "5b6253c1-e55d-4070-9c27-aec7d020588c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateOfRegistration",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0AD36A86-F6CA-4AE5-AC56-C24C2D8DBFCC",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "78d4dbb4-dde3-4846-a4b5-199c9b7ad446", "AQAAAAIAAYagAAAAEEkPBI5govfClUfESeQNGy3cAmGaNnABM4lhvr6MfCZe4BWNKrVO5ErZ/Tn2DfOB4g==", "acec17a7-9237-4361-8bae-8e7ead800d95" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "A9127F04-292B-4A13-BFD9-F510BC2E2769",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "19d29602-333e-4fc0-a1e4-22bce2964569", "AQAAAAIAAYagAAAAEKyVCuCabXYzIiFy5F6mD8O8q/Agv9RWtLq2ML4EfYHMEmB11FjGtIo+Og0RKSsqRg==", "c6b18998-7cb8-42a0-81cd-29f58983fb12" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "E301AF60-A2CD-457B-A337-3B5BB73208DA",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d3ec436d-6da7-4c07-829e-e9af89b853fa", "AQAAAAIAAYagAAAAEJ8Lr9Ajgj/Ej3U8MIFiVDL4V0NUYvvGuua8FQThivO+UTIz0DlPCb/EVdO/8NU26g==", "5f31a9b3-80df-46bb-b054-2bbbbfb370b6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "F457DC2A-9480-43C3-8136-288098C87117",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c65611e8-ef2b-415d-9f98-f8c2c87dbbfd", "AQAAAAIAAYagAAAAENhtKFQrTm4p9WGueCHNdAbL3CyNrmyA4C6n03ig/ybzWtmDBt9PeRUZO8nzoTgz3w==", "c6277422-2f8e-4d71-a46a-a141f8564f5f" });
        }
    }
}
