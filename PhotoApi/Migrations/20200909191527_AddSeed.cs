using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoApi.Migrations
{
    public partial class AddSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "FirstName", "LastName", "Patronymic" },
                values: new object[] { -1, "Anna", "Gurova", "Aleksandrovna" });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "FirstName", "LastName", "Patronymic" },
                values: new object[] { -2, "Petr", "Petrov", "Petrovich" });

            migrationBuilder.InsertData(
                table: "Faces",
                columns: new[] { "Id", "PersonId", "Photo" },
                values: new object[] { -1, -1, new byte[] { 1, 1, 1, 1, 1 } });

            migrationBuilder.InsertData(
                table: "Faces",
                columns: new[] { "Id", "PersonId", "Photo" },
                values: new object[] { -2, -2, new byte[] { 1, 0, 1, 0, 1, 1 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Faces",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Faces",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: -1);
        }
    }
}
