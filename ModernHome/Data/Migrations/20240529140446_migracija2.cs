using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModernHome.Data.Migrations
{
    /// <inheritdoc />
    public partial class migracija2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "slika",
                table: "Artikal",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "slika",
                table: "Artikal");
        }
    }
}
