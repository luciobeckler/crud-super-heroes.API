using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace crud_super_heroes.API.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoModelo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Herois_Nome",
                table: "Herois");

            migrationBuilder.DeleteData(
                table: "SuperPoderes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SuperPoderes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "SuperPoderes",
                newName: "SuperPoder");

            migrationBuilder.RenameColumn(
                name: "DataDeNascimento",
                table: "Herois",
                newName: "DataNascimento");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Herois",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "NomeHeroi",
                table: "Herois",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Herois_NomeHeroi",
                table: "Herois",
                column: "NomeHeroi",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Herois_NomeHeroi",
                table: "Herois");

            migrationBuilder.DropColumn(
                name: "NomeHeroi",
                table: "Herois");

            migrationBuilder.RenameColumn(
                name: "SuperPoder",
                table: "SuperPoderes",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "DataNascimento",
                table: "Herois",
                newName: "DataDeNascimento");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Herois",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "SuperPoderes",
                columns: new[] { "Id", "Descricao", "Nome" },
                values: new object[,]
                {
                    { 1, "Super força", "Força" },
                    { 2, "Super velocidade", "Velocidade" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Herois_Nome",
                table: "Herois",
                column: "Nome",
                unique: true);
        }
    }
}
