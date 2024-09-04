using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocadoraDeVeiculos.Infra.Orm.Migrations
{
    /// <inheritdoc />
    public partial class Updatetables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBAutomovel_AspNetUsers_Usuario_Id",
                table: "TBAutomovel");

            migrationBuilder.DropForeignKey(
                name: "FK_TBCliente_AspNetUsers_Usuario_Id",
                table: "TBCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_TBCondutor_AspNetUsers_Usuario_Id",
                table: "TBCondutor");

            migrationBuilder.DropForeignKey(
                name: "FK_TBConfiguracaoCombustivel_AspNetUsers_Usuario_Id",
                table: "TBConfiguracaoCombustivel");

            migrationBuilder.DropForeignKey(
                name: "FK_TBGrupoAutomoveis_AspNetUsers_Usuario_Id",
                table: "TBGrupoAutomoveis");

            migrationBuilder.DropForeignKey(
                name: "FK_TBLocacao_AspNetUsers_Usuario_Id",
                table: "TBLocacao");

            migrationBuilder.DropForeignKey(
                name: "FK_TBPlanoCobranca_AspNetUsers_Usuario_Id",
                table: "TBPlanoCobranca");

            migrationBuilder.DropForeignKey(
                name: "FK_TBTaxa_AspNetUsers_Usuario_Id",
                table: "TBTaxa");

            migrationBuilder.DropIndex(
                name: "IX_TBTaxa_Usuario_Id",
                table: "TBTaxa");

            migrationBuilder.DropIndex(
                name: "IX_TBPlanoCobranca_Usuario_Id",
                table: "TBPlanoCobranca");

            migrationBuilder.DropIndex(
                name: "IX_TBLocacao_Usuario_Id",
                table: "TBLocacao");

            migrationBuilder.DropIndex(
                name: "IX_TBGrupoAutomoveis_Usuario_Id",
                table: "TBGrupoAutomoveis");

            migrationBuilder.DropIndex(
                name: "IX_TBConfiguracaoCombustivel_Usuario_Id",
                table: "TBConfiguracaoCombustivel");

            migrationBuilder.DropIndex(
                name: "IX_TBCondutor_Usuario_Id",
                table: "TBCondutor");

            migrationBuilder.DropIndex(
                name: "IX_TBCliente_Usuario_Id",
                table: "TBCliente");

            migrationBuilder.DropIndex(
                name: "IX_TBAutomovel_Usuario_Id",
                table: "TBAutomovel");

            migrationBuilder.DropColumn(
                name: "Usuario_Id",
                table: "TBTaxa");

            migrationBuilder.DropColumn(
                name: "Usuario_Id",
                table: "TBPlanoCobranca");

            migrationBuilder.DropColumn(
                name: "Usuario_Id",
                table: "TBLocacao");

            migrationBuilder.DropColumn(
                name: "Usuario_Id",
                table: "TBGrupoAutomoveis");

            migrationBuilder.DropColumn(
                name: "Usuario_Id",
                table: "TBConfiguracaoCombustivel");

            migrationBuilder.DropColumn(
                name: "Usuario_Id",
                table: "TBCondutor");

            migrationBuilder.DropColumn(
                name: "Usuario_Id",
                table: "TBCliente");

            migrationBuilder.DropColumn(
                name: "Usuario_Id",
                table: "TBAutomovel");

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EmpresaId",
                table: "AspNetUsers",
                column: "EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_EmpresaId",
                table: "AspNetUsers",
                column: "EmpresaId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_EmpresaId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EmpresaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "Usuario_Id",
                table: "TBTaxa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Usuario_Id",
                table: "TBPlanoCobranca",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Usuario_Id",
                table: "TBLocacao",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Usuario_Id",
                table: "TBGrupoAutomoveis",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Usuario_Id",
                table: "TBConfiguracaoCombustivel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Usuario_Id",
                table: "TBCondutor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Usuario_Id",
                table: "TBCliente",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Usuario_Id",
                table: "TBAutomovel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TBTaxa_Usuario_Id",
                table: "TBTaxa",
                column: "Usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TBPlanoCobranca_Usuario_Id",
                table: "TBPlanoCobranca",
                column: "Usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TBLocacao_Usuario_Id",
                table: "TBLocacao",
                column: "Usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TBGrupoAutomoveis_Usuario_Id",
                table: "TBGrupoAutomoveis",
                column: "Usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TBConfiguracaoCombustivel_Usuario_Id",
                table: "TBConfiguracaoCombustivel",
                column: "Usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TBCondutor_Usuario_Id",
                table: "TBCondutor",
                column: "Usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TBCliente_Usuario_Id",
                table: "TBCliente",
                column: "Usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TBAutomovel_Usuario_Id",
                table: "TBAutomovel",
                column: "Usuario_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TBAutomovel_AspNetUsers_Usuario_Id",
                table: "TBAutomovel",
                column: "Usuario_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TBCliente_AspNetUsers_Usuario_Id",
                table: "TBCliente",
                column: "Usuario_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TBCondutor_AspNetUsers_Usuario_Id",
                table: "TBCondutor",
                column: "Usuario_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TBConfiguracaoCombustivel_AspNetUsers_Usuario_Id",
                table: "TBConfiguracaoCombustivel",
                column: "Usuario_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TBGrupoAutomoveis_AspNetUsers_Usuario_Id",
                table: "TBGrupoAutomoveis",
                column: "Usuario_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TBLocacao_AspNetUsers_Usuario_Id",
                table: "TBLocacao",
                column: "Usuario_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TBPlanoCobranca_AspNetUsers_Usuario_Id",
                table: "TBPlanoCobranca",
                column: "Usuario_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TBTaxa_AspNetUsers_Usuario_Id",
                table: "TBTaxa",
                column: "Usuario_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
