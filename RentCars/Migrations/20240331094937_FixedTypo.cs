using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentCars.Migrations
{
    /// <inheritdoc />
    public partial class FixedTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Cars_carId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "carId",
                table: "Reservations",
                newName: "CarId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_carId",
                table: "Reservations",
                newName: "IX_Reservations_CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Cars_CarId",
                table: "Reservations",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Cars_CarId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "CarId",
                table: "Reservations",
                newName: "carId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_CarId",
                table: "Reservations",
                newName: "IX_Reservations_carId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Cars_carId",
                table: "Reservations",
                column: "carId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
