using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Wishlist.Migrations
{
    public partial class UpdateIdsToGuidWithRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Step 1: Rename existing tables
            migrationBuilder.RenameTable(name: "Wishlists", newName: "Wishlists_Old");
            migrationBuilder.RenameTable(name: "Wishes", newName: "Wishes_Old");
            migrationBuilder.RenameTable(name: "ShareLinks", newName: "ShareLinks_Old");
            migrationBuilder.RenameTable(name: "WishlistWish", newName: "WishlistWish_Old");

            // Step 2: Create new tables with updated schema
            migrationBuilder.CreateTable(
                name: "Wishlists",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Title = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false, defaultValue: DateTime.UtcNow),
                    LastUpdated = table.Column<DateTime>(nullable: false, defaultValue: DateTime.UtcNow),
                    IsShared = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wishes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Title = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Currency = table.Column<string>(nullable: false),
                    Amount = table.Column<int>(nullable: false, defaultValue: 1),
                    Description = table.Column<string>(nullable: true),
                    LinkUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShareLinks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Token = table.Column<string>(maxLength: 128, nullable: false),
                    WishlistId = table.Column<Guid>(nullable: false),
                    ExpiryDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShareLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShareLinks_Wishlists_WishlistId",
                        column: x => x.WishlistId,
                        principalTable: "Wishlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishlistWish",
                columns: table => new
                {
                    WishlistId = table.Column<Guid>(nullable: false),
                    WishId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistWish", x => new { x.WishlistId, x.WishId });
                    table.ForeignKey(
                        name: "FK_WishlistWish_Wishlists_WishlistId",
                        column: x => x.WishlistId,
                        principalTable: "Wishlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishlistWish_Wishes_WishId",
                        column: x => x.WishId,
                        principalTable: "Wishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Step 3: Copy data from old tables to new tables
            migrationBuilder.Sql(@"
                INSERT INTO ""Wishlists"" (""Id"", ""Title"", ""UserId"", ""CreationDate"", ""LastUpdated"", ""IsShared"")
                SELECT uuid_generate_v4(), ""Title"", ""UserId"", ""CreationDate"", ""LastUpdated"", ""IsShared""
                FROM ""Wishlists_Old"";");

            migrationBuilder.Sql(@"
                INSERT INTO ""Wishes"" (""Id"", ""Title"", ""UserId"", ""ImageUrl"", ""Price"", ""Currency"", ""Amount"", ""Description"", ""LinkUrl"")
                SELECT uuid_generate_v4(), ""Title"", ""UserId"", ""ImageUrl"", ""Price"", ""Currency"", ""Amount"", ""Description"", ""LinkUrl""
                FROM ""Wishes_Old"";");

            migrationBuilder.Sql(@"
                INSERT INTO ""ShareLinks"" (""Id"", ""Token"", ""WishlistId"", ""ExpiryDate"", ""Name"")
                SELECT uuid_generate_v4(), ""Token"", ""WishlistId"", ""ExpiryDate"", ""Name""
                FROM ""ShareLinks_Old"";");

            migrationBuilder.Sql(@"
                INSERT INTO ""WishlistWish"" (""WishlistId"", ""WishId"")
                SELECT ""WishlistId"", ""WishId""
                FROM ""WishlistWish_Old"";");

            // Step 4: Drop old tables
            migrationBuilder.DropTable(name: "Wishlists_Old");
            migrationBuilder.DropTable(name: "Wishes_Old");
            migrationBuilder.DropTable(name: "ShareLinks_Old");
            migrationBuilder.DropTable(name: "WishlistWish_Old");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert primary keys, foreign keys, and columns to int
            // (Essentially the reverse of the Up method)
            migrationBuilder.RenameTable(name: "Wishlists", newName: "Wishlists_Old");
            migrationBuilder.RenameTable(name: "Wishes", newName: "Wishes_Old");
            migrationBuilder.RenameTable(name: "ShareLinks", newName: "ShareLinks_Old");
            migrationBuilder.RenameTable(name: "WishlistWish", newName: "WishlistWish_Old");

            migrationBuilder.CreateTable(
                name: "Wishlists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Title = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false, defaultValue: DateTime.UtcNow),
                    LastUpdated = table.Column<DateTime>(nullable: false, defaultValue: DateTime.UtcNow),
                    IsShared = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wishes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Title = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Currency = table.Column<string>(nullable: false),
                    Amount = table.Column<int>(nullable: false, defaultValue: 1),
                    Description = table.Column<string>(nullable: true),
                    LinkUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShareLinks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Token = table.Column<string>(maxLength: 128, nullable: false),
                    WishlistId = table.Column<int>(nullable: false),
                    ExpiryDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShareLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShareLinks_Wishlists_WishlistId",
                        column: x => x.WishlistId,
                        principalTable: "Wishlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishlistWish",
                columns: table => new
                {
                    WishlistId = table.Column<int>(nullable: false),
                    WishId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistWish", x => new { x.WishlistId, x.WishId });
                    table.ForeignKey(
                        name: "FK_WishlistWish_Wishlists_WishlistId",
                        column: x => x.WishlistId,
                        principalTable: "Wishlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishlistWish_Wishes_WishId",
                        column: x => x.WishId,
                        principalTable: "Wishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Copy data from old tables back to new tables
            migrationBuilder.Sql(@"
                INSERT INTO ""Wishlists"" (""Id"", ""Title"", ""UserId"", ""CreationDate"", ""LastUpdated"", ""IsShared"")
                SELECT Id, ""Title"", ""UserId"", ""CreationDate"", ""LastUpdated"", ""IsShared""
                FROM ""Wishlists_Old"";");

            migrationBuilder.Sql(@"
                INSERT INTO ""Wishes"" (""Id"", ""Title"", ""UserId"", ""ImageUrl"", ""Price"", ""Currency"", ""Amount"", ""Description"", ""LinkUrl"")
                SELECT Id, ""Title"", ""UserId"", ""ImageUrl"", ""Price"", ""Currency"", ""Amount"", ""Description"", ""LinkUrl""
                FROM ""Wishes_Old"";");

            migrationBuilder.Sql(@"
                INSERT INTO ""ShareLinks"" (""Id"", ""Token"", ""WishlistId"", ""ExpiryDate"", ""Name"")
                SELECT Id, ""Token"", ""WishlistId"", ""ExpiryDate"", ""Name""
                FROM ""ShareLinks_Old"";");

            migrationBuilder.Sql(@"
                INSERT INTO ""WishlistWish"" (""WishlistId"", ""WishId"")
                SELECT ""WishlistId"", ""WishId""
                FROM ""WishlistWish_Old"";");

            // Drop old tables
            migrationBuilder.DropTable(name: "Wishlists_Old");
            migrationBuilder.DropTable(name: "Wishes_Old");
            migrationBuilder.DropTable(name: "ShareLinks_Old");
            migrationBuilder.DropTable(name: "WishlistWish_Old");
        }
    }
}