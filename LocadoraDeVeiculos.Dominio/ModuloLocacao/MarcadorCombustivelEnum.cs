using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocadoraDeVeiculos.Dominio.ModuloLocacao;
public enum MarcadorCombustivelEnum
{
	Vazio,
	[Display(Name = "Um Quarto")] UmQuarto,
	[Display(Name = "Meio Tanque")] MeioTanque,
	[Display(Name = "Três Quartos")] TresQuartos,
	Completo
}
