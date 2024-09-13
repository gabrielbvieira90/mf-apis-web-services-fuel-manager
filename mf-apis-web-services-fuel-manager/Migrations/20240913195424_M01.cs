using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mf_apis_web_services_fuel_manager.Migrations
{
    /// <inheritdoc />
    public partial class M01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Consumos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Consumos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
