using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiSocialWeb.Migrations
{
    /// <inheritdoc />
    public partial class test4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostsId",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "CommentsId",
                table: "Posts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CommentsId",
                table: "Posts",
                column: "CommentsId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Comments_CommentsId",
                table: "Posts",
                column: "CommentsId",
                principalTable: "Comments",
                principalColumn: "CommentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Comments_CommentsId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_CommentsId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CommentsId",
                table: "Posts");

            migrationBuilder.AddColumn<int>(
                name: "PostsId",
                table: "Comments",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
