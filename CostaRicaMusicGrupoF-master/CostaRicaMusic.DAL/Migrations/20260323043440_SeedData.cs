using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostaRicaMusic.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Artistas",
                columns: new[] { "Id", "Descripcion", "ImagenUrl", "Nombre", "Pais" },
                values: new object[] { 1, "Artista urbano", null, "Bad Bunny", "Puerto Rico" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Correo", "FechaRegistro", "Nombre", "PasswordHash" },
                values: new object[] { 1, "admin@test.com", new DateTime(2026, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "1234" });

            migrationBuilder.InsertData(
                table: "Albumes",
                columns: new[] { "Id", "Anio", "ArtistaId", "PortadaUrl", "Titulo" },
                values: new object[] { 1, 2022, 1, null, "Un Verano Sin Ti" });

            migrationBuilder.InsertData(
                table: "Canciones",
                columns: new[] { "Id", "AlbumId", "ArtistaId", "AudioUrl", "Duracion", "Nombre" },
                values: new object[] { 1, 1, 1, "audio/titi.mp3", 240, "Tití Me Preguntó" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Canciones",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Albumes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Artistas",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
