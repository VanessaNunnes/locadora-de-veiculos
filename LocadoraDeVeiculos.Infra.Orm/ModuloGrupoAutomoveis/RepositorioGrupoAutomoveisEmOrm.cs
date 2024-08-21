using LocadoraDeVeiculos.Infra.Orm.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomoveis;
using Microsoft.EntityFrameworkCore;

namespace LocadoraDeVeiculos.Infra.Orm.ModuloGrupoAutomoveis
{
    public class RepositorioGrupoAutomoveisEmOrm : RepositorioBaseEmOrm<GrupoAutomoveis>, IRepositorioGrupoAutomoveis
    {
        public RepositorioGrupoAutomoveisEmOrm(
            LocadoraDbContext dbContext) : base(dbContext) { }

        protected override DbSet<GrupoAutomoveis> ObterRegistros()
        {
            return _dbContext.GrupoAutomoveis;
        }

        public List<GrupoAutomoveis> Filtrar(Func<GrupoAutomoveis, bool> predicate)
        {
            return _dbContext.GrupoAutomoveis
                .Where(predicate)
                .ToList();
        }
    }
}
