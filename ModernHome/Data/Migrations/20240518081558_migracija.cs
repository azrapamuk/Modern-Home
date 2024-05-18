using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModernHome.Data.Migrations
{
    /// <inheritdoc />
    public partial class migracija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dimenzije",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    visina = table.Column<double>(type: "float", nullable: false),
                    sirina = table.Column<double>(type: "float", nullable: false),
                    duzina = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dimenzije", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    brojTelefona = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Korisnik_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Artikal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tip = table.Column<int>(type: "int", nullable: false),
                    boja = table.Column<int>(type: "int", nullable: false),
                    kolicina = table.Column<int>(type: "int", nullable: false),
                    cijena = table.Column<double>(type: "float", nullable: false),
                    Iddimenzije = table.Column<int>(type: "int", nullable: false),
                    dimenzijeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artikal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artikal_Dimenzije_dimenzijeId",
                        column: x => x.dimenzijeId,
                        principalTable: "Dimenzije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kartica",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    brojKartice = table.Column<int>(type: "int", nullable: false),
                    CVV = table.Column<int>(type: "int", nullable: false),
                    datumIsteka = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Idkorisnik = table.Column<int>(type: "int", nullable: false),
                    korisnikId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kartica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kartica_Korisnik_korisnikId",
                        column: x => x.korisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Korpa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ukupanIznos = table.Column<double>(type: "float", nullable: false),
                    Idkorisnik = table.Column<int>(type: "int", nullable: false),
                    korisnikId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korpa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Korpa_Korisnik_korisnikId",
                        column: x => x.korisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ocjena",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ocjena = table.Column<int>(type: "int", nullable: false),
                    komentar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Idkorisnik = table.Column<int>(type: "int", nullable: false),
                    korisnikId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocjena", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ocjena_Korisnik_korisnikId",
                        column: x => x.korisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Narudzba",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Idkorisnik = table.Column<int>(type: "int", nullable: false),
                    korisnikId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    vrijemeNarudzbe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    stanjeIsporuke = table.Column<bool>(type: "bit", nullable: false),
                    Idkorpa = table.Column<int>(type: "int", nullable: false),
                    korpaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Narudzba", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Narudzba_Korisnik_korisnikId",
                        column: x => x.korisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Narudzba_Korpa_korpaId",
                        column: x => x.korpaId,
                        principalTable: "Korpa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StavkaNarudzbe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Idartikal = table.Column<int>(type: "int", nullable: false),
                    artikalId = table.Column<int>(type: "int", nullable: false),
                    kolicina = table.Column<int>(type: "int", nullable: false),
                    cijena = table.Column<double>(type: "float", nullable: false),
                    Idkorpa = table.Column<int>(type: "int", nullable: false),
                    korpaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StavkaNarudzbe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StavkaNarudzbe_Artikal_artikalId",
                        column: x => x.artikalId,
                        principalTable: "Artikal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StavkaNarudzbe_Korpa_korpaId",
                        column: x => x.korpaId,
                        principalTable: "Korpa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artikal_dimenzijeId",
                table: "Artikal",
                column: "dimenzijeId");

            migrationBuilder.CreateIndex(
                name: "IX_Kartica_korisnikId",
                table: "Kartica",
                column: "korisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Korpa_korisnikId",
                table: "Korpa",
                column: "korisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Narudzba_korisnikId",
                table: "Narudzba",
                column: "korisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Narudzba_korpaId",
                table: "Narudzba",
                column: "korpaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocjena_korisnikId",
                table: "Ocjena",
                column: "korisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_StavkaNarudzbe_artikalId",
                table: "StavkaNarudzbe",
                column: "artikalId");

            migrationBuilder.CreateIndex(
                name: "IX_StavkaNarudzbe_korpaId",
                table: "StavkaNarudzbe",
                column: "korpaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kartica");

            migrationBuilder.DropTable(
                name: "Narudzba");

            migrationBuilder.DropTable(
                name: "Ocjena");

            migrationBuilder.DropTable(
                name: "StavkaNarudzbe");

            migrationBuilder.DropTable(
                name: "Artikal");

            migrationBuilder.DropTable(
                name: "Korpa");

            migrationBuilder.DropTable(
                name: "Dimenzije");

            migrationBuilder.DropTable(
                name: "Korisnik");
        }
    }
}
