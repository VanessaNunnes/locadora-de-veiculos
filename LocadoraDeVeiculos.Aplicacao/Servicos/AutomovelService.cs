using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloAutomoveis;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomoveis;

namespace LocadoraDeVeiculos.Aplicacao.Servicos
{
    public class AutomovelService
    {
        private readonly IRepositorioAutomovel repositorioAutomovel;

		public AutomovelService(IRepositorioAutomovel repositorioAutomovel)
		{
			this.repositorioAutomovel = repositorioAutomovel;
		}

		public Result<Automovel> Inserir(Automovel veiculo)
		{
			repositorioAutomovel.Inserir(veiculo);

			return Result.Ok(veiculo);
		}

		public Result<Automovel> Editar(Automovel automovelAtualizado)
		{
			var automovel = repositorioAutomovel.SelecionarPorId(automovelAtualizado.Id);

			if (automovel is null)
				return Result.Fail("O veículo não foi encontrado!");

			automovel.Modelo = automovelAtualizado.Modelo;
			automovel.Marca = automovelAtualizado.Marca;
			automovel.TipoCombustivel = automovelAtualizado.TipoCombustivel;
			automovel.CapacidadeMax = automovelAtualizado.CapacidadeMax;
			automovel.GrupoAutomoveisId = automovelAtualizado.GrupoAutomoveisId;

			repositorioAutomovel.Editar(automovel);

			return Result.Ok(automovel);
		}

		public Result<Automovel> Excluir(int automovelId)
		{
			var automovel = repositorioAutomovel.SelecionarPorId(automovelId);

			if (automovel is null)
				return Result.Fail("O veículo não foi encontrado!");

			repositorioAutomovel.Excluir(automovel);

			return Result.Ok(automovel);
		}

		public Result<Automovel> SelecionarPorId(int automovelId)
		{
			var automovel = repositorioAutomovel.SelecionarPorId(automovelId);

			if (automovel is null)
				return Result.Fail("O veículo não foi encontrado!");

			return Result.Ok(automovel);
		}

		public Result<List<Automovel>> SelecionarTodos(int empresaId)
		{
			var automoveis = repositorioAutomovel.Filtrar(
				l => l.EmpresaId == empresaId &&
				     l.Alugado == false
			);

			return Result.Ok(automoveis);
		}
	}
}
