using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace crud_super_heroes.API.Migrations
{
    /// <inheritdoc />
    public partial class correcaoHeroisDto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Herois_NomeHeroi",
                table: "Herois");

            migrationBuilder.AlterColumn<string>(
                name: "NomeHeroi",
                table: "Herois",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NomeHeroi",
                table: "Herois",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Herois_NomeHeroi",
                table: "Herois",
                column: "NomeHeroi",
                unique: true);
        }
    }
}
