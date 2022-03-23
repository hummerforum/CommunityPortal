using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CommunityPortal.Data.Migrations
{
    public partial class DiscussionPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "06178627-a0d4-44cc-8335-de572873424d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "26dd96ed-80a9-4566-be77-b9e1c6dc5854");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3e98e936-2069-474d-ae5e-8cdb5a4a4f60", "b8b5ad4e-87da-4daf-bc47-b8833be07d7b" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3e98e936-2069-474d-ae5e-8cdb5a4a4f60");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b8b5ad4e-87da-4daf-bc47-b8833be07d7b");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "NewsPosts",
                columns: table => new
                {
                    NewsPostId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostType = table.Column<int>(type: "int", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Heading = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Information = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsPosts", x => x.NewsPostId);
                    table.ForeignKey(
                        name: "FK_NewsPosts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NewsPosts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c7a9050d-abe1-4985-a04b-7bbdf7227e70", "a3c039a0-4a3b-48ea-bcda-83640c803219", "Admin", "ADMIN" },
                    { "8d2096aa-f396-42bd-9a18-582df3c88d63", "62d28325-b1de-4e4d-b5a8-5b1c710f001f", "Moderator", "MODERATOR" },
                    { "9b2e3711-b965-4d61-a20b-78827751bd0f", "dce23f5a-3b8a-4084-9e25-5f414af5c3e4", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "afcadf21-0612-4388-a6a0-8d6e37f010db", 0, "6b6ff339-80d6-4a7b-9e5c-22bd750482b8", "a@b.com", false, false, null, "A@B.COM", "A@B.COM", "AQAAAAEAACcQAAAAEMsUq//BxS0EWeE38LusFEJKjCYWyfF9PM+7glAZPagTWRkEuBlYdVrgVXJTdOrGug==", null, false, "69a91796-4a62-44de-88ab-274f3a9e26d8", false, "a@b.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "9b2e3711-b965-4d61-a20b-78827751bd0f", "afcadf21-0612-4388-a6a0-8d6e37f010db" });

            migrationBuilder.CreateIndex(
                name: "IX_NewsPosts_CategoryId",
                table: "NewsPosts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsPosts_UserId",
                table: "NewsPosts",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsPosts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d2096aa-f396-42bd-9a18-582df3c88d63");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7a9050d-abe1-4985-a04b-7bbdf7227e70");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9b2e3711-b965-4d61-a20b-78827751bd0f", "afcadf21-0612-4388-a6a0-8d6e37f010db" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9b2e3711-b965-4d61-a20b-78827751bd0f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "afcadf21-0612-4388-a6a0-8d6e37f010db");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "06178627-a0d4-44cc-8335-de572873424d", "a3f5a127-4fe7-4926-8f38-c3474bdaa342", "Admin", "ADMIN" },
                    { "26dd96ed-80a9-4566-be77-b9e1c6dc5854", "cda35c47-ee22-4e41-a6d1-8e652d193585", "Moderator", "MODERATOR" },
                    { "3e98e936-2069-474d-ae5e-8cdb5a4a4f60", "f1e77546-6811-4e06-a7b0-eba8171e1d36", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b8b5ad4e-87da-4daf-bc47-b8833be07d7b", 0, "d75f2ab2-de89-4fbb-b10c-fcd37857a7a8", "a@b.com", false, false, null, "A@B.COM", "A@B.COM", "AQAAAAEAACcQAAAAEEKg/+2dwmro8gYYy1TTkJwhTgTepF2oSrpdsPJ9nfxeQ8sRVJwrTC+lCaSgsZGbJA==", null, false, "037b1191-08a8-49b8-9eed-958d688d747f", false, "a@b.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "3e98e936-2069-474d-ae5e-8cdb5a4a4f60", "b8b5ad4e-87da-4daf-bc47-b8833be07d7b" });
        }
    }
}
