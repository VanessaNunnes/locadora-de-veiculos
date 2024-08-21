using LocadoraDeVeiculos.Dominio.Compartilhado;

namespace LocadoraDeVeiculos.Dominio.ModuloGrupoAutomoveis;

    public class GrupoAutomoveis : EntidadeBase
    {
        public string Nome { get; set; }

        public GrupoAutomoveis() { }

        public GrupoAutomoveis(string nome)
        {
            Nome = nome;
        }
    }

