using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomoveis;

namespace LocadoraDeVeiculos.Dominio.ModuloAutomoveis;

    public class Automovel : EntidadeBase
    {
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Cor { get; set; }
        public string Modelo { get; set; }
        public TipoCombustivelEnum TipoCombustivel{ get; set; }
        public int CapacidadeMax { get; set; }
        public int Ano { get; set; }
        public GrupoAutomoveis? GrupoAutomoveis { get; set; }
        public int GrupoAutomoveisId { get; set; }

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
}

