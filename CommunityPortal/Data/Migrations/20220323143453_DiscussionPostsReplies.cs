using Microsoft.EntityFrameworkCore.Migrations;

namespace CommunityPortal.Data.Migrations
{
    public partial class DiscussionPostsReplies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiscussionPostReplyDiscussionPostId",
                table: "DiscussionPosts",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_DiscussionPosts_DiscussionPostReplyDiscussionPostId",
                table: "DiscussionPosts",
                column: "DiscussionPostReplyDiscussionPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscussionPosts_DiscussionPosts_DiscussionPostReplyDiscussionPostId",
                table: "DiscussionPosts",
                column: "DiscussionPostReplyDiscussionPostId",
                principalTable: "DiscussionPosts",
                principalColumn: "DiscussionPostId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscussionPosts_DiscussionPosts_DiscussionPostReplyDiscussionPostId",
                table: "DiscussionPosts");

            migrationBuilder.DropIndex(
                name: "IX_DiscussionPosts_DiscussionPostReplyDiscussionPostId",
                table: "DiscussionPosts");

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

            migrationBuilder.DropColumn(
                name: "DiscussionPostReplyDiscussionPostId",
                table: "DiscussionPosts");
        }
    }
}
