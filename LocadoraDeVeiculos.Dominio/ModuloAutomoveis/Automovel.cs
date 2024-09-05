using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomoveis;
using LocadoraDeVeiculos.Dominio.ModuloLocacao;

namespace LocadoraDeVeiculos.Dominio.ModuloAutomoveis;

    public class Automovel : EntidadeBase
    {
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Cor { get; set; }
        public string Modelo { get; set; }
        public TipoCombustivelEnum TipoCombustivel{ get; set; }
        public int CapacidadeMax { get; set; }
        public byte[] Foto { get; set; }
		public int Ano { get; set; }
		public GrupoAutomoveis? GrupoAutomoveis { get; set; }
        public int GrupoAutomoveisId { get; set; }
        public bool Alugado { get; set; }

		protected Automovel() { }

        public Automovel(string placa, string marca, string cor, TipoCombustivelEnum tipoCombustivel, int capacidadeMax, int ano, int grupoAutomoveisId) 
        {
            Placa = placa;
            Marca = marca;
            Cor = cor;
            TipoCombustivel = tipoCombustivel;
            CapacidadeMax = capacidadeMax;
            Ano = ano;
            GrupoAutomoveisId = grupoAutomoveisId;
        }

        public override List<string> Validar()
        {
	        List<string> erros = [];

	        if (GrupoAutomoveisId == 0)
		        erros.Add("O grupo de veículos é obrigatório");

	        return erros;
        }

        public void Alugar()
        {
	        Alugado = true;
        }

        public void Desocupar()
        {
	        Alugado = false;
        }

        public decimal CalcularLitrosParaAbastecimento(MarcadorCombustivelEnum marcadorCombustivel)
        {
	        switch (marcadorCombustivel)
	        {
		        case MarcadorCombustivelEnum.Vazio: return CapacidadeMax;

		        case MarcadorCombustivelEnum.UmQuarto: return (CapacidadeMax - (CapacidadeMax * 1 / 4));

		        case MarcadorCombustivelEnum.MeioTanque: return (CapacidadeMax - (CapacidadeMax * 1 / 2));

		        case MarcadorCombustivelEnum.TresQuartos: return (CapacidadeMax - (CapacidadeMax * 3 / 4));

		        default:
			        return 0;
	        }
        }
}

