using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Pos.Model.Migrations
{
    /// <inheritdoc />
    public partial class MigrarModeloNegocio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Negocios",
                columns: table => new
                {
                    idNegocio = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ruc = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    razonSocial = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    telefono = table.Column<string>(type: "character varying(15)", unicode: false, maxLength: 15, nullable: false),
                    direccion = table.Column<string>(type: "character varying(500)", unicode: false, maxLength: 500, nullable: false),
                    propietario = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    descuento = table.Column<decimal>(type: "numeric(4,2)", unicode: false, precision: 4, scale: 2, nullable: false, defaultValue: 0m),
                    fechaRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "Now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Negocios", x => x.idNegocio);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Negocios");
        }
    }
}
