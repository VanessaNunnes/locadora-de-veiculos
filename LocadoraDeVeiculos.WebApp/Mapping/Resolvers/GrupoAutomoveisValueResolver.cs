using AutoMapper;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomoveis;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LocadoraDeVeiculos.WebApp.Mapping.Resolvers;
    public class GrupoAutomoveisValueResolver : IValueResolver<object, object, IEnumerable<SelectListItem>?>
    {
        private readonly IRepositorioGrupoAutomoveis repositorioGrupo;

        public GrupoAutomoveisValueResolver(IRepositorioGrupoAutomoveis repositorioGrupo)
        {
            this.repositorioGrupo = repositorioGrupo;
        }

        public IEnumerable<SelectListItem> Resolve(
            object source,
            object destination,
            IEnumerable<SelectListItem>? destMember,
            ResolutionContext context
        )
        {
            return repositorioGrupo
                .SelecionarTodos()
                .Select(g => new SelectListItem(g.Nome, g.Id.ToString()));
        }
}

