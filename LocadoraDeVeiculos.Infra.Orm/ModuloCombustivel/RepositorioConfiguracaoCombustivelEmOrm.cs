using LocadoraDeVeiculos.Dominio.ModuloCombustivel;
using LocadoraDeVeiculos.Infra.Orm.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocadoraDeVeiculos.Infra.Orm.ModuloCombustivel;
	public class RepositorioConfiguracaoCombustivelEmOrm : IRepositorioConfiguracaoCombustivel
	{
		private readonly LocadoraDbContext dbContext;

		public RepositorioConfiguracaoCombustivelEmOrm(LocadoraDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public void GravarConfiguracao(ConfiguracaoCombustivel configuracaoCombustivel)
		{
			dbContext.ConfiguracoesCombustiveis.Add(configuracaoCombustivel);

			dbContext.SaveChanges();
		}

		public ConfiguracaoCombustivel? ObterConfiguracao()
		{
			return dbContext.ConfiguracoesCombustiveis
				.OrderByDescending(c => c.Id)
				.FirstOrDefault();
		}
	}

