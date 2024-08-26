using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAutomoveis;

namespace LocadoraDeVeiculos.Dominio.ModuloGrupoAutomoveis;

    public class GrupoAutomoveis : EntidadeBase
    {
        public string Nome { get; set; }
        public List<Automovel> Automoveis { get; set; } = [];

        protected GrupoAutomoveis() { }

        public GrupoAutomoveis(string nome)
        {
            Nome = nome;
        }
    }

