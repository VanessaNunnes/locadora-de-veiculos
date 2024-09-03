using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocadoraDeVeiculos.Dominio.Compartilhado;

namespace LocadoraDeVeiculos.Dominio.ModuloPlanoCobranca;
    public interface IRepositorioPlanoCobranca : IRepositorio<PlanoCobranca>
    {
	    PlanoCobranca? FiltrarPlano(Func<PlanoCobranca, bool> predicate);
	}
