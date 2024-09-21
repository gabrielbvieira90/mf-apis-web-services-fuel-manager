using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mf_apis_web_services_fuel_manager.Migrations
{
    /// <inheritdoc />
    public partial class M04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VeiculoUsuario_Usuarios_UsuairoId",
                table: "VeiculoUsuario");

            migrationBuilder.RenameColumn(
                name: "UsuairoId",
                table: "VeiculoUsuario",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_VeiculoUsuario_UsuairoId",
                table: "VeiculoUsuario",
                newName: "IX_VeiculoUsuario_UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_VeiculoUsuario_Usuarios_UsuarioId",
                table: "VeiculoUsuario",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VeiculoUsuario_Usuarios_UsuarioId",
                table: "VeiculoUsuario");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "VeiculoUsuario",
                newName: "UsuairoId");

            migrationBuilder.RenameIndex(
                name: "IX_VeiculoUsuario_UsuarioId",
                table: "VeiculoUsuario",
                newName: "IX_VeiculoUsuario_UsuairoId");

            migrationBuilder.AddForeignKey(
                name: "FK_VeiculoUsuario_Usuarios_UsuairoId",
                table: "VeiculoUsuario",
                column: "UsuairoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
