using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Pos.Model.Migrations
{
    /// <inheritdoc />
    public partial class MigrarModeloDetalleVenta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DetallesVenta",
                columns: table => new
                {
                    idDetalleVenta = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idVenta = table.Column<int>(type: "integer", nullable: false),
                    idProducto = table.Column<int>(type: "integer", nullable: false),
                    nombreProducto = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    precio = table.Column<decimal>(type: "numeric(18,2)", unicode: false, precision: 18, scale: 2, nullable: false),
                    cantidad = table.Column<int>(type: "integer", unicode: false, nullable: false, defaultValue: 1),
                    descuento = table.Column<decimal>(type: "numeric", unicode: false, nullable: false, defaultValue: 0m),
                    total = table.Column<decimal>(type: "numeric", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesVenta", x => x.idDetalleVenta);
                    table.ForeignKey(
                        name: "FK_DetallesVenta_Productos_idProducto",
                        column: x => x.idProducto,
                        principalTable: "Productos",
                        principalColumn: "idProducto",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetallesVenta_Ventas_idVenta",
                        column: x => x.idVenta,
                        principalTable: "Ventas",
                        principalColumn: "idVenta",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_factura",
                table: "Ventas",
                column: "factura",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetallesVenta_idProducto",
                table: "DetallesVenta",
                column: "idProducto");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesVenta_idVenta",
                table: "DetallesVenta",
                column: "idVenta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallesVenta");

            migrationBuilder.DropIndex(
                name: "IX_Ventas_factura",
                table: "Ventas");
        }
    }
}
