using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Pos.Model.Migrations
{
    /// <inheritdoc />
    public partial class MigrarModeloUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FechaRegistro",
                table: "Roles",
                newName: "fechaRegistro");

            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "Roles",
                newName: "estado");

            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "Roles",
                newName: "descripcion");

            migrationBuilder.RenameColumn(
                name: "IdRol",
                table: "Roles",
                newName: "idRol");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_Descripcion",
                table: "Roles",
                newName: "IX_Roles_descripcion");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    idUsuario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombres = table.Column<string>(type: "character varying(35)", unicode: false, maxLength: 35, nullable: false),
                    apellidos = table.Column<string>(type: "character varying(35)", unicode: false, maxLength: 35, nullable: false),
                    idRol = table.Column<int>(type: "integer", nullable: false),
                    Telefono = table.Column<string>(type: "character varying(15)", unicode: false, maxLength: 15, nullable: false),
                    estado = table.Column<string>(type: "character varying(8)", unicode: false, maxLength: 8, nullable: false),
                    fechaRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "Now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.idUsuario);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_idRol",
                        column: x => x.idRol,
                        principalTable: "Roles",
                        principalColumn: "idRol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_idRol",
                table: "Usuarios",
                column: "idRol");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Telefono",
                table: "Usuarios",
                column: "Telefono",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "fechaRegistro",
                table: "Roles",
                newName: "FechaRegistro");

            migrationBuilder.RenameColumn(
                name: "estado",
                table: "Roles",
                newName: "Estado");

            migrationBuilder.RenameColumn(
                name: "descripcion",
                table: "Roles",
                newName: "Descripcion");

            migrationBuilder.RenameColumn(
                name: "idRol",
                table: "Roles",
                newName: "IdRol");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_descripcion",
                table: "Roles",
                newName: "IX_Roles_Descripcion");
        }
    }
}
