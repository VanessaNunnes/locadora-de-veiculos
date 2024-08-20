using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocadoraDeVeiculos.Dominio.Compartilhado;

public interface IRepositorio<TEntidade> where TEntidade : EntidadeBase
{
    void Inserir(TEntidade entidade);
    void Editar(TEntidade entidadeAtualizada);
    void Excluir(TEntidade entidadeParaExcluir);
    TEntidade? SelecionarPorId(int idSelecionado);
    List<TEntidade> SelecionarTodos();
    List<TEntidade> Filtrar(Func<TEntidade, bool> predicate);
}