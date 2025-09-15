using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurang_luna.Migrations
{
    /// <inheritdoc />
    public partial class updatte : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tables_TableNr",
                table: "Tables");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_TableNr",
                table: "Tables",
                column: "TableNr",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tables_TableNr",
                table: "Tables");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_TableNr",
                table: "Tables",
                column: "TableNr");
        }
    }
}
