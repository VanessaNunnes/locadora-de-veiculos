using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LocadoraDeVeiculos.WebApp.Models;
        public class FormularioAutomovelViewModel
        {
	        [Required(ErrorMessage = "A placa é obrigatório")]
	        [MinLength(3, ErrorMessage = "A placa deve conter ao menos 3 caracteres")]
			public string Placa { get; set; }

			[Required(ErrorMessage = "O modelo é obrigatório")]
            [MinLength(3, ErrorMessage = "O modelo deve conter ao menos 3 caracteres")]
            public string Modelo { get; set; }

            [Required(ErrorMessage = "A marca é obrigatória")]
            [MinLength(2, ErrorMessage = "A marca deve conter ao menos 2 caracteres")]
            public string Marca { get; set; }

            [Required(ErrorMessage = "A cor é obrigatória")]
            [MinLength(2, ErrorMessage = "A cor deve conter ao menos 2 caracteres")]
            public string Cor { get; set; }

			[Required(ErrorMessage = "O tipo de combustível é obrigatório")]
            public int TipoCombustivel { get; set; }

            [Required(ErrorMessage = "A capacidade do tanque é obrigatória")]
            [Range(1, int.MaxValue, ErrorMessage = "A capacidade do tanque deve ser maior que 0")]
            public int CapacidadeMax { get; set; }

            [Required(ErrorMessage = "O grupo de veículos é obrigatório")]
            public int GrupoAutomoveisId { get; set; }

            public IEnumerable<SelectListItem>? GrupoAutomoveis { get; set; }
        }

        public class InserirAutomovelViewModel : FormularioAutomovelViewModel { }

        public class EditarAutomovelViewModel : FormularioAutomovelViewModel
        {
            public int Id { get; set; }

        }

        public class ListarAutomovelViewModel
        {
            public int Id { get; set; }
            public string Placa { get; set; }
			public string Modelo { get; set; }
            public string Marca { get; set; }
            public string Cor { get; set; }
			public string TipoCombustivel { get; set; }
            public int CapacidadeMax { get; set; }
            public string GrupoAutomoveis { get; set; }
        }

        public class DetalhesAutomovelViewModel
        {
	        public int Id { get; set; }
	        public string Placa { get; set; }
	        public string Modelo { get; set; }
	        public string Marca { get; set; }
	        public string Cor { get; set; }
	        public string TipoCombustivel { get; set; }
	        public int CapacidadeMax { get; set; }
	        public string GrupoAutomoveis { get; set; }
}
