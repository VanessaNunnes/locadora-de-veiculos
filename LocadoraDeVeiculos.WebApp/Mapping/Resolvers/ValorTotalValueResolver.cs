using AutoMapper;
using LocadoraDeVeiculos.Aplicacao.Servicos;
using LocadoraDeVeiculos.Dominio.ModuloLocacao;
using LocadoraDeVeiculos.WebApp.Models;

namespace LocadoraDeVeiculos.WebApp.Mapping.Resolvers;

public class ValorTotalValueResolver : IValueResolver<Locacao, ConfirmarDevolucaoLocacaoViewModel, decimal>
{
    private readonly AutomovelService servicoVeiculo;
    private readonly PlanoCobrancaService servicoPlano;

    public ValorTotalValueResolver(AutomovelService servicoVeiculo, PlanoCobrancaService servicoPlano)
    {
        this.servicoVeiculo = servicoVeiculo;
        this.servicoPlano = servicoPlano;
    }

    public decimal Resolve(
        Locacao source,
        ConfirmarDevolucaoLocacaoViewModel destination,
        decimal destMember,
        ResolutionContext context
    )
    {
        var veiculo = servicoVeiculo.SelecionarPorId(source.AutomovelId).Value;

        var planoSelecionado = servicoPlano.SelecionarPorIdGrupoAutomoveis(veiculo.GrupoAutomoveisId).Value;

        return source.CalcularValorTotal(planoSelecionado);
    }
}