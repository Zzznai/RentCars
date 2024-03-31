using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentCars.Migrations
{
    /// <inheritdoc />
    public partial class MinorChangesInReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "RentalSum",
                table: "Reservations",
                type: "decimal(2,2)",
                precision: 2,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentalSum",
                table: "Reservations");
        }
    }
}
