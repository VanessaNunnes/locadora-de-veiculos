using System.Net.Mail;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAutomoveis;

namespace LocadoraDeVeiculos.Dominio.ModuloCombustivel;
	public class ConfiguracaoCombustivel : EntidadeBase
	{
		public int Id { get; set; }
		public DateTime DataCriacao { get; set; }

		public decimal ValorGasolina { get; set; }
		public decimal ValorGas { get; set; }
		public decimal ValorDiesel { get; set; }
		public decimal ValorAlcool { get; set; }

		protected ConfiguracaoCombustivel() { }

		public ConfiguracaoCombustivel(
			decimal valorGasolina,
			decimal valorGas,
			decimal valorDiesel,
			decimal valorAlcool
		) : this()
		{
			ValorGasolina = valorGasolina;
			ValorGas = valorGas;
			ValorDiesel = valorDiesel;
			ValorAlcool = valorAlcool;
		}

		public decimal ObterValorCombustivel(TipoCombustivelEnum tipoCombustivel)
		{
			return tipoCombustivel switch
			{
				TipoCombustivelEnum.Alcool => ValorAlcool,
				TipoCombustivelEnum.Diesel => ValorDiesel,
				TipoCombustivelEnum.Gas => ValorGas,
				_ => ValorGasolina
			};
		}

		public override List<string> Validar()
		{
			List<string> erros = [];

			if (ValorGasolina > 0)
				erros.Add("O valor precisa ser maior que 0");

			if (ValorGas > 0)
				erros.Add("O valor precisa ser maior que 0");

			if (ValorDiesel > 0)
				erros.Add("O valor precisa ser maior que 0");

			if (ValorAlcool > 0)
				erros.Add("O valor precisa ser maior que 0");

			return erros;
		}
}