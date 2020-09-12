using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoApi.Migrations
{
    public partial class ChangePhotoType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Photo",
                table: "Faces",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(900)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Faces",
                keyColumn: "Id",
                keyValue: -2,
                column: "Photo",
                value: "AQEBAQE=");

            migrationBuilder.UpdateData(
                table: "Faces",
                keyColumn: "Id",
                keyValue: -1,
                column: "Photo",
                value: "AQABAAEB");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Photo",
                table: "Faces",
                type: "varbinary(900)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Faces",
                keyColumn: "Id",
                keyValue: -2,
                column: "Photo",
                value: new byte[] { 1, 0, 1, 0, 1, 1 });

            migrationBuilder.UpdateData(
                table: "Faces",
                keyColumn: "Id",
                keyValue: -1,
                column: "Photo",
                value: new byte[] { 1, 1, 1, 1, 1 });
        }
    }
}
