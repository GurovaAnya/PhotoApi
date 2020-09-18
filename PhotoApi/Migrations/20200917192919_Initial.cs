using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Patronymic = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Faces",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(nullable: false),
                    PhotoName = table.Column<string>(nullable: true),
                    PhotoHash = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Faces_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                columns: new[] { "Id", "PersonId", "PhotoHash", "PhotoName" },
                values: new object[] { -1, -1, 1924905495, "637360586020601329" });

            migrationBuilder.InsertData(
                table: "Faces",
                columns: new[] { "Id", "PersonId", "PhotoHash", "PhotoName" },
                values: new object[] { -2, -2, 1429759506, "637360612095630831" });

            migrationBuilder.CreateIndex(
                name: "IX_Faces_PersonId",
                table: "Faces",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Faces_PhotoHash",
                table: "Faces",
                column: "PhotoHash");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Faces");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
