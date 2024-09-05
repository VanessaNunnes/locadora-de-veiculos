using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocadoraDeVeiculos.Dominio.ModuloCombustivel;
	public interface IRepositorioConfiguracaoCombustivel
	{
	void GravarConfiguracao(ConfiguracaoCombustivel configuracaoCombustivel);
	ConfiguracaoCombustivel? ObterConfiguracao(int idEmpresa);
}
