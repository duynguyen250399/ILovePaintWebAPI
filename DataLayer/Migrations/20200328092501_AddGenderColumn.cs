using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddGenderColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CommentDate",
                table: "ProductComments",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 28, 16, 25, 1, 530, DateTimeKind.Local).AddTicks(301),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 3, 28, 15, 51, 37, 404, DateTimeKind.Local).AddTicks(4374));

            migrationBuilder.AddColumn<bool>(
                name: "Gender",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CommentDate",
                table: "ProductComments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 28, 15, 51, 37, 404, DateTimeKind.Local).AddTicks(4374),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 3, 28, 16, 25, 1, 530, DateTimeKind.Local).AddTicks(301));
        }
    }
}
