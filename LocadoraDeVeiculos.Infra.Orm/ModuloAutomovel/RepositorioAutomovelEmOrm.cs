using LocadoraDeVeiculos.Infra.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;
using LocadoraDeVeiculos.Dominio.ModuloAutomoveis;

namespace LocadoraDeVeiculos.Infra.Orm.ModuloAutomovel;

    public class RepositorioAutomovelEmOrm : RepositorioBaseEmOrm<Automovel>, IRepositorioAutomovel
    {
        public RepositorioAutomovelEmOrm(LocadoraDbContext dbContext) : base(dbContext)
        {
        }

        protected override DbSet<Automovel> ObterRegistros()
        {
            return _dbContext.Automoveis;
        }

        public override Automovel? SelecionarPorId(int id)
        {
            return ObterRegistros()
                .Include(v => v.GrupoAutomoveis)
                .FirstOrDefault(v => v.Id == id);
        }

        public override List<Automovel> SelecionarTodos()
        {
            return ObterRegistros()
                .Include(v => v.GrupoAutomoveis)
                .ToList();
        }

	public List<Automovel> Filtrar(Func<Automovel, bool> predicate)
	{
		return _dbContext.Automoveis
			.Where(predicate)
			.ToList();
	}
}
