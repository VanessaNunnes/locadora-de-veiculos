using AutoMapper;
using LocadoraDeVeiculos.Aplicacao.ModuloTaxa;
using LocadoraDeVeiculos.Aplicacao.Servicos;
using LocadoraDeVeiculos.Dominio.ModuloLocacao;
using LocadoraDeVeiculos.Dominio.ModuloPlanoCobranca;
using LocadoraDeVeiculos.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace LocadoraDeVeiculos.WebApp.Controllers;

public class LocacaoController : WebControllerBase
{
    private readonly LocacaoService servicoLocacao;
    private readonly AutomovelService servicoAutomovel;
    private readonly CondutorService servicoCondutor;
    private readonly TaxaService servicoTaxa;
    private readonly IMapper mapeador;

    public LocacaoController(
        LocacaoService servicoLocacao,
        AutomovelService servicoAutomovel,
        CondutorService servicoCondutor,
        TaxaService servicoTaxa,
        IMapper mapeador
    )
    {
        this.servicoLocacao = servicoLocacao;
        this.servicoAutomovel = servicoAutomovel;
        this.servicoCondutor = servicoCondutor;
        this.servicoTaxa = servicoTaxa;
        this.mapeador = mapeador;
    }

	public IActionResult Listar()
	{
		var resultado = servicoLocacao.SelecionarTodos();

		if (resultado.IsFailed)
		{
			ApresentarMensagemFalha(resultado.ToResult());

			return RedirectToAction("Index", "Inicio");
		}

		var locacoes = resultado.Value;

		var listarLocacoesVm = mapeador.Map<IEnumerable<ListarLocacaoViewModel>>(locacoes);

		return View(listarLocacoesVm);
	}

	public IActionResult Inserir()
	{
		return View(CarregarDadosFormulario());
	}

	[HttpPost]
	public IActionResult Inserir(InserirLocacaoViewModel inserirVm)
	{
		if (!ModelState.IsValid)
			return View(CarregarDadosFormulario(inserirVm));

		var locacao = mapeador.Map<Locacao>(inserirVm);

		var confirmarVm = mapeador.Map<ConfirmarAberturaLocacaoViewModel>(locacao);

		TempData["LocacaoParaInsercao"] = JsonSerializer.Serialize(confirmarVm);

		return RedirectToAction("ConfirmarAbertura");
	}

	public IActionResult ConfirmarAbertura()
	{
		if (TempData["LocacaoParaInsercao"] is null)
			return RedirectToAction(nameof(Inserir));

		var locacaoDataJson = TempData["LocacaoParaInsercao"]!.ToString();

		var confirmarVm = JsonSerializer.Deserialize<ConfirmarAberturaLocacaoViewModel>(locacaoDataJson);

		return View(confirmarVm);
	}

	[HttpPost]
	public IActionResult ConfirmarAbertura(ConfirmarAberturaLocacaoViewModel confirmarVm)
	{
		var locacao = mapeador.Map<Locacao>(confirmarVm);

		var resultado = servicoLocacao.Inserir(locacao);

		if (resultado.IsFailed)
		{
			ApresentarMensagemFalha(resultado.ToResult());

			return RedirectToAction(nameof(Listar));
		}

		ApresentarMensagemSucesso($"A locação ID [{locacao.Id}] foi aberta com sucesso!");

		return RedirectToAction(nameof(Listar));
	}

	public IActionResult RealizarDevolucao(int id)
	{
		var resultado = servicoLocacao.SelecionarPorId(id);

		if (resultado.IsFailed)
		{
			ApresentarMensagemFalha(resultado.ToResult());

			return RedirectToAction(nameof(Listar));
		}

		var locacao = resultado.Value;

		var devolucaoVm = mapeador.Map<RealizarDevolucaoViewModel>(locacao);

		return View(devolucaoVm);
	}

	[HttpPost]
	public IActionResult RealizarDevolucao(RealizarDevolucaoViewModel devolucaoVm)
	{
		var locacao = mapeador.Map<Locacao>(devolucaoVm);

		var confirmarVm = mapeador.Map<ConfirmarDevolucaoLocacaoViewModel>(locacao);

		TempData["LocacaoParaDevolucao"] = JsonSerializer.Serialize(confirmarVm);

		return RedirectToAction("ConfirmarDevolucao");

	}

	public IActionResult ConfirmarDevolucao()
	{
		if (TempData["LocacaoParaDevolucao"] is null)
			return RedirectToAction(nameof(Listar));

		var locacaoDataJson = TempData["LocacaoParaDevolucao"]!.ToString();

		var confirmarVm = JsonSerializer.Deserialize<ConfirmarDevolucaoLocacaoViewModel>(locacaoDataJson);

		return View(confirmarVm);
	}

	[HttpPost]
	public IActionResult ConfirmarDevolucao(ConfirmarDevolucaoLocacaoViewModel confirmarVm)
	{
		var locacaoOriginal = servicoLocacao.SelecionarPorId(confirmarVm.Id).Value;

		var locacaoAtualizada = mapeador.Map<ConfirmarDevolucaoLocacaoViewModel, Locacao>(confirmarVm, locacaoOriginal);

		var resultado = servicoLocacao.RealizarDevolucao(locacaoAtualizada);

		if (resultado.IsFailed)
		{
			ApresentarMensagemFalha(resultado.ToResult());

			return RedirectToAction(nameof(Listar));
		}

		ApresentarMensagemSucesso($"A locação ID [{locacaoAtualizada.Id}] foi concluída com sucesso!");

		return RedirectToAction(nameof(Listar));
	}


	private InserirLocacaoViewModel CarregarDadosFormulario(InserirLocacaoViewModel? formularioVm = null)
	{
		var condutores = servicoCondutor.SelecionarTodos().Value;
		var veiculos = servicoAutomovel.SelecionarTodos().Value;
		var taxas = servicoTaxa.SelecionarTodos().Value;

		if (formularioVm is null){}
			formularioVm = new InserirLocacaoViewModel();

		formularioVm.Condutores =
			condutores.Select(c => new SelectListItem(c.Nome, c.Id.ToString()));

		formularioVm.Automoveis =
			veiculos.Select(c => new SelectListItem(c.Modelo, c.Id.ToString()));

		formularioVm.Taxas =
			taxas.Select(c => new SelectListItem(c.ToString(), c.Id.ToString()));

		return formularioVm;
	}

}
