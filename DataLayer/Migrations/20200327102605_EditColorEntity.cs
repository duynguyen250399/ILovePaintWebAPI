using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class EditColorEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Color");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CommentDate",
                table: "ProductComments",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 27, 17, 26, 5, 111, DateTimeKind.Local).AddTicks(6245),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 3, 27, 15, 29, 2, 728, DateTimeKind.Local).AddTicks(4065));

            migrationBuilder.AddColumn<string>(
                name: "ColorCode",
                table: "Color",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorCode",
                table: "Color");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CommentDate",
                table: "ProductComments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 27, 15, 29, 2, 728, DateTimeKind.Local).AddTicks(4065),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 3, 27, 17, 26, 5, 111, DateTimeKind.Local).AddTicks(6245));

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Color",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
