using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibreriaCesar.Migrations
{
    /// <inheritdoc />
    public partial class AgregarStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Stock",
                table: "Libros",
                newName: "StockTotal");

            migrationBuilder.AddColumn<int>(
                name: "StockDisponible",
                table: "Libros",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockDisponible",
                table: "Libros");

            migrationBuilder.RenameColumn(
                name: "StockTotal",
                table: "Libros",
                newName: "Stock");
        }
    }
}
