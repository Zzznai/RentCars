using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentCars.Migrations
{
    /// <inheritdoc />
    public partial class FixStoreTypeForRentalPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "RentalPricePerDay",
                table: "Cars",
                type: "decimal(2,2)",
                precision: 2,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,10)",
                oldPrecision: 2,
                oldScale: 10);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "RentalPricePerDay",
                table: "Cars",
                type: "decimal(2,10)",
                precision: 2,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,2)",
                oldPrecision: 2,
                oldScale: 2);
        }
    }
}
