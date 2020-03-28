using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class RemoveShipperEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Shippers_ShipperID",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Shippers");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ShipperID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShipperID",
                table: "Orders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CommentDate",
                table: "ProductComments",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 28, 15, 51, 37, 404, DateTimeKind.Local).AddTicks(4374),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 3, 28, 15, 8, 39, 662, DateTimeKind.Local).AddTicks(9266));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CommentDate",
                table: "ProductComments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 28, 15, 8, 39, 662, DateTimeKind.Local).AddTicks(9266),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 3, 28, 15, 51, 37, 404, DateTimeKind.Local).AddTicks(4374));

            migrationBuilder.AddColumn<int>(
                name: "ShipperID",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Shippers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHashed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordSalted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<double>(type: "float", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shippers", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShipperID",
                table: "Orders",
                column: "ShipperID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Shippers_ShipperID",
                table: "Orders",
                column: "ShipperID",
                principalTable: "Shippers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
