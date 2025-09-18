using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cinema.Migrations
{
    /// <inheritdoc />
    public partial class SeedSeats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_seats_Movies_MoviesId",
                table: "seats");

            migrationBuilder.DropIndex(
                name: "IX_seats_MoviesId",
                table: "seats");

            migrationBuilder.DropColumn(
                name: "MoviesId",
                table: "seats");

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "seats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "seats",
                columns: new[] { "Id", "IsBooked", "MovieId", "SeatNumber", "ShowTimeId" },
                values: new object[,]
                {
                    { 1, false, 1, "A1", 0 },
                    { 2, false, 1, "A2", 0 },
                    { 3, false, 1, "A3", 0 },
                    { 4, false, 1, "A4", 0 },
                    { 5, false, 1, "A5", 0 },
                    { 6, false, 1, "A6", 0 },
                    { 7, false, 1, "A7", 0 },
                    { 8, false, 1, "A8", 0 },
                    { 9, false, 1, "A9", 0 },
                    { 10, false, 1, "A10", 0 },
                    { 11, false, 2, "A1", 0 },
                    { 12, false, 2, "A2", 0 },
                    { 13, false, 2, "A3", 0 },
                    { 14, false, 2, "A4", 0 },
                    { 15, false, 2, "A5", 0 },
                    { 16, false, 2, "A6", 0 },
                    { 17, false, 2, "A7", 0 },
                    { 18, false, 2, "A8", 0 },
                    { 19, false, 2, "A9", 0 },
                    { 20, false, 2, "A10", 0 },
                    { 21, false, 3, "A1", 0 },
                    { 22, false, 3, "A2", 0 },
                    { 23, false, 3, "A3", 0 },
                    { 24, false, 3, "A4", 0 },
                    { 25, false, 3, "A5", 0 },
                    { 26, false, 3, "A6", 0 },
                    { 27, false, 3, "A7", 0 },
                    { 28, false, 3, "A8", 0 },
                    { 29, false, 3, "A9", 0 },
                    { 30, false, 3, "A10", 0 },
                    { 31, false, 4, "A1", 0 },
                    { 32, false, 4, "A2", 0 },
                    { 33, false, 4, "A3", 0 },
                    { 34, false, 4, "A4", 0 },
                    { 35, false, 4, "A5", 0 },
                    { 36, false, 4, "A6", 0 },
                    { 37, false, 4, "A7", 0 },
                    { 38, false, 4, "A8", 0 },
                    { 39, false, 4, "A9", 0 },
                    { 40, false, 4, "A10", 0 },
                    { 41, false, 5, "A1", 0 },
                    { 42, false, 5, "A2", 0 },
                    { 43, false, 5, "A3", 0 },
                    { 44, false, 5, "A4", 0 },
                    { 45, false, 5, "A5", 0 },
                    { 46, false, 5, "A6", 0 },
                    { 47, false, 5, "A7", 0 },
                    { 48, false, 5, "A8", 0 },
                    { 49, false, 5, "A9", 0 },
                    { 50, false, 5, "A10", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_seats_MovieId",
                table: "seats",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_seats_Movies_MovieId",
                table: "seats",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_seats_Movies_MovieId",
                table: "seats");

            migrationBuilder.DropIndex(
                name: "IX_seats_MovieId",
                table: "seats");

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "seats");

            migrationBuilder.AddColumn<int>(
                name: "MoviesId",
                table: "seats",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_seats_MoviesId",
                table: "seats",
                column: "MoviesId");

            migrationBuilder.AddForeignKey(
                name: "FK_seats_Movies_MoviesId",
                table: "seats",
                column: "MoviesId",
                principalTable: "Movies",
                principalColumn: "Id");
        }
    }
}
