using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Veterinaria.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 200, nullable: true),
                    telefono = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 9, nullable: true),
                    sexo = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 20, nullable: true),
                    direccion = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__clientes__3213E83F6F7331C7", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usuario = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    role = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__usuarios__3213E83FAA08AA7C", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "veterinarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    telefono = table.Column<string>(type: "varchar(9)", unicode: false, maxLength: 9, nullable: true),
                    sexo = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    direccion = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__veterina__3213E83F7E60B2F3", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mascotas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    tipo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    sexo = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    fecha_nacimiento = table.Column<DateTime>(type: "date", nullable: true),
                    cliente_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__mascotas__3213E83F6AA4138E", x => x.id);
                    table.ForeignKey(
                        name: "pk_cliente_mascota",
                        column: x => x.cliente_id,
                        principalTable: "clientes",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "citas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fecha = table.Column<DateTime>(type: "datetime", nullable: true),
                    Hora = table.Column<TimeSpan>(type: "time", nullable: false),
                    mascota_id = table.Column<int>(type: "int", nullable: true),
                    veterinario_id = table.Column<int>(type: "int", nullable: true),
                    estado = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__citas__3213E83F3DDEA8CE", x => x.id);
                    table.ForeignKey(
                        name: "fk_citas_mascota",
                        column: x => x.mascota_id,
                        principalTable: "mascotas",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_citas_veterinario",
                        column: x => x.veterinario_id,
                        principalTable: "veterinarios",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "expediente",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mascota_id = table.Column<int>(type: "int", nullable: true),
                    cita_id = table.Column<int>(type: "int", nullable: true),
                    diagnostico = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    recetas = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__expedien__3213E83F349C854B", x => x.id);
                    table.ForeignKey(
                        name: "fk_exp_cita",
                        column: x => x.cita_id,
                        principalTable: "citas",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_exp_mascota",
                        column: x => x.mascota_id,
                        principalTable: "mascotas",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_citas_mascota_id",
                table: "citas",
                column: "mascota_id");

            migrationBuilder.CreateIndex(
                name: "IX_citas_veterinario_id",
                table: "citas",
                column: "veterinario_id");

            migrationBuilder.CreateIndex(
                name: "IX_expediente_cita_id",
                table: "expediente",
                column: "cita_id");

            migrationBuilder.CreateIndex(
                name: "IX_expediente_mascota_id",
                table: "expediente",
                column: "mascota_id");

            migrationBuilder.CreateIndex(
                name: "IX_mascotas_cliente_id",
                table: "mascotas",
                column: "cliente_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "expediente");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "citas");

            migrationBuilder.DropTable(
                name: "mascotas");

            migrationBuilder.DropTable(
                name: "veterinarios");

            migrationBuilder.DropTable(
                name: "clientes");
        }
    }
}
