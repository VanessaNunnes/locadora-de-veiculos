using AutoMapper;
using LocadoraDeVeiculos.Aplicacao.Servicos;
using LocadoraDeVeiculos.Dominio.ModuloLocacao;
using LocadoraDeVeiculos.WebApp.Models;

namespace LocadoraDeVeiculos.WebApp.Mapping.Resolvers;

public class ValorParcialValueResolver : IValueResolver<Locacao, ConfirmarAberturaLocacaoViewModel, decimal>
{
    private readonly AutomovelService servicoVeiculo;
    private readonly PlanoCobrancaService servicoPlano;

    public ValorParcialValueResolver(AutomovelService servicoVeiculo, PlanoCobrancaService servicoPlano)
    {
        this.servicoVeiculo = servicoVeiculo;
        this.servicoPlano = servicoPlano;
    }

    public decimal Resolve(
        Locacao source,
        ConfirmarAberturaLocacaoViewModel destination,
        decimal destMember,
        ResolutionContext context
    )
    {
        var veiculo = servicoVeiculo.SelecionarPorId(source.AutomovelId).Value;

        var planoSelecionado = servicoPlano.SelecionarPorIdGrupoAutomoveis(veiculo.GrupoAutomoveisId).Value;

        return source.CalcularValorParcial(planoSelecionado);
    }
}