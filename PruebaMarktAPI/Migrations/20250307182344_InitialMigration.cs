using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PruebaMarktAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "categorias",
                columns: new[] { "idcategorias", "nombre" },
                values: new object[,]
                {
                    { 1, "Smartphones" },
                    { 2, "Portátiles" },
                    { 3, "televisores" }
                });

            migrationBuilder.InsertData(
                table: "productos",
                columns: new[] { "idproductos", "descripcion", "idcategoria", "nombre", "valoracion" },
                values: new object[,]
                {
                    { 1, "Movilazo", 1, "iPhone13", 4m },
                    { 2, "Movilazo", 1, "iPhone14", 4m },
                    { 3, "Tele 32º.....", 3, "HiSense", 2m },
                    { 4, "12GB RAM 256GB", 2, "Mac Book", 5m }
                });

            migrationBuilder.InsertData(
                table: "ventas",
                columns: new[] { "idventas", "cantidad", "idproducto", "nombreuser" },
                values: new object[,]
                {
                    { 1, 8, 1, "paco@gmail.com" },
                    { 2, 2, 4, "rosa@gmail.com" },
                    { 3, 10, 3, "empresasTV@gmail.com" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
