using FizzWare.NBuilder;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomoveis;
using LocadoraDeVeiculos.Dominio.ModuloPlanoCobranca;
using LocadoraDeVeiculos.Infra.Orm.Compartilhado;
using LocadoraDeVeiculos.Infra.Orm.ModuloGrupoAutomoveis;
using LocadoraDeVeiculos.Infra.Orm.ModuloPlanoCobranca;

namespace LocadoraDeVeiculos.Testes.Integracao.ModuloPlanoCobranca;

[TestClass]
[TestCategory("Integração")]
public class RepositorioPlanoCobrancaEmOrmTests
{
    private LocadoraDbContext dbContext;
    private RepositorioPlanoCobrancaEmOrm repositorio;
    private RepositorioGrupoAutomoveisEmOrm repositorioGrupos;

    [TestInitialize]
    public void Inicializar()
    {
        dbContext = new LocadoraDbContext();

        dbContext.PlanoCobrancas.RemoveRange(dbContext.PlanoCobrancas);
        dbContext.Automoveis.RemoveRange(dbContext.Automoveis);
        dbContext.GrupoAutomoveis.RemoveRange(dbContext.GrupoAutomoveis);

        repositorio = new RepositorioPlanoCobrancaEmOrm(dbContext);
        repositorioGrupos = new RepositorioGrupoAutomoveisEmOrm(dbContext);

        BuilderSetup.SetCreatePersistenceMethod<PlanoCobranca>(repositorio.Inserir);
        BuilderSetup.SetCreatePersistenceMethod<GrupoAutomoveis>(repositorioGrupos.Inserir);
    }

    [TestMethod]
    public void Deve_Inserir_PlanoCobranca()
    {
        var grupo = Builder<GrupoAutomoveis>
            .CreateNew()
            .With(g => g.Id = 0)
            .Persist();

        var planoCobranca = Builder<PlanoCobranca>
            .CreateNew()
            .With(p => p.Id = 0)
            .With(p => p.GrupoAutomoveisId = grupo.Id)
            .Build();

        repositorio.Inserir(planoCobranca);

        var planoCobrancaSelecionado = repositorio.SelecionarPorId(planoCobranca.Id);

        Assert.IsNotNull(planoCobrancaSelecionado);
        Assert.AreEqual(planoCobranca, planoCobrancaSelecionado);
    }

    [TestMethod]
    public void Deve_Editar_PlanoCobranca()
    {
        var grupo = Builder<GrupoAutomoveis>
            .CreateNew()
            .With(g => g.Id = 0)
            .Persist();

        var planoCobranca = Builder<PlanoCobranca>
            .CreateNew()
            .With(p => p.Id = 0)
            .With(p => p.GrupoAutomoveisId = grupo.Id)
            .Persist();

        planoCobranca.PrecoDiarioPlanoDiario = 200.0m;

        repositorio.Editar(planoCobranca);

        var planoCobrancaSelecionado = repositorio.SelecionarPorId(planoCobranca.Id);

        Assert.IsNotNull(planoCobrancaSelecionado);
        Assert.AreEqual(planoCobranca, planoCobrancaSelecionado);
    }

    [TestMethod]
    public void Deve_Excluir_PlanoCobranca()
    {
        var grupo = Builder<GrupoAutomoveis>
            .CreateNew()
            .With(g => g.Id = 0)
            .Persist();

        var planoCobranca = Builder<PlanoCobranca>
            .CreateNew()
            .With(p => p.Id = 0)
            .With(p => p.GrupoAutomoveisId = grupo.Id)
            .Persist();

        repositorio.Excluir(planoCobranca);

        var planoCobrancaSelecionado = repositorio.SelecionarPorId(planoCobranca.Id);

        var planosCobranca = repositorio.SelecionarTodos();

        Assert.IsNull(planoCobrancaSelecionado);
        Assert.AreEqual(0, planosCobranca.Count);
    }
}
