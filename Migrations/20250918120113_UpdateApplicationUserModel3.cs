using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Migrations
{
    /// <inheritdoc />
    public partial class UpdateApplicationUserModel3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bookings_AspNetUsers_UserId1",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_cartItems_AspNetUsers_UserId1",
                table: "cartItems");

            migrationBuilder.DropIndex(
                name: "IX_cartItems_UserId1",
                table: "cartItems");

            migrationBuilder.DropIndex(
                name: "IX_bookings_UserId1",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "cartItems");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "bookings");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "showTimes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "cartItems",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "bookings",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_cartItems_UserId",
                table: "cartItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_UserId",
                table: "bookings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_AspNetUsers_UserId",
                table: "bookings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cartItems_AspNetUsers_UserId",
                table: "cartItems",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bookings_AspNetUsers_UserId",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_cartItems_AspNetUsers_UserId",
                table: "cartItems");

            migrationBuilder.DropIndex(
                name: "IX_cartItems_UserId",
                table: "cartItems");

            migrationBuilder.DropIndex(
                name: "IX_bookings_UserId",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "showTimes");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "cartItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "cartItems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "bookings",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "bookings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_cartItems_UserId1",
                table: "cartItems",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_UserId1",
                table: "bookings",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_AspNetUsers_UserId1",
                table: "bookings",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_cartItems_AspNetUsers_UserId1",
                table: "cartItems",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
