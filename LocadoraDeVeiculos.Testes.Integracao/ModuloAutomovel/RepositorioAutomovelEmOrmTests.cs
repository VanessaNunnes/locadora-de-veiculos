using FizzWare.NBuilder;
using LocadoraDeVeiculos.Dominio.ModuloAutomoveis;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomoveis;
using LocadoraDeVeiculos.Infra.Orm.Compartilhado;
using LocadoraDeVeiculos.Infra.Orm.ModuloAutomovel;
using LocadoraDeVeiculos.Infra.Orm.ModuloGrupoAutomoveis;

namespace LocadoraDeVeiculos.Testes.Integracao.ModuloVeiculo;

[TestClass]
[TestCategory("Integração")]
public class RepositorioAutomovelEmOrmTests
{
    private LocadoraDbContext dbContext;
    private RepositorioAutomovelEmOrm repositorio;
    private RepositorioGrupoAutomoveisEmOrm repositorioGrupos;

    [TestInitialize]
    public void Inicializar()
    {
        dbContext = new LocadoraDbContext();

        dbContext.Automoveis.RemoveRange(dbContext.Automoveis);
        dbContext.GrupoAutomoveis.RemoveRange(dbContext.GrupoAutomoveis);

        repositorio = new RepositorioAutomovelEmOrm(dbContext);
        repositorioGrupos = new RepositorioGrupoAutomoveisEmOrm(dbContext);

        BuilderSetup.SetCreatePersistenceMethod<Automovel>(repositorio.Inserir);
        BuilderSetup.SetCreatePersistenceMethod<GrupoAutomoveis>(repositorioGrupos.Inserir);
    }

    [TestMethod]
    public void Deve_Inserir_Automovel()
    {
        var grupo = Builder<GrupoAutomoveis>
            .CreateNew()
            .With(g => g.Id = 0)
            .Persist();

        var veiculo = Builder<Automovel>
            .CreateNew()
            .With(v => v.Id = 0)
            .With(v => v.GrupoAutomoveisId = grupo.Id)
            .Persist();

        var veiculoSelecionado = repositorio.SelecionarPorId(veiculo.Id);

        Assert.IsNotNull(veiculoSelecionado);
        Assert.AreEqual(veiculo, veiculoSelecionado);
    }

    [TestMethod]
    public void Deve_Editar_Automovel()
    {
        var grupo = Builder<GrupoAutomoveis>
            .CreateNew()
            .With(g => g.Id = 0)
            .Persist();

        var veiculo = Builder<Automovel>
            .CreateNew()
            .With(v => v.Id = 0)
            .With(v => v.GrupoAutomoveisId = grupo.Id)
            .Persist();

        veiculo.Modelo = "Novo Modelo";

        repositorio.Editar(veiculo);

        var veiculoSelecionado = repositorio.SelecionarPorId(veiculo.Id);

        Assert.IsNotNull(veiculoSelecionado);
        Assert.AreEqual(veiculo, veiculoSelecionado);
    }

    [TestMethod]
    public void Deve_Excluir_Automovel()
    {
        var grupo = Builder<GrupoAutomoveis>
            .CreateNew()
            .With(g => g.Id = 0)
            .Persist();

        var veiculo = Builder<Automovel>
            .CreateNew()
            .With(v => v.Id = 0)
            .With(v => v.GrupoAutomoveisId = grupo.Id)
            .Persist();

        repositorio.Excluir(veiculo);

        var veiculoSelecionado = repositorio.SelecionarPorId(veiculo.Id);

        var veiculos = repositorio.SelecionarTodos();

        Assert.IsNull(veiculoSelecionado);
        Assert.AreEqual(0, veiculos.Count);
    }
}
