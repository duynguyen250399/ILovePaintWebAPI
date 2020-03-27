using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddCommentReplyEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CommentDate",
                table: "ProductComments",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 27, 14, 37, 50, 208, DateTimeKind.Local).AddTicks(8789),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 3, 27, 10, 36, 13, 628, DateTimeKind.Local).AddTicks(4909));

            migrationBuilder.CreateTable(
                name: "CommentReplies",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentID = table.Column<int>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    ReplyDate = table.Column<DateTime>(nullable: false),
                    ProductCommentID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentReplies", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CommentReplies_ProductComments_ProductCommentID",
                        column: x => x.ProductCommentID,
                        principalTable: "ProductComments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommentReplies_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentReplies_ProductCommentID",
                table: "CommentReplies",
                column: "ProductCommentID");

            migrationBuilder.CreateIndex(
                name: "IX_CommentReplies_UserID",
                table: "CommentReplies",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentReplies");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CommentDate",
                table: "ProductComments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 27, 10, 36, 13, 628, DateTimeKind.Local).AddTicks(4909),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 3, 27, 14, 37, 50, 208, DateTimeKind.Local).AddTicks(8789));
        }
    }
}
