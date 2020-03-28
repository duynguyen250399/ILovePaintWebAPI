using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class MofidyOrderItemEntity2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CommentDate",
                table: "ProductComments",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 28, 15, 8, 39, 662, DateTimeKind.Local).AddTicks(9266),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 3, 27, 20, 32, 49, 945, DateTimeKind.Local).AddTicks(8728));

            migrationBuilder.AddColumn<string>(
                name: "ColorCode",
                table: "OrderItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColorName",
                table: "OrderItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorCode",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ColorName",
                table: "OrderItems");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CommentDate",
                table: "ProductComments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 27, 20, 32, 49, 945, DateTimeKind.Local).AddTicks(8728),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 3, 28, 15, 8, 39, 662, DateTimeKind.Local).AddTicks(9266));
        }
    }
}
