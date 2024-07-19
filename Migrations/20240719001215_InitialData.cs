using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace proyectoef.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Tarea",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Categoria",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "CategoriaId", "Descripcion", "Nombre", "Peso" },
                values: new object[,]
                {
                    { new Guid("c50cfa79-9551-48d4-8b52-a15b9830d1b8"), null, "Actividades personales", 50 },
                    { new Guid("f05ac2fb-4b4e-4b38-9be9-c0c797374f75"), null, "Actividades pendientes", 20 }
                });

            migrationBuilder.InsertData(
                table: "Tarea",
                columns: new[] { "TareaId", "CategoriaId", "Descripcion", "FechaCreacion", "PrioridadTarea", "Titulo" },
                values: new object[,]
                {
                    { new Guid("8d5c1a5b-d243-4fa3-8641-655b593cd081"), new Guid("f05ac2fb-4b4e-4b38-9be9-c0c797374f75"), null, new DateTime(2024, 7, 18, 19, 12, 14, 851, DateTimeKind.Local).AddTicks(9699), 1, "Pago de Servicios publicos" },
                    { new Guid("b0a1dbe0-9532-4583-bd7b-e53ae381ac5f"), new Guid("c50cfa79-9551-48d4-8b52-a15b9830d1b8"), null, new DateTime(2024, 7, 18, 19, 12, 14, 851, DateTimeKind.Local).AddTicks(9684), 0, "Terminar de ver peliculas en Netflix" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tarea",
                keyColumn: "TareaId",
                keyValue: new Guid("8d5c1a5b-d243-4fa3-8641-655b593cd081"));

            migrationBuilder.DeleteData(
                table: "Tarea",
                keyColumn: "TareaId",
                keyValue: new Guid("b0a1dbe0-9532-4583-bd7b-e53ae381ac5f"));

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaId",
                keyValue: new Guid("c50cfa79-9551-48d4-8b52-a15b9830d1b8"));

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaId",
                keyValue: new Guid("f05ac2fb-4b4e-4b38-9be9-c0c797374f75"));

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Tarea",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Categoria",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
