using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomoveis;

namespace LocadoraDeVeiculos.Aplicacao.Servicos;
    public class GrupoAutomoveisService
    {
	private readonly IRepositorioGrupoAutomoveis repositorioGrupo;

	public GrupoAutomoveisService(IRepositorioGrupoAutomoveis repositorioGrupo)
	{
		this.repositorioGrupo = repositorioGrupo;
	}

	public Result<GrupoAutomoveis> Inserir(GrupoAutomoveis grupo)
	{
		repositorioGrupo.Inserir(grupo);

		return Result.Ok(grupo);
	}

	public Result<GrupoAutomoveis> Editar(GrupoAutomoveis grupoAtualizado)
	{
		var grupo = repositorioGrupo.SelecionarPorId(grupoAtualizado.Id);

		if (grupo is null)
			return Result.Fail("O grupo não foi encontrado!");

		grupo.Nome = grupoAtualizado.Nome;

		repositorioGrupo.Editar(grupo);

		return Result.Ok(grupo);
	}

	public Result<GrupoAutomoveis> Excluir(int grupoId)
	{
		var grupo = repositorioGrupo.SelecionarPorId(grupoId);

		if (grupo is null)
			return Result.Fail("O grupo não foi encontrado!");

		repositorioGrupo.Excluir(grupo);

		return Result.Ok(grupo);
	}

	public Result<GrupoAutomoveis> SelecionarPorId(int grupoId)
	{
		var grupo = repositorioGrupo.SelecionarPorId(grupoId);

		if (grupo is null)
			return Result.Fail("O grupo não foi encontrado!");

		return Result.Ok(grupo);
	}

	public Result<List<GrupoAutomoveis>> SelecionarTodos(int empresaId)
	{
		var grupos = repositorioGrupo.Filtrar(g => g.EmpresaId == empresaId);

		return Result.Ok(grupos);
	}
}
