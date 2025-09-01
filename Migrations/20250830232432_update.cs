using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurang_luna.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookings_TableId_FK_StartAt",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Bookings");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Bookings",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Bookings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: new Guid("a2b3c4d5-e6f7-4a89-9b01-23456789abcd"),
                column: "Status",
                value: "Confirmed");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: new Guid("d6a6d2f0-1f44-4b77-9b6c-0c9d1a2b3c4d"),
                column: "Status",
                value: "Confirmed");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: new Guid("f1e2d3c4-b5a6-4789-8123-9abcdeff0011"),
                column: "Status",
                value: "Confirmed");

            migrationBuilder.CreateIndex(
                name: "UX_Bookings_TableId_StartAt_Active",
                table: "Bookings",
                columns: new[] { "TableId_FK", "StartAt" },
                unique: true,
                filter: "[IsActive] = 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UX_Bookings_TableId_StartAt_Active",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Bookings");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Bookings",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: new Guid("a2b3c4d5-e6f7-4a89-9b01-23456789abcd"),
                columns: new[] { "Duration", "Status" },
                values: new object[] { 90, 0 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: new Guid("d6a6d2f0-1f44-4b77-9b6c-0c9d1a2b3c4d"),
                columns: new[] { "Duration", "Status" },
                values: new object[] { 90, 0 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: new Guid("f1e2d3c4-b5a6-4789-8123-9abcdeff0011"),
                columns: new[] { "Duration", "Status" },
                values: new object[] { 90, 0 });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TableId_FK_StartAt",
                table: "Bookings",
                columns: new[] { "TableId_FK", "StartAt" });
        }
    }
}
