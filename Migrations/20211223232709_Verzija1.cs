using Microsoft.EntityFrameworkCore.Migrations;

namespace ButiciBackend.Migrations
{
    public partial class Verzija1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artikal",
                columns: table => new
                {
                    Sifra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brend = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Model = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Cena = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artikal", x => x.Sifra);
                });

            migrationBuilder.CreateTable(
                name: "Butik",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Kontakttelefon = table.Column<int>(name: "Kontakt telefon", type: "int", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Butik", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Grad",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grad", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SpojVelicina",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Velicina = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ButikID = table.Column<int>(type: "int", nullable: true),
                    ArtikalSifra = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpojVelicina", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SpojVelicina_Artikal_ArtikalSifra",
                        column: x => x.ArtikalSifra,
                        principalTable: "Artikal",
                        principalColumn: "Sifra",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SpojVelicina_Butik_ButikID",
                        column: x => x.ButikID,
                        principalTable: "Butik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SpojAdresa",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adresa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ButikID = table.Column<int>(type: "int", nullable: true),
                    GradID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpojAdresa", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SpojAdresa_Butik_ButikID",
                        column: x => x.ButikID,
                        principalTable: "Butik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SpojAdresa_Grad_GradID",
                        column: x => x.GradID,
                        principalTable: "Grad",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpojAdresa_ButikID",
                table: "SpojAdresa",
                column: "ButikID");

            migrationBuilder.CreateIndex(
                name: "IX_SpojAdresa_GradID",
                table: "SpojAdresa",
                column: "GradID");

            migrationBuilder.CreateIndex(
                name: "IX_SpojVelicina_ArtikalSifra",
                table: "SpojVelicina",
                column: "ArtikalSifra");

            migrationBuilder.CreateIndex(
                name: "IX_SpojVelicina_ButikID",
                table: "SpojVelicina",
                column: "ButikID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpojAdresa");

            migrationBuilder.DropTable(
                name: "SpojVelicina");

            migrationBuilder.DropTable(
                name: "Grad");

            migrationBuilder.DropTable(
                name: "Artikal");

            migrationBuilder.DropTable(
                name: "Butik");
        }
    }
}
