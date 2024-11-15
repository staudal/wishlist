using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wishlist.Migrations
{
    /// <inheritdoc />
    public partial class AddWishLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LinkUrl",
                table: "Wishes",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkUrl",
                table: "Wishes");
        }
    }
}
