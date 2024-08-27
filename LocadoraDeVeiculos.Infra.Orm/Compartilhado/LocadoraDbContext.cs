using LocadoraDeVeiculos.Dominio.ModuloAutomoveis;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomoveis;
using LocadoraDeVeiculos.Dominio.ModuloPlanoCobranca;
using LocadoraDeVeiculos.Dominio.ModuloUsuario;
using LocadoraDeVeiculos.Infra.Orm.ModuloAutomovel;
using LocadoraDeVeiculos.Infra.Orm.ModuloGrupoAutomoveis;
using LocadoraDeVeiculos.Infra.Orm.ModuloPlanoCobranca;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LocadoraDeVeiculos.Infra.Orm.Compartilhado;

public class LocadoraDbContext : IdentityDbContext<Usuario, Perfil, int>
{
    public DbSet<GrupoAutomoveis> GrupoAutomoveis { get; set; }
    public DbSet<Automovel> Automoveis { get; set; }
    public DbSet<PlanoCobranca> PlanoCobrancas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = config
            .GetConnectionString("SqlServer");

        optionsBuilder.UseSqlServer(connectionString);

        optionsBuilder.LogTo(Console.WriteLine).EnableSensitiveDataLogging();

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MapeadorGrupoAutomoveisEmOrm());
        modelBuilder.ApplyConfiguration(new MapeadorAutomovelEmOrm());
        modelBuilder.ApplyConfiguration(new MapeadorPlanoCobrancaEmOrm());

        base.OnModelCreating(modelBuilder);
    }
}