using System.ComponentModel.DataAnnotations;

namespace LocadoraDeVeiculos.Dominio.ModuloUsuario;
	public enum TipoUsuarioEnum
	{
		Empresa,
		[Display(Name = "Funcionário")] Funcionario
	}
