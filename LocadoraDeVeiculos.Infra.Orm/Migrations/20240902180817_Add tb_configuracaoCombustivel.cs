using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocadoraDeVeiculos.Infra.Orm.Migrations
{
    /// <inheritdoc />
    public partial class Addtb_configuracaoCombustivel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBConfiguracaoCombustivel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorGasolina = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorGas = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorDiesel = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorAlcool = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Usuario_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBConfiguracaoCombustivel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBConfiguracaoCombustivel_AspNetUsers_Usuario_Id",
                        column: x => x.Usuario_Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBConfiguracaoCombustivel_Usuario_Id",
                table: "TBConfiguracaoCombustivel",
                column: "Usuario_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBConfiguracaoCombustivel");
        }
    }
}
