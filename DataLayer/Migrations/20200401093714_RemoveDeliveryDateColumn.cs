using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DataLayer.Migrations
{
    public partial class RemoveDeliveryDateColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "Orders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CommentDate",
                table: "ProductComments",
                nullable: false,
                defaultValue: new DateTime(2020, 4, 1, 16, 37, 14, 315, DateTimeKind.Local).AddTicks(45),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 3, 31, 11, 3, 10, 555, DateTimeKind.Local).AddTicks(9317));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CommentDate",
                table: "ProductComments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 31, 11, 3, 10, 555, DateTimeKind.Local).AddTicks(9317),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 4, 1, 16, 37, 14, 315, DateTimeKind.Local).AddTicks(45));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDate",
                table: "Orders",
                type: "datetime2",
                nullable: true);
        }
    }
}
