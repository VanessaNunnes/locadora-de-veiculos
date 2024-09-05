using AutoMapper;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomoveis;
using LocadoraDeVeiculos.WebApp.Mapping.Resolvers;
using LocadoraDeVeiculos.WebApp.Models;

namespace LocadoraDeVeiculos.WebApp.Mapping;
    public class GrupoAutomoveisProfile : Profile
    {
	public GrupoAutomoveisProfile()
	{
		CreateMap<InserirGrupoAutomoveisViewModel, GrupoAutomoveis>()
			.ForMember(dest => dest.EmpresaId, opt => opt.MapFrom<EmpresaIdValueResolver>());

		CreateMap<EditarGrupoAutomoveisViewModel, GrupoAutomoveis>();

		CreateMap<GrupoAutomoveis, ListarGrupoAutomoveisViewModel>();
		CreateMap<GrupoAutomoveis, DetalhesGrupoAutomoveisViewModel>();
		CreateMap<GrupoAutomoveis, EditarGrupoAutomoveisViewModel>();
	}
}

