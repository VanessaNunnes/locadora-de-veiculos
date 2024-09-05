using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloCombustivel;

namespace LocadoraDeVeiculos.Aplicacao.Servicos;
	public class ConfiguracaoCombustivelService
	{
		private readonly IRepositorioConfiguracaoCombustivel repositorioConfig;

		public ConfiguracaoCombustivelService(IRepositorioConfiguracaoCombustivel repositorioConfig)
		{
			this.repositorioConfig = repositorioConfig;
		}

		public Result SalvarConfiguracao(ConfiguracaoCombustivel configuracao)
		{
			configuracao.DataCriacao = DateTime.Now;

			repositorioConfig.GravarConfiguracao(configuracao);

			return Result.Ok();
		}

		public Result<ConfiguracaoCombustivel> ObterConfiguracao(int idEmpresa)
		{
			var config = repositorioConfig.ObterConfiguracao(idEmpresa);

			if (config is null)
			{
				config = new ConfiguracaoCombustivel(
					valorAlcool: 0.0m,
					valorDiesel: 0.0m,
					valorGas: 0.0m,
					valorGasolina: 0.0m
				);
			}

			return Result.Ok(config);
		}
}