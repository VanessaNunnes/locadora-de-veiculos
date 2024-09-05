using AutoMapper;
using LocadoraDeVeiculos.Dominio.ModuloAutomoveis;
using LocadoraDeVeiculos.WebApp.Mapping.Resolvers;
using LocadoraDeVeiculos.WebApp.Models;

namespace LocadoraDeVeiculos.WebApp.Mapping;

public class AutomovelProfile : Profile
{
	public AutomovelProfile()
	{
		CreateMap<InserirAutomovelViewModel, Automovel>()
			.ForMember(dest => dest.EmpresaId, opt => opt.MapFrom<EmpresaIdValueResolver>())
			.ForMember(dest => dest.Foto, opt => opt.MapFrom<FotoValueResolver>());

		CreateMap<EditarAutomovelViewModel, Automovel>()
			.ForMember(dest => dest.Foto, opt => opt.MapFrom<FotoValueResolver>());

		CreateMap<Automovel, ListarAutomovelViewModel>()
			.ForMember(
				dest => dest.GrupoAutomoveis,
				opt => opt.MapFrom(src => src.GrupoAutomoveis!.Nome)
			);

		CreateMap<Automovel, DetalhesAutomovelViewModel>()
			.ForMember(dest => dest.GrupoAutomoveis, opt => opt.MapFrom(src => src.GrupoAutomoveis!.Nome));

		CreateMap<Automovel, EditarAutomovelViewModel>()
			.ForMember(v => v.Foto, opt => opt.Ignore())
			.ForMember(v => v.GrupoAutomoveis, opt => opt.MapFrom<GrupoAutomoveisValueResolver>());
	}
}

