using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using LocadoraDeVeiculos.Infra.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LocadoraDeVeiculos.Infra.Orm.ModuloTaxa;

public class RepositorioTaxaEmOrm : RepositorioBaseEmOrm<Taxa>, IRepositorioTaxa
{
	public RepositorioTaxaEmOrm(
		LocadoraDbContext dbContext) : base(dbContext) { }

	protected override DbSet<Taxa> ObterRegistros()
	{
		return _dbContext.Taxas;
	}

	//public List<Taxa> Filtrar(Func<Taxa, bool> predicate)
	//{
	//	return _dbContext.Taxas
	//		.Where(predicate)
	//		.ToList();
	//}

	public List<Taxa> SelecionarMuitos(List<int> idsTaxasSelecionadas)
	{
		return _dbContext.Taxas
			.Where(taxa => idsTaxasSelecionadas.Contains(taxa.Id))
			.ToList();
	}
}
