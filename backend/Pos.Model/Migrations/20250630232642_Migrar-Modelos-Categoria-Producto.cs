using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Pos.Model.Migrations
{
    /// <inheritdoc />
    public partial class MigrarModelosCategoriaProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    idProducto = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codigoBarra = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    descripcion = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    idCategoria = table.Column<int>(type: "integer", nullable: false),
                    precioVenta = table.Column<decimal>(type: "numeric(18,2)", unicode: false, precision: 18, scale: 2, nullable: false),
                    stock = table.Column<int>(type: "integer", unicode: false, nullable: false, defaultValue: 0),
                    stockMinimo = table.Column<int>(type: "integer", unicode: false, nullable: false, defaultValue: 5),
                    estado = table.Column<string>(type: "character varying(8)", unicode: false, maxLength: 8, nullable: false),
                    fechaRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "Now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.idProducto);
                    table.ForeignKey(
                        name: "FK_Productos_Categorias_idCategoria",
                        column: x => x.idCategoria,
                        principalTable: "Categorias",
                        principalColumn: "idCategoria",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_codigoBarra",
                table: "Productos",
                column: "codigoBarra",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_descripcion",
                table: "Productos",
                column: "descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_idCategoria",
                table: "Productos",
                column: "idCategoria");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Productos");
        }
    }
}
