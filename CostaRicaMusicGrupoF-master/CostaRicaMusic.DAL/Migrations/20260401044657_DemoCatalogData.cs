using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CostaRicaMusic.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DemoCatalogData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Albumes",
                keyColumn: "Id",
                keyValue: 1,
                column: "PortadaUrl",
                value: "images/albums/un-verano-sin-ti.jpg");

            migrationBuilder.UpdateData(
                table: "Artistas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Descripcion", "ImagenUrl" },
                values: new object[] { "Artista urbano y uno de los exponentes más conocidos del reggaetón y trap latino.", "images/artists/badbunny.jpg" });

            migrationBuilder.InsertData(
                table: "Artistas",
                columns: new[] { "Id", "Descripcion", "ImagenUrl", "Nombre", "Pais" },
                values: new object[,]
                {
                    { 2, "Cantante británica reconocida por su potente voz y baladas pop.", "images/artists/adele.jpg", "Adele", "Reino Unido" },
                    { 3, "Banda británica de pop rock alternativo con reconocimiento internacional.", "images/artists/coldplay.jpg", "Coldplay", "Reino Unido" },
                    { 4, "Cantante colombiana destacada en el género urbano y pop latino.", "images/artists/karolg.jpg", "Karol G", "Colombia" },
                    { 5, "Cantante y compositor estadounidense de pop, funk y R&B.", "images/artists/brunomars.jpg", "Bruno Mars", "Estados Unidos" }
                });

            migrationBuilder.UpdateData(
                table: "Canciones",
                keyColumn: "Id",
                keyValue: 1,
                column: "AudioUrl",
                value: "audio/titi-me-pregunto.mp3");

            migrationBuilder.InsertData(
                table: "Canciones",
                columns: new[] { "Id", "AlbumId", "ArtistaId", "AudioUrl", "Duracion", "Nombre" },
                values: new object[] { 2, 1, 1, "audio/ojitos-lindos.mp3", 258, "Ojitos Lindos" });

            migrationBuilder.InsertData(
                table: "Albumes",
                columns: new[] { "Id", "Anio", "ArtistaId", "PortadaUrl", "Titulo" },
                values: new object[,]
                {
                    { 2, 2015, 2, "images/albums/25.jpg", "25" },
                    { 3, 2015, 3, "images/albums/a-head-full-of-dreams.jpg", "A Head Full of Dreams" },
                    { 4, 2023, 4, "images/albums/manana-sera-bonito.jpg", "Mañana Será Bonito" },
                    { 5, 2016, 5, "images/albums/24k-magic.jpg", "24K Magic" }
                });

            migrationBuilder.InsertData(
                table: "Canciones",
                columns: new[] { "Id", "AlbumId", "ArtistaId", "AudioUrl", "Duracion", "Nombre" },
                values: new object[,]
                {
                    { 3, 2, 2, "audio/hello.mp3", 295, "Hello" },
                    { 4, 2, 2, "audio/send-my-love.mp3", 223, "Send My Love" },
                    { 5, 3, 3, "audio/hymn-for-the-weekend.mp3", 258, "Hymn for the Weekend" },
                    { 6, 3, 3, "audio/adventure-of-a-lifetime.mp3", 263, "Adventure of a Lifetime" },
                    { 7, 4, 4, "audio/provenza.mp3", 210, "Provenza" },
                    { 8, 4, 4, "audio/cairo.mp3", 200, "Cairo" },
                    { 9, 5, 5, "audio/24k-magic.mp3", 227, "24K Magic" },
                    { 10, 5, 5, "audio/thats-what-i-like.mp3", 206, "That's What I Like" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Canciones",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Canciones",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Canciones",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Canciones",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Canciones",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Canciones",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Canciones",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Canciones",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Canciones",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Albumes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Albumes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Albumes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Albumes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Artistas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Artistas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Artistas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Artistas",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Albumes",
                keyColumn: "Id",
                keyValue: 1,
                column: "PortadaUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Artistas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Descripcion", "ImagenUrl" },
                values: new object[] { "Artista urbano", null });

            migrationBuilder.UpdateData(
                table: "Canciones",
                keyColumn: "Id",
                keyValue: 1,
                column: "AudioUrl",
                value: "audio/titi.mp3");
        }
    }
}
