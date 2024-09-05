using AutoMapper;
using LocadoraDeVeiculos.Aplicacao.Servicos;
using LocadoraDeVeiculos.Dominio.ModuloLocacao;
using LocadoraDeVeiculos.WebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LocadoraDeVeiculos.WebApp.Mapping.Resolvers;

public class AutomoveisValueResolver : IValueResolver<Locacao, FormularioLocacaoViewModel, IEnumerable<SelectListItem>?>
{
	private readonly AutomovelService _servicoAutomovel;

	public AutomoveisValueResolver(AutomovelService servicoAutomovel)
	{
		_servicoAutomovel = servicoAutomovel;
	}

	public IEnumerable<SelectListItem>? Resolve(Locacao source, FormularioLocacaoViewModel destination, IEnumerable<SelectListItem>? destMember,
		ResolutionContext context)
	{
		if (destination is RealizarDevolucaoViewModel or ConfirmarAberturaLocacaoViewModel or ConfirmarDevolucaoLocacaoViewModel)
		{
			var automovelSelecionado = _servicoAutomovel.SelecionarPorId(source.AutomovelId).Value;

			return [new SelectListItem(automovelSelecionado!.Modelo, automovelSelecionado.Id.ToString())];
		}

		return _servicoAutomovel
			.SelecionarTodos(source.EmpresaId)
			.Value
			.Select(v => new SelectListItem(v.Modelo, v.Id.ToString()));
	}
}