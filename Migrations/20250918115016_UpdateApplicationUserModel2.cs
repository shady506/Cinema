using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Migrations
{
    /// <inheritdoc />
    public partial class UpdateApplicationUserModel2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_AspNetUsers_UserId1",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Seat_SeatId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_ShowTime_ShowTimeId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_AspNetUsers_UserId1",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Seat_SeatId",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_ShowTime_ShowTimeId",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Seat_ShowTime_ShowTimeId",
                table: "Seat");

            migrationBuilder.DropForeignKey(
                name: "FK_ShowTime_Movies_MovieId",
                table: "ShowTime");

            migrationBuilder.DropTable(
                name: "UserOTPs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShowTime",
                table: "ShowTime");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seat",
                table: "Seat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItem",
                table: "CartItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Booking",
                table: "Booking");

            migrationBuilder.RenameTable(
                name: "ShowTime",
                newName: "showTimes");

            migrationBuilder.RenameTable(
                name: "Seat",
                newName: "seats");

            migrationBuilder.RenameTable(
                name: "CartItem",
                newName: "cartItems");

            migrationBuilder.RenameTable(
                name: "Booking",
                newName: "bookings");

            migrationBuilder.RenameColumn(
                name: "Streat",
                table: "AspNetUsers",
                newName: "Street");

            migrationBuilder.RenameIndex(
                name: "IX_ShowTime_MovieId",
                table: "showTimes",
                newName: "IX_showTimes_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_Seat_ShowTimeId",
                table: "seats",
                newName: "IX_seats_ShowTimeId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_UserId1",
                table: "cartItems",
                newName: "IX_cartItems_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_ShowTimeId",
                table: "cartItems",
                newName: "IX_cartItems_ShowTimeId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_SeatId",
                table: "cartItems",
                newName: "IX_cartItems_SeatId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_UserId1",
                table: "bookings",
                newName: "IX_bookings_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_ShowTimeId",
                table: "bookings",
                newName: "IX_bookings_ShowTimeId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_SeatId",
                table: "bookings",
                newName: "IX_bookings_SeatId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId1",
                table: "cartItems",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId1",
                table: "bookings",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_showTimes",
                table: "showTimes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_seats",
                table: "seats",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cartItems",
                table: "cartItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_bookings",
                table: "bookings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_AspNetUsers_UserId1",
                table: "bookings",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_seats_SeatId",
                table: "bookings",
                column: "SeatId",
                principalTable: "seats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_showTimes_ShowTimeId",
                table: "bookings",
                column: "ShowTimeId",
                principalTable: "showTimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cartItems_AspNetUsers_UserId1",
                table: "cartItems",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_cartItems_seats_SeatId",
                table: "cartItems",
                column: "SeatId",
                principalTable: "seats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cartItems_showTimes_ShowTimeId",
                table: "cartItems",
                column: "ShowTimeId",
                principalTable: "showTimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_seats_showTimes_ShowTimeId",
                table: "seats",
                column: "ShowTimeId",
                principalTable: "showTimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_showTimes_Movies_MovieId",
                table: "showTimes",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bookings_AspNetUsers_UserId1",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_bookings_seats_SeatId",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_bookings_showTimes_ShowTimeId",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_cartItems_AspNetUsers_UserId1",
                table: "cartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_cartItems_seats_SeatId",
                table: "cartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_cartItems_showTimes_ShowTimeId",
                table: "cartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_seats_showTimes_ShowTimeId",
                table: "seats");

            migrationBuilder.DropForeignKey(
                name: "FK_showTimes_Movies_MovieId",
                table: "showTimes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_showTimes",
                table: "showTimes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_seats",
                table: "seats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cartItems",
                table: "cartItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_bookings",
                table: "bookings");

            migrationBuilder.RenameTable(
                name: "showTimes",
                newName: "ShowTime");

            migrationBuilder.RenameTable(
                name: "seats",
                newName: "Seat");

            migrationBuilder.RenameTable(
                name: "cartItems",
                newName: "CartItem");

            migrationBuilder.RenameTable(
                name: "bookings",
                newName: "Booking");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "AspNetUsers",
                newName: "Streat");

            migrationBuilder.RenameIndex(
                name: "IX_showTimes_MovieId",
                table: "ShowTime",
                newName: "IX_ShowTime_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_seats_ShowTimeId",
                table: "Seat",
                newName: "IX_Seat_ShowTimeId");

            migrationBuilder.RenameIndex(
                name: "IX_cartItems_UserId1",
                table: "CartItem",
                newName: "IX_CartItem_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_cartItems_ShowTimeId",
                table: "CartItem",
                newName: "IX_CartItem_ShowTimeId");

            migrationBuilder.RenameIndex(
                name: "IX_cartItems_SeatId",
                table: "CartItem",
                newName: "IX_CartItem_SeatId");

            migrationBuilder.RenameIndex(
                name: "IX_bookings_UserId1",
                table: "Booking",
                newName: "IX_Booking_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_bookings_ShowTimeId",
                table: "Booking",
                newName: "IX_Booking_ShowTimeId");

            migrationBuilder.RenameIndex(
                name: "IX_bookings_SeatId",
                table: "Booking",
                newName: "IX_Booking_SeatId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "UserId1",
                table: "CartItem",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId1",
                table: "Booking",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShowTime",
                table: "ShowTime",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seat",
                table: "Seat",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItem",
                table: "CartItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Booking",
                table: "Booking",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserOTPs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OTPNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOTPs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOTPs_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserOTPs_ApplicationUserId",
                table: "UserOTPs",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_AspNetUsers_UserId1",
                table: "Booking",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Seat_SeatId",
                table: "Booking",
                column: "SeatId",
                principalTable: "Seat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_ShowTime_ShowTimeId",
                table: "Booking",
                column: "ShowTimeId",
                principalTable: "ShowTime",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_AspNetUsers_UserId1",
                table: "CartItem",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Seat_SeatId",
                table: "CartItem",
                column: "SeatId",
                principalTable: "Seat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_ShowTime_ShowTimeId",
                table: "CartItem",
                column: "ShowTimeId",
                principalTable: "ShowTime",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_ShowTime_ShowTimeId",
                table: "Seat",
                column: "ShowTimeId",
                principalTable: "ShowTime",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShowTime_Movies_MovieId",
                table: "ShowTime",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
