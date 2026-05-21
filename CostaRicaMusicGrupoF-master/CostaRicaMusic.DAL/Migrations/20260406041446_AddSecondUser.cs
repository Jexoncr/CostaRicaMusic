using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostaRicaMusic.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddSecondUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Correo", "FechaRegistro", "Nombre", "PasswordHash" },
                values: new object[] { 2, "test@test.com", new DateTime(2026, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Usuario Test", "1234" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
