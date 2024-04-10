using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Proyecto_API.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTabla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Apellido", "Correo", "FechaCreacion", "ImagenUrl", "Nombre", "Numero" },
                values: new object[,]
                {
                    { 1, "Prueba1", "cortesantonio822@gmail.com", new DateTime(2024, 4, 10, 11, 25, 59, 952, DateTimeKind.Local).AddTicks(2180), "", "Prueba", "4494597406" },
                    { 2, "Prueba2", "cortesantonio820@gmail.com", new DateTime(2024, 4, 10, 11, 25, 59, 952, DateTimeKind.Local).AddTicks(2191), "", "Prueba2", "4494597405" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
