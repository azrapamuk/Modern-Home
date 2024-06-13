using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModernHome.Data.Migrations
{
    /// <inheritdoc />
    public partial class migra314 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "cijena",
                table: "Narudzba",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cijena",
                table: "Narudzba");
        }
    }
}
