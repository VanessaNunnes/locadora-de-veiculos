using AutoMapper;
using LocadoraDeVeiculos.Aplicacao.Servicos;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using LocadoraDeVeiculos.Dominio.ModuloLocacao;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using LocadoraDeVeiculos.Dominio.ModuloUsuario;
using LocadoraDeVeiculos.WebApp.Mapping.Resolvers;
using LocadoraDeVeiculos.WebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LocadoraDeVeiculos.WebApp.Mapping;

public class LocacaoProfile : Profile
{
    public LocacaoProfile()
    {
		CreateMap<InserirLocacaoViewModel, Locacao>()
			.ForMember(l => l.TaxasSelecionadas, opt => opt.MapFrom<TaxasSelecionadasValueResolver>());

		CreateMap<RealizarDevolucaoViewModel, Locacao>()
			.ForMember(l => l.TaxasSelecionadas, opt => opt.MapFrom<TaxasSelecionadasValueResolver>());

		CreateMap<Locacao, ListarLocacaoViewModel>()
			.ForMember(l => l.Automovel, opt => opt.MapFrom(src => src.Automovel!.Modelo))
			.ForMember(l => l.Condutor, opt => opt.MapFrom(src => src.Condutor!.Nome))
			.ForMember(l => l.TipoPlano, opt => opt.MapFrom(src => src.TipoPlano.ToString()));

		CreateMap<Locacao, RealizarDevolucaoViewModel>()
			.ForMember(l => l.Condutores, opt => opt.MapFrom<CondutoresValueResolver>())
			.ForMember(l => l.Automoveis, opt => opt.MapFrom<AutomoveisValueResolver>())
			.ForMember(l => l.Taxas, opt => opt.MapFrom<TaxasValueResolver>())
			.ForMember(l => l.TaxasSelecionadas,
				opt => opt.MapFrom(src => src.TaxasSelecionadas.Select(tx => tx.Id)));

		// Check-in
		CreateMap<Locacao, ConfirmarAberturaLocacaoViewModel>()
			.ForMember(l => l.ValorParcial, opt => opt.MapFrom<ValorParcialValueResolver>())
			.ForMember(l => l.Condutores, opt => opt.MapFrom<CondutoresValueResolver>())
			.ForMember(l => l.Automoveis, opt => opt.MapFrom<AutomoveisValueResolver>())
			.ForMember(l => l.Taxas, opt => opt.MapFrom<TaxasValueResolver>())
			.ForMember(l => l.TaxasSelecionadas,
				opt => opt.MapFrom(src => src.TaxasSelecionadas.Select(tx => tx.Id)));

		CreateMap<ConfirmarAberturaLocacaoViewModel, Locacao>()
			.ForMember(l => l.TaxasSelecionadas, opt => opt.MapFrom<TaxasSelecionadasValueResolver>());

		// Check-out
		CreateMap<Locacao, ConfirmarDevolucaoLocacaoViewModel>()
			.ForMember(vm => vm.ValorTotal, opt => opt.MapFrom<ValorTotalValueResolver>())
			.ForMember(l => l.Condutores, opt => opt.MapFrom<CondutoresValueResolver>())
			.ForMember(l => l.Automoveis, opt => opt.MapFrom<AutomoveisValueResolver>())
			.ForMember(l => l.Taxas, opt => opt.MapFrom<TaxasValueResolver>())
			.ForMember(l => l.TaxasSelecionadas,
				opt => opt.MapFrom(src => src.TaxasSelecionadas.Select(tx => tx.Id)));

		CreateMap<ConfirmarDevolucaoLocacaoViewModel, Locacao>()
			.ForMember(l => l.TaxasSelecionadas, opt => opt.MapFrom<TaxasSelecionadasValueResolver>());
	}
}
