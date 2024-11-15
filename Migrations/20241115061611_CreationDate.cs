using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wishlist.Migrations
{
    /// <inheritdoc />
    public partial class CreationDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Wishlists",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Wishlists",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_WishlistWish_WishId_WishlistId",
                table: "WishlistWish",
                columns: new[] { "WishId", "WishlistId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WishlistWish_WishId_WishlistId",
                table: "WishlistWish");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Wishlists");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Wishlists");
        }
    }
}
