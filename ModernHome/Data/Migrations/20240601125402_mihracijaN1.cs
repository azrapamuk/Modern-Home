using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModernHome.Data.Migrations
{
    /// <inheritdoc />
    public partial class mihracijaN1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artikal_Dimenzije_dimenzijeId",
                table: "Artikal");

            migrationBuilder.DropForeignKey(
                name: "FK_Kartica_Korisnik_korisnikId",
                table: "Kartica");

            migrationBuilder.DropForeignKey(
                name: "FK_Korpa_Korisnik_korisnikId",
                table: "Korpa");

            migrationBuilder.DropForeignKey(
                name: "FK_Narudzba_Korisnik_korisnikId",
                table: "Narudzba");

            migrationBuilder.DropForeignKey(
                name: "FK_Narudzba_Korpa_korpaId",
                table: "Narudzba");

            migrationBuilder.DropForeignKey(
                name: "FK_Ocjena_Korisnik_korisnikId",
                table: "Ocjena");

            migrationBuilder.DropForeignKey(
                name: "FK_StavkaNarudzbe_Korpa_korpaId",
                table: "StavkaNarudzbe");

            migrationBuilder.DropIndex(
                name: "IX_StavkaNarudzbe_korpaId",
                table: "StavkaNarudzbe");

            migrationBuilder.DropIndex(
                name: "IX_Ocjena_korisnikId",
                table: "Ocjena");

            migrationBuilder.DropIndex(
                name: "IX_Narudzba_korisnikId",
                table: "Narudzba");

            migrationBuilder.DropIndex(
                name: "IX_Narudzba_korpaId",
                table: "Narudzba");

            migrationBuilder.DropIndex(
                name: "IX_Korpa_korisnikId",
                table: "Korpa");

            migrationBuilder.DropIndex(
                name: "IX_Kartica_korisnikId",
                table: "Kartica");

            migrationBuilder.DropIndex(
                name: "IX_Artikal_dimenzijeId",
                table: "Artikal");

            migrationBuilder.DropColumn(
                name: "korpaId",
                table: "StavkaNarudzbe");

            migrationBuilder.DropColumn(
                name: "korisnikId",
                table: "Ocjena");

            migrationBuilder.DropColumn(
                name: "korisnikId",
                table: "Narudzba");

            migrationBuilder.DropColumn(
                name: "korpaId",
                table: "Narudzba");

            migrationBuilder.DropColumn(
                name: "korisnikId",
                table: "Korpa");

            migrationBuilder.DropColumn(
                name: "korisnikId",
                table: "Kartica");

            migrationBuilder.DropColumn(
                name: "dimenzijeId",
                table: "Artikal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "korpaId",
                table: "StavkaNarudzbe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "korisnikId",
                table: "Ocjena",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "korisnikId",
                table: "Narudzba",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "korpaId",
                table: "Narudzba",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "korisnikId",
                table: "Korpa",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "korisnikId",
                table: "Kartica",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "dimenzijeId",
                table: "Artikal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StavkaNarudzbe_korpaId",
                table: "StavkaNarudzbe",
                column: "korpaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocjena_korisnikId",
                table: "Ocjena",
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
                name: "IX_Korpa_korisnikId",
                table: "Korpa",
                column: "korisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Kartica_korisnikId",
                table: "Kartica",
                column: "korisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Artikal_dimenzijeId",
                table: "Artikal",
                column: "dimenzijeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artikal_Dimenzije_dimenzijeId",
                table: "Artikal",
                column: "dimenzijeId",
                principalTable: "Dimenzije",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Kartica_Korisnik_korisnikId",
                table: "Kartica",
                column: "korisnikId",
                principalTable: "Korisnik",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Korpa_Korisnik_korisnikId",
                table: "Korpa",
                column: "korisnikId",
                principalTable: "Korisnik",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzba_Korisnik_korisnikId",
                table: "Narudzba",
                column: "korisnikId",
                principalTable: "Korisnik",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzba_Korpa_korpaId",
                table: "Narudzba",
                column: "korpaId",
                principalTable: "Korpa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ocjena_Korisnik_korisnikId",
                table: "Ocjena",
                column: "korisnikId",
                principalTable: "Korisnik",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StavkaNarudzbe_Korpa_korpaId",
                table: "StavkaNarudzbe",
                column: "korpaId",
                principalTable: "Korpa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
