using AutoMapper;
using LocadoraDeVeiculos.Dominio.ModuloPlanoCobranca;
using LocadoraDeVeiculos.WebApp.Mapping.Resolvers;
using LocadoraDeVeiculos.WebApp.Models;

namespace LocadoraDeVeiculos.WebApp.Mapping;
    public class PlanoCobrancaProfile : Profile
    {
	public PlanoCobrancaProfile()
	{
		CreateMap<InserirPlanoCobrancaViewModel, PlanoCobranca>()
			.ForMember(dest => dest.EmpresaId, opt => opt.MapFrom<EmpresaIdValueResolver>());

		CreateMap<EditarPlanoCobrancaViewModel, PlanoCobranca>();

		CreateMap<PlanoCobranca, ListarPlanoCobrancaViewModel>()
			.ForMember(
				dest => dest.GrupoAutomoveis,
				opt => opt.MapFrom(src => src.GrupoAutomoveis!.Nome));

		CreateMap<PlanoCobranca, DetalhesPlanoCobrancaViewModel>()
			.ForMember(
				dest => dest.GrupoAutomoveis,
				opt => opt.MapFrom(src => src.GrupoAutomoveis!.Nome));

		CreateMap<PlanoCobranca, EditarPlanoCobrancaViewModel>()
			.ForMember(dest => dest.GrupoAutomoveis, opt => opt.MapFrom<GrupoAutomoveisValueResolver>());
	}
}

