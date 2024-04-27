using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManufacturingApp.Migrations
{
    /// <inheritdoc />
    public partial class RawMaterialSupplierPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "SupplierRawMaterials",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "SupplierRawMaterials");
        }
    }
}
