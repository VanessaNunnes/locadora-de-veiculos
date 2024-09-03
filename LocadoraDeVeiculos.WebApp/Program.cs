using LocadoraDeVeiculos.Dominio.ModuloUsuario;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using LocadoraDeVeiculos.Aplicacao.ModuloTaxa;
using LocadoraDeVeiculos.Aplicacao.Servicos;
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
using LocadoraDeVeiculos.WebApp.Mapping.Resolvers;

namespace LocadoraDeVeiculos.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            
            #region Inje��o de Depend�ncia de Servi�os

            builder.Services.AddDbContext<LocadoraDbContext>();

            builder.Services.AddScoped<IRepositorioGrupoAutomoveis, RepositorioGrupoAutomoveisEmOrm>();
            builder.Services.AddScoped<GrupoAutomoveisService>();

            builder.Services.AddScoped<IRepositorioAutomovel, RepositorioAutomovelEmOrm>();
            builder.Services.AddScoped<AutomovelService>();

            builder.Services.AddScoped<IRepositorioPlanoCobranca, RepositorioPlanoCobrancaEmOrm>();
            builder.Services.AddScoped<PlanoCobrancaService>();

            builder.Services.AddScoped<IRepositorioTaxa, RepositorioTaxaEmOrm>();
            builder.Services.AddScoped<TaxaService>();

			builder.Services.AddScoped<IRepositorioCliente, RepositorioClienteEmOrm>();
			builder.Services.AddScoped<ClienteService>();

			builder.Services.AddScoped<IRepositorioCondutor, RepositorioCondutorEmOrm>();
			builder.Services.AddScoped<CondutorService>();

			builder.Services.AddScoped<IRepositorioConfiguracaoCombustivel, RepositorioConfiguracaoCombustivelEmOrm>();
            builder.Services.AddScoped<ConfiguracaoCombustivelService>();

            builder.Services.AddScoped<IRepositorioLocacao, RepositorioLocacaoEmOrm>();
            builder.Services.AddScoped<LocacaoService>();

			builder.Services.AddScoped<GrupoAutomoveisValueResolver>();

			builder.Services.AddScoped<TaxasSelecionadasValueResolver>();
			builder.Services.AddScoped<TaxasValueResolver>();
			builder.Services.AddScoped<CondutoresValueResolver>();
			builder.Services.AddScoped<AutomoveisValueResolver>();
			builder.Services.AddScoped<ValorParcialValueResolver>();
			builder.Services.AddScoped<ValorTotalValueResolver>();

			builder.Services.AddIdentity<Usuario, Perfil>()
                .AddEntityFrameworkStores<LocadoraDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 1;
            });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "AspNetCore.Cookies";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                    options.SlidingExpiration = true;
                });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Usuario/Login";
                options.AccessDeniedPath = "/Usuario/AcessoNegado";
            });

            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(Assembly.GetExecutingAssembly());
            });

            #endregion

            var app = builder.Build();

            app.UseStaticFiles();

            app.MapControllerRoute("default", "{controller=Inicio}/{action=Index}/{id:int?}");

            app.Run();
        }
    }
}
