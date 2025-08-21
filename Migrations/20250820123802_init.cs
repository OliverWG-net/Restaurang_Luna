using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Restaurang_luna.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuItem = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsPopular = table.Column<bool>(type: "bit", nullable: false),
                    PicutreUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.MenuId);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    TableId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableNr = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.TableId);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    GuestAmount = table.Column<int>(type: "int", nullable: false),
                    TableId_FK = table.Column<int>(type: "int", nullable: false),
                    CustomerId_FK = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_Customers_CustomerId_FK",
                        column: x => x.CustomerId_FK,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Tables_TableId_FK",
                        column: x => x.TableId_FK,
                        principalTable: "Tables",
                        principalColumn: "TableId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "PasswordHash", "UserName" },
                values: new object[] { new Guid("8f4f1a1e-3a08-4f6a-9a8b-6b2a7d2c4f10"), "G/ZurCUC4QMRDqXwZwQRsA==;ULAG2GRXijaJasIKRhSJUTZtZQGTsAm3UTjPE7xsEBY=", "Oliver" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("a1c7d3a6-8b10-4a1f-90a4-7f2f0d9b1234"), "Bob", "Svensson", "+46709876543" },
                    { new Guid("b5b55e03-2d42-4b64-9a1d-3a4fd0a9e6a1"), "Alice", "Johansson", "+46701234567" },
                    { new Guid("c9e2f4b1-7a27-4c9c-8b3b-0b4c8a7f55e2"), "Klara", null, "+46855500010" }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuId", "Description", "IsPopular", "MenuItem", "PicutreUrl", "Price" },
                values: new object[,]
                {
                    { 1, "Classic pasta with creamy egg sauce, pancetta, and parmesan.", true, "Spaghetti Carbonara", "https://vasterbottensost.com/wp-content/uploads/2025/03/carbonara.jpg", 120.0 },
                    { 2, "Spicy tomato sauce with garlic, chili, and fresh parsley.", false, "Penne Arrabbiata", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS-3mx7DCimLIn1_B7a5JbY8SvzPT4jpahzeQ&s", 100.0 },
                    { 3, "Rich butter and parmesan sauce with silky fettuccine pasta.", true, "Fettuccine Alfredo", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQpfJYxux157r0GogK-XhDhxjnTvmQYVgaPUg&s", 140.0 },
                    { 4, "Layered pasta with beef ragù, béchamel, and parmesan.", true, "Lasagna Bolognese", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRkkjn3cMbzsNByc3-WkOob9H0c3ioINWgE5g&s", 160.0 },
                    { 5, "Wood-fired pizza with tomato, mozzarella, and fresh basil.", true, "Margherita Pizza", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSpBDIHxbtMe_C5SJhO2AujarCzMCKXQpO6Qw&s", 110.0 },
                    { 6, "Full-bodied Italian red wine, served by the glass.", false, "Red Wine (Glass)", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTLeLeVxyjldu8sSuMuPdL3EgzMT8Y68uWECg&s", 75.0 },
                    { 7, "Crisp Italian white wine, served chilled by the glass.", false, "White Wine (Glass)", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSiRqs99I_sHPJ6eWHmEKrMYo66ZWYNLDNnqg&s", 75.0 },
                    { 8, "Strong Italian coffee served in a small cup.", true, "Espresso", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRqpOw-kv9-J1OzjJICx_KZEU-KhhLYfYKF1w&s", 40.0 },
                    { 9, "Classic Italian dessert with mascarpone and espresso.", true, "Tiramisu", "https://staticcookist.akamaized.net/wp-content/uploads/sites/22/2024/09/THUMB-VIDEO-2_rev1-56.jpeg", 90.0 }
                });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "TableId", "Capacity", "TableNr" },
                values: new object[,]
                {
                    { 1, 2, 1 },
                    { 2, 4, 2 },
                    { 3, 4, 3 },
                    { 4, 6, 4 },
                    { 5, 8, 5 }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "BookingId", "CustomerId_FK", "GuestAmount", "StartAt", "TableId_FK" },
                values: new object[,]
                {
                    { new Guid("a2b3c4d5-e6f7-4a89-9b01-23456789abcd"), new Guid("a1c7d3a6-8b10-4a1f-90a4-7f2f0d9b1234"), 4, new DateTimeOffset(new DateTime(2025, 9, 2, 18, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2 },
                    { new Guid("d6a6d2f0-1f44-4b77-9b6c-0c9d1a2b3c4d"), new Guid("b5b55e03-2d42-4b64-9a1d-3a4fd0a9e6a1"), 2, new DateTimeOffset(new DateTime(2025, 9, 1, 17, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { new Guid("f1e2d3c4-b5a6-4789-8123-9abcdeff0011"), new Guid("c9e2f4b1-7a27-4c9c-8b3b-0b4c8a7f55e2"), 6, new DateTimeOffset(new DateTime(2025, 9, 3, 19, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_UserName",
                table: "Admins",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CustomerId_FK",
                table: "Bookings",
                column: "CustomerId_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_StartAt",
                table: "Bookings",
                column: "StartAt");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TableId_FK",
                table: "Bookings",
                column: "TableId_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PhoneNumber",
                table: "Customers",
                column: "PhoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_MenuItem",
                table: "Menus",
                column: "MenuItem");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_TableNr",
                table: "Tables",
                column: "TableNr");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Tables");
        }
    }
}
