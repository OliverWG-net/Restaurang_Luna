using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurang_luna.Migrations
{
    /// <inheritdoc />
    public partial class updateMenumodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PicutreUrl",
                table: "Menus",
                newName: "PictureUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PictureUrl",
                table: "Menus",
                newName: "PicutreUrl");
        }
    }
}
