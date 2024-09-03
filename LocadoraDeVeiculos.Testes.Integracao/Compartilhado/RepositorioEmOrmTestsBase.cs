using FizzWare.NBuilder;
using LocadoraDeVeiculos.Dominio.ModuloAutomoveis;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloCombustivel;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomoveis;
using LocadoraDeVeiculos.Dominio.ModuloLocacao;
using LocadoraDeVeiculos.Dominio.ModuloPlanoCobranca;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using LocadoraDeVeiculos.Infra.Orm.Compartilhado;
using LocadoraDeVeiculos.Infra.Orm.ModuloAutomovel;
using LocadoraDeVeiculos.Infra.Orm.ModuloCliente;
using LocadoraDeVeiculos.Infra.Orm.ModuloCombustivel;
using LocadoraDeVeiculos.Infra.Orm.ModuloCondutor;
using LocadoraDeVeiculos.Infra.Orm.ModuloGrupoAutomoveis;
using LocadoraDeVeiculos.Infra.Orm.ModuloLocacao;
using LocadoraDeVeiculos.Infra.Orm.ModuloPlanoCobranca;
using LocadoraDeVeiculos.Infra.Orm.ModuloTaxa;

namespace LocadoraDeVeiculos.Testes.Integracao.Compartilhado;

public abstract class RepositorioEmOrmTestsBase
{
    protected LocadoraDbContext _dbContext;

    protected RepositorioLocacaoEmOrm repositorioLocacao;
    protected RepositorioConfiguracaoCombustivelEmOrm repositorioCombustivel;
    protected RepositorioTaxaEmOrm repositorioTaxa;
    protected RepositorioClienteEmOrm repositorioCliente;
    protected RepositorioCondutorEmOrm repositorioCondutor;
    protected RepositorioAutomovelEmOrm repositorioVeiculo;
    protected RepositorioGrupoAutomoveisEmOrm repositorioGrupo;
    protected RepositorioPlanoCobrancaEmOrm repositorioPlano;

    [TestInitialize]
    public void Inicializar()
    {
        _dbContext = new LocadoraDbContext();

        _dbContext.Locacoes.RemoveRange(_dbContext.Locacoes);
        _dbContext.ConfiguracoesCombustiveis.RemoveRange(_dbContext.ConfiguracoesCombustiveis);
        _dbContext.Taxas.RemoveRange(_dbContext.Taxas);
        _dbContext.PlanoCobrancas.RemoveRange(_dbContext.PlanoCobrancas);
        _dbContext.Condutores.RemoveRange(_dbContext.Condutores);
        _dbContext.Clientes.RemoveRange(_dbContext.Clientes);
        _dbContext.Automoveis.RemoveRange(_dbContext.Automoveis);
        _dbContext.GrupoAutomoveis.RemoveRange(_dbContext.GrupoAutomoveis);

        _dbContext.SaveChanges();

        repositorioTaxa = new RepositorioTaxaEmOrm(_dbContext);
        repositorioPlano = new RepositorioPlanoCobrancaEmOrm(_dbContext);
        repositorioCliente = new RepositorioClienteEmOrm(_dbContext);
        repositorioCondutor = new RepositorioCondutorEmOrm(_dbContext);
        repositorioVeiculo = new RepositorioAutomovelEmOrm(_dbContext);
        repositorioGrupo = new RepositorioGrupoAutomoveisEmOrm(_dbContext);
        repositorioLocacao = new RepositorioLocacaoEmOrm(_dbContext);
        repositorioCombustivel = new RepositorioConfiguracaoCombustivelEmOrm(_dbContext);

        BuilderSetup.SetCreatePersistenceMethod<Locacao>(repositorioLocacao.Inserir);
        BuilderSetup.SetCreatePersistenceMethod<ConfiguracaoCombustivel>(repositorioCombustivel.GravarConfiguracao);
        BuilderSetup.SetCreatePersistenceMethod<Taxa>(repositorioTaxa.Inserir);
        BuilderSetup.SetCreatePersistenceMethod<PlanoCobranca>(repositorioPlano.Inserir);
        BuilderSetup.SetCreatePersistenceMethod<Condutor>(repositorioCondutor.Inserir);
        BuilderSetup.SetCreatePersistenceMethod<Cliente>(repositorioCliente.Inserir);
        BuilderSetup.SetCreatePersistenceMethod<Automovel>(repositorioVeiculo.Inserir);
        BuilderSetup.SetCreatePersistenceMethod<GrupoAutomoveis>(repositorioGrupo.Inserir);
    }
}
