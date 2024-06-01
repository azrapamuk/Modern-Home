using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModernHome.Data.Migrations
{
    /// <inheritdoc />
    public partial class migracijaN2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StavkaNarudzbe_Artikal_artikalId",
                table: "StavkaNarudzbe");

            migrationBuilder.DropIndex(
                name: "IX_StavkaNarudzbe_artikalId",
                table: "StavkaNarudzbe");

            migrationBuilder.DropColumn(
                name: "artikalId",
                table: "StavkaNarudzbe");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "artikalId",
                table: "StavkaNarudzbe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StavkaNarudzbe_artikalId",
                table: "StavkaNarudzbe",
                column: "artikalId");

            migrationBuilder.AddForeignKey(
                name: "FK_StavkaNarudzbe_Artikal_artikalId",
                table: "StavkaNarudzbe",
                column: "artikalId",
                principalTable: "Artikal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
