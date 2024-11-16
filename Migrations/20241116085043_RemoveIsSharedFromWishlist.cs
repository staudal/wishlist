using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wishlist.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIsSharedFromWishlist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsShared",
                table: "Wishlists");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsShared",
                table: "Wishlists",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
