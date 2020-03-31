using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class RemoveImageFromProvider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Providers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CommentDate",
                table: "ProductComments",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 31, 11, 3, 10, 555, DateTimeKind.Local).AddTicks(9317),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 3, 30, 11, 38, 29, 780, DateTimeKind.Local).AddTicks(4794));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Providers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CommentDate",
                table: "ProductComments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 30, 11, 38, 29, 780, DateTimeKind.Local).AddTicks(4794),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 3, 31, 11, 3, 10, 555, DateTimeKind.Local).AddTicks(9317));
        }
    }
}
