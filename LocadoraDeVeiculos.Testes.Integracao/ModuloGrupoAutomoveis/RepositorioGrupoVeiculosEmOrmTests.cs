using FizzWare.NBuilder;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomoveis;
using LocadoraDeVeiculos.Infra.Orm.Compartilhado;
using LocadoraDeVeiculos.Infra.Orm.ModuloGrupoAutomoveis;

namespace LocadoraDeVeiculos.Testes.Integracao.ModuloGrupoVeiculos;

[TestClass]
[TestCategory("Integração")]
public class RepositorioGrupoAutomoveisEmOrmTests
{
    private LocadoraDbContext dbContext;
    private RepositorioGrupoAutomoveisEmOrm repositorio;

    [TestInitialize]
    public void Inicializar()
    {
        dbContext = new LocadoraDbContext();

        dbContext.GrupoAutomoveis.RemoveRange(dbContext.GrupoAutomoveis);

        repositorio = new RepositorioGrupoAutomoveisEmOrm(dbContext);

        BuilderSetup.SetCreatePersistenceMethod<GrupoAutomoveis>(repositorio.Inserir);
    }

    [TestMethod]
    public void Deve_Inserir_GrupoAutomoveis()
    {
        var grupo = Builder<GrupoAutomoveis>
            .CreateNew()
            .Persist();

        var grupoSelecionado = repositorio.SelecionarPorId(grupo.Id);

        Assert.IsNotNull(grupoSelecionado);
        Assert.AreEqual(grupo, grupoSelecionado);
    }

    [TestMethod]
    public void Deve_Editar_GrupoAutomoveis()
    {
        var grupo = Builder<GrupoAutomoveis>
            .CreateNew()
            .Persist();

        grupo.Nome = "Teste de Edição";
        repositorio.Editar(grupo);

        var grupoSelecionado = repositorio.SelecionarPorId(grupo.Id);

        Assert.IsNotNull(grupoSelecionado);
        Assert.AreEqual(grupo, grupoSelecionado);
    }

    [TestMethod]
    public void Deve_Excluir_GrupoAutomoveis()
    {
        var grupo = Builder<GrupoAutomoveis>
            .CreateNew()
            .Persist();

        repositorio.Excluir(grupo);

        var grupoSelecionado = repositorio.SelecionarPorId(grupo.Id);

        var grupos = repositorio.SelecionarTodos();

        Assert.IsNull(grupoSelecionado);
        Assert.AreEqual(0, grupos.Count);
    }
}
