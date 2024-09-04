using LocadoraDeVeiculos.Dominio.ModuloAutomoveis;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloCombustivel;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomoveis;
using LocadoraDeVeiculos.Dominio.ModuloLocacao;
using LocadoraDeVeiculos.Dominio.ModuloPlanoCobranca;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using LocadoraDeVeiculos.Dominio.ModuloUsuario;
using LocadoraDeVeiculos.Infra.Orm.ModuloAutomovel;
using LocadoraDeVeiculos.Infra.Orm.ModuloCliente;
using LocadoraDeVeiculos.Infra.Orm.ModuloCombustivel;
using LocadoraDeVeiculos.Infra.Orm.ModuloCondutor;
using LocadoraDeVeiculos.Infra.Orm.ModuloFuncionario;
using LocadoraDeVeiculos.Infra.Orm.ModuloGrupoAutomoveis;
using LocadoraDeVeiculos.Infra.Orm.ModuloLocacao;
using LocadoraDeVeiculos.Infra.Orm.ModuloPlanoCobranca;
using LocadoraDeVeiculos.Infra.Orm.ModuloTaxa;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LocadoraDeVeiculos.Infra.Orm.Compartilhado;

public class LocadoraDbContext : IdentityDbContext<Usuario, Perfil, int>
{
    public DbSet<GrupoAutomoveis> GrupoAutomoveis { get; set; }
    public DbSet<Automovel> Automoveis { get; set; }
    public DbSet<PlanoCobranca> PlanoCobrancas { get; set; }
    public DbSet<Taxa> Taxas { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Condutor> Condutores { get; set; }
    public DbSet<ConfiguracaoCombustivel> ConfiguracoesCombustiveis { get; set; }
    public DbSet<Locacao> Locacoes { get; set; }
    public DbSet<Funcionario> Funcionarios { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
		var config = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json")
			.Build();

		var connectionString = config.GetConnectionString("SqlServer");

		optionsBuilder.UseSqlServer(connectionString);

		base.OnConfiguring(optionsBuilder);
	}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
		var assembly = typeof(LocadoraDbContext).Assembly;

		modelBuilder.ApplyConfigurationsFromAssembly(assembly);

		base.OnModelCreating(modelBuilder);
	}
}