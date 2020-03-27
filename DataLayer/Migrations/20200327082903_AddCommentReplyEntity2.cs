using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddCommentReplyEntity2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentReplies_ProductComments_ProductCommentID",
                table: "CommentReplies");

            migrationBuilder.DropColumn(
                name: "CommentID",
                table: "CommentReplies");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CommentDate",
                table: "ProductComments",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 27, 15, 29, 2, 728, DateTimeKind.Local).AddTicks(4065),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 3, 27, 14, 37, 50, 208, DateTimeKind.Local).AddTicks(8789));

            migrationBuilder.AlterColumn<int>(
                name: "ProductCommentID",
                table: "CommentReplies",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "CommentReplies",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentReplies_ProductComments_ProductCommentID",
                table: "CommentReplies",
                column: "ProductCommentID",
                principalTable: "ProductComments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentReplies_ProductComments_ProductCommentID",
                table: "CommentReplies");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "CommentReplies");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CommentDate",
                table: "ProductComments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 27, 14, 37, 50, 208, DateTimeKind.Local).AddTicks(8789),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 3, 27, 15, 29, 2, 728, DateTimeKind.Local).AddTicks(4065));

            migrationBuilder.AlterColumn<int>(
                name: "ProductCommentID",
                table: "CommentReplies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "CommentID",
                table: "CommentReplies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentReplies_ProductComments_ProductCommentID",
                table: "CommentReplies",
                column: "ProductCommentID",
                principalTable: "ProductComments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
