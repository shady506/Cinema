using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMoviesModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
