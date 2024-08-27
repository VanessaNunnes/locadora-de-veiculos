using AutoMapper;
using LocadoraDeVeiculos.Dominio.ModuloAutomoveis;
using LocadoraDeVeiculos.WebApp.Mapping.Resolvers;
using LocadoraDeVeiculos.WebApp.Models;

namespace LocadoraDeVeiculos.WebApp.Mapping;

    public class AutomovelProfile : Profile
    {
        public AutomovelProfile()
        {
        CreateMap<InserirAutomovelViewModel, Automovel>();
        CreateMap<EditarAutomovelViewModel, Automovel>();

        CreateMap<Automovel, ListarAutomovelViewModel>()
            .ForMember(
                dest => dest.GrupoAutomoveis,
                opt => opt.MapFrom(src => src.GrupoAutomoveis != null ? src.GrupoAutomoveis.Nome : string.Empty)
            );

        CreateMap<Automovel, DetalhesAutomovelViewModel>()
            .ForMember(
                dest => dest.GrupoAutomoveis,
                opt => opt.MapFrom(src => src.GrupoAutomoveis != null ? src.GrupoAutomoveis.Nome : string.Empty)
            );

        CreateMap<Automovel, EditarAutomovelViewModel>()
            .ForMember(v => v.GrupoAutomoveis, opt => opt.MapFrom<GrupoAutomoveisValueResolver>());
    }
    }

