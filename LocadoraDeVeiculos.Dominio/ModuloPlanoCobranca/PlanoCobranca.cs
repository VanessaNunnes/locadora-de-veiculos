using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomoveis;

namespace LocadoraDeVeiculos.Dominio.ModuloPlanoCobranca;
    public class PlanoCobranca : EntidadeBase
    {
        public int GrupoAutomoveisId { get; set; }
        public GrupoAutomoveis? GrupoAutomoveis { get; set; }

        public decimal PrecoDiarioPlanoDiario { get; set; }
        public decimal PrecoQuilometroPlanoDiario { get; set; }

        public decimal QuilometrosDisponiveisPlanoControlado { get; set; }
        public decimal PrecoDiarioPlanoControlado { get; set; }
        public decimal PrecoQuilometroExtrapoladoPlanoControlado { get; set; }

        public decimal PrecoDiarioPlanoLivre { get; set; }

        protected PlanoCobranca() { }

        public PlanoCobranca(
            int grupoAutomoveisId,
            decimal precoDiarioPlanoDiario,
            decimal precoQuilometroPlanoDiario,
            decimal quilometrosDisponiveisPlanoControlado,
            decimal precoDiarioPlanoControlado,
            decimal precoQuilometroExtrapoladoPlanoControlado,
            decimal precoDiarioPlanoLivre
        )
        {
            GrupoAutomoveisId = grupoAutomoveisId;

            PrecoDiarioPlanoDiario = precoDiarioPlanoDiario;
            PrecoQuilometroPlanoDiario = precoQuilometroPlanoDiario;

            QuilometrosDisponiveisPlanoControlado = quilometrosDisponiveisPlanoControlado;
            PrecoDiarioPlanoControlado = precoDiarioPlanoControlado;
            PrecoQuilometroExtrapoladoPlanoControlado = precoQuilometroExtrapoladoPlanoControlado;

            PrecoDiarioPlanoLivre = precoDiarioPlanoLivre;
        }

        public override List<string> Validar()
        {
	        List<string> erros = [];

	        if (GrupoAutomoveisId == 0)
		        erros.Add("O grupo de veículos é obrigatório");

	        return erros;
        }
}
