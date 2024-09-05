using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocadoraDeVeiculos.Dominio.ModuloPlanoCobranca;

namespace LocadoraDeVeiculos.Aplicacao.Servicos;
    public class PlanoCobrancaService
    {
	private readonly IRepositorioPlanoCobranca repositorioPlanoCobranca;

	public PlanoCobrancaService(IRepositorioPlanoCobranca repositorioPlanoCobranca)
	{
		this.repositorioPlanoCobranca = repositorioPlanoCobranca;
	}

	public Result<PlanoCobranca> Inserir(PlanoCobranca planoCobranca)
	{
		repositorioPlanoCobranca.Inserir(planoCobranca);

		return Result.Ok(planoCobranca);
	}

	public Result<PlanoCobranca> Editar(PlanoCobranca planoCobrancaAtualizado)
	{
		var planoCobranca = repositorioPlanoCobranca.SelecionarPorId(planoCobrancaAtualizado.Id);

		if (planoCobranca is null)
			return Result.Fail("O plano de cobrança não foi encontrado!");

		planoCobranca.GrupoAutomoveisId = planoCobrancaAtualizado.GrupoAutomoveisId;
		planoCobranca.PrecoDiarioPlanoDiario = planoCobrancaAtualizado.PrecoDiarioPlanoDiario;
		planoCobranca.PrecoQuilometroPlanoDiario = planoCobrancaAtualizado.PrecoQuilometroPlanoDiario;
		planoCobranca.QuilometrosDisponiveisPlanoControlado = planoCobrancaAtualizado.QuilometrosDisponiveisPlanoControlado;
		planoCobranca.PrecoDiarioPlanoControlado = planoCobrancaAtualizado.PrecoDiarioPlanoControlado;
		planoCobranca.PrecoQuilometroExtrapoladoPlanoControlado = planoCobrancaAtualizado.PrecoQuilometroExtrapoladoPlanoControlado;
		planoCobranca.PrecoDiarioPlanoLivre = planoCobrancaAtualizado.PrecoDiarioPlanoLivre;

		repositorioPlanoCobranca.Editar(planoCobranca);

		return Result.Ok(planoCobranca);
	}

	public Result<PlanoCobranca> Excluir(int planoCobrancaId)
	{
		var planoCobranca = repositorioPlanoCobranca.SelecionarPorId(planoCobrancaId);

		if (planoCobranca is null)
			return Result.Fail("O plano de cobrança não foi encontrado!");

		repositorioPlanoCobranca.Excluir(planoCobranca);

		return Result.Ok(planoCobranca);
	}

	public Result<PlanoCobranca> SelecionarPorId(int planoCobrancaId)
	{
		var planoCobranca = repositorioPlanoCobranca.SelecionarPorId(planoCobrancaId);

		if (planoCobranca is null)
			return Result.Fail("O plano de cobrança não foi encontrado!");

		return Result.Ok(planoCobranca);
	}

	public Result<List<PlanoCobranca>> SelecionarTodos(int empresaId)
	{
		var planosCobranca = repositorioPlanoCobranca.Filtrar(l => l.EmpresaId == empresaId);

		return Result.Ok(planosCobranca);
	}

	public Result<PlanoCobranca> SelecionarPorIdGrupoAutomoveis(int grupoAutomoveisId)
	{
		var plano = repositorioPlanoCobranca.FiltrarPlano(p => p.GrupoAutomoveisId == grupoAutomoveisId);

		if (plano is null)
			return Result.Fail("O plano de cobrança não foi encontrado!");

		return Result.Ok(plano);
	}
}

