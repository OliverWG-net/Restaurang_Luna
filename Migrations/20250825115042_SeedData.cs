using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurang_luna.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Customers_CustomerId_FK",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_TableId_FK",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Admins_UserName",
                table: "Admins");

            migrationBuilder.AlterColumn<string>(
                name: "TableNr",
                table: "Tables",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Customers",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoShowCount",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId_FK",
                table: "Bookings",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CancelledAt",
                table: "Bookings",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CheckedInAt",
                table: "Bookings",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CompletedAt",
                table: "Bookings",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Bookings",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsSnapshotLocked",
                table: "Bookings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Bookings",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "SnapshotEmail",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SnapshotName",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SnapshotNotes",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SnapshotPhone",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Bookings",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: new Guid("a2b3c4d5-e6f7-4a89-9b01-23456789abcd"),
                columns: new[] { "CancelledAt", "CheckedInAt", "CompletedAt", "CreatedAt", "Duration", "IsSnapshotLocked", "SnapshotEmail", "SnapshotName", "SnapshotNotes", "SnapshotPhone", "Status", "UpdatedAt" },
                values: new object[] { null, null, null, new DateTimeOffset(new DateTime(2025, 8, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 90, true, "bob@example.com", "Bob Svensson", null, "+46709876543", 0, null });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: new Guid("d6a6d2f0-1f44-4b77-9b6c-0c9d1a2b3c4d"),
                columns: new[] { "CancelledAt", "CheckedInAt", "CompletedAt", "CreatedAt", "Duration", "IsSnapshotLocked", "SnapshotEmail", "SnapshotName", "SnapshotNotes", "SnapshotPhone", "Status", "UpdatedAt" },
                values: new object[] { null, null, null, new DateTimeOffset(new DateTime(2025, 8, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 90, true, "alice@example.com", "Alice Johansson", null, "+46701234567", 0, null });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: new Guid("f1e2d3c4-b5a6-4789-8123-9abcdeff0011"),
                columns: new[] { "CancelledAt", "CheckedInAt", "CompletedAt", "CreatedAt", "Duration", "IsSnapshotLocked", "SnapshotEmail", "SnapshotName", "SnapshotNotes", "SnapshotPhone", "Status", "UpdatedAt" },
                values: new object[] { null, null, null, new DateTimeOffset(new DateTime(2025, 8, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 90, true, null, "Klara", null, "+46855500010", 0, null });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("a1c7d3a6-8b10-4a1f-90a4-7f2f0d9b1234"),
                columns: new[] { "Email", "NoShowCount", "Notes" },
                values: new object[] { "bob@example.com", 0, null });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("b5b55e03-2d42-4b64-9a1d-3a4fd0a9e6a1"),
                columns: new[] { "Email", "NoShowCount", "Notes" },
                values: new object[] { "alice@example.com", 0, null });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("c9e2f4b1-7a27-4c9c-8b3b-0b4c8a7f55e2"),
                columns: new[] { "Email", "NoShowCount", "Notes" },
                values: new object[] { null, 0, null });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 1,
                column: "TableNr",
                value: "T1");

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 2,
                column: "TableNr",
                value: "T2");

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 3,
                column: "TableNr",
                value: "T3");

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 4,
                column: "TableNr",
                value: "T4");

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 5,
                column: "TableNr",
                value: "T5");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_Status_StartAt",
                table: "Bookings",
                columns: new[] { "Status", "StartAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TableId_FK_StartAt",
                table: "Bookings",
                columns: new[] { "TableId_FK", "StartAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_UserName",
                table: "Admins",
                column: "UserName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Customers_CustomerId_FK",
                table: "Bookings",
                column: "CustomerId_FK",
                principalTable: "Customers",
                principalColumn: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Customers_CustomerId_FK",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_Status_StartAt",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_TableId_FK_StartAt",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Admins_UserName",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "NoShowCount",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CancelledAt",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CheckedInAt",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CompletedAt",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "IsSnapshotLocked",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "SnapshotEmail",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "SnapshotName",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "SnapshotNotes",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "SnapshotPhone",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Bookings");

            migrationBuilder.AlterColumn<int>(
                name: "TableNr",
                table: "Tables",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Customers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId_FK",
                table: "Bookings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 1,
                column: "TableNr",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 2,
                column: "TableNr",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 3,
                column: "TableNr",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 4,
                column: "TableNr",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 5,
                column: "TableNr",
                value: 5);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TableId_FK",
                table: "Bookings",
                column: "TableId_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_UserName",
                table: "Admins",
                column: "UserName");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Customers_CustomerId_FK",
                table: "Bookings",
                column: "CustomerId_FK",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
