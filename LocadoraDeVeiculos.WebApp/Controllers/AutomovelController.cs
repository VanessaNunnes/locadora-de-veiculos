using AutoMapper;
using FluentResults;
using LocadoraDeVeiculos.Aplicacao.Servicos;
using LocadoraDeVeiculos.Dominio.ModuloAutomoveis;
using LocadoraDeVeiculos.Dominio.ModuloUsuario;
using LocadoraDeVeiculos.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LocadoraDeVeiculos.WebApp.Controllers;

[Authorize(Roles = "Empresa,Funcionario")]
public class AutomovelController : WebControllerBase
    {
	private readonly AutomovelService servico;
	private readonly GrupoAutomoveisService servicoGrupos;
	private readonly IMapper mapeador;

	public AutomovelController(
		AutenticacaoService servicoAuth,
		AutomovelService servico,
		GrupoAutomoveisService servicoGrupos,
		IMapper mapeador
	) : base(servicoAuth)
	{
		this.servico = servico;
		this.servicoGrupos = servicoGrupos;
		this.mapeador = mapeador;
	}

	public IActionResult Listar()
	{
		var resultado = servico.SelecionarTodos(EmpresaId.GetValueOrDefault());

		if (resultado.IsFailed)
		{
			ApresentarMensagemFalha(resultado.ToResult());

			return RedirectToAction("Index", "Inicio");
		}

		var veiculos = resultado.Value;

		var listarAutomoveisVm = mapeador.Map<IEnumerable<ListarAutomovelViewModel>>(veiculos);

		return View(listarAutomoveisVm);
	}

	public IActionResult Inserir()
	{
		return View(CarregarDadosFormulario());
	}

	[HttpPost]
	public IActionResult Inserir(InserirAutomovelViewModel inserirVm)
	{
		if (!ModelState.IsValid)
			return View(CarregarDadosFormulario(inserirVm));

		var veiculo = mapeador.Map<Automovel>(inserirVm);

		var resultado = servico.Inserir(veiculo);

		if (resultado.IsFailed)
		{
			ApresentarMensagemFalha(resultado.ToResult());

			return RedirectToAction(nameof(Listar));
		}

		ApresentarMensagemSucesso($"O registro ID [{veiculo.Id}] foi inserido com sucesso!");

		return RedirectToAction(nameof(Listar));
	}

	public IActionResult Editar(int id)
	{
		var resultado = servico.SelecionarPorId(id);

		if (resultado.IsFailed)
		{
			ApresentarMensagemFalha(resultado.ToResult());

			return RedirectToAction(nameof(Listar));
		}

		var resultadoGrupos = servicoGrupos.SelecionarTodos(EmpresaId.GetValueOrDefault());

		if (resultadoGrupos.IsFailed)
		{
			ApresentarMensagemFalha(resultadoGrupos.ToResult());

			return null;
		}

		var veiculo = resultado.Value;

		var editarVm = mapeador.Map<EditarAutomovelViewModel>(veiculo);

		return View(editarVm);
	}

	[HttpPost]
	public IActionResult Editar(EditarAutomovelViewModel editarVm)
	{
		if (!ModelState.IsValid)
			return View(CarregarDadosFormulario(editarVm));

		var veiculo = mapeador.Map<Automovel>(editarVm);

		var resultado = servico.Editar(veiculo);

		if (resultado.IsFailed)
		{
			ApresentarMensagemFalha(resultado.ToResult());

			return RedirectToAction(nameof(Listar));
		}

		ApresentarMensagemSucesso($"O registro ID [{veiculo.Id}] foi editado com sucesso!");

		return RedirectToAction(nameof(Listar));
	}

	public IActionResult Excluir(int id)
	{
		var resultado = servico.SelecionarPorId(id);

		if (resultado.IsFailed)
		{
			ApresentarMensagemFalha(resultado.ToResult());

			return RedirectToAction(nameof(Listar));
		}

		var veiculo = resultado.Value;

		var detalhesVm = mapeador.Map<DetalhesAutomovelViewModel>(veiculo);

		return View(detalhesVm);
	}

	[HttpPost]
	public IActionResult Excluir(DetalhesAutomovelViewModel detalhesVm)
	{
		var resultado = servico.Excluir(detalhesVm.Id);

		if (resultado.IsFailed)
		{
			ApresentarMensagemFalha(resultado.ToResult());

			return RedirectToAction(nameof(Listar));
		}

		ApresentarMensagemSucesso($"O registro ID [{detalhesVm.Id}] foi excluído com sucesso!");

		return RedirectToAction(nameof(Listar));
	}

	public IActionResult Detalhes(int id)
	{
		var resultado = servico.SelecionarPorId(id);

		if (resultado.IsFailed)
		{
			ApresentarMensagemFalha(resultado.ToResult());

			return RedirectToAction(nameof(Listar));
		}

		var veiculo = resultado.Value;

		var detalhesVm = mapeador.Map<DetalhesAutomovelViewModel>(veiculo);

		return View(detalhesVm);
	}

	public IActionResult ObterFoto(int id)
	{
		var resultado = servico.SelecionarPorId(id);

		if (resultado.IsFailed)
		{
			ApresentarMensagemFalha(resultado.ToResult());

			return NotFound();
		}

		var veiculo = resultado.Value;

		return File(veiculo.Foto, "image/jpeg");
	}

	private FormularioAutomovelViewModel? CarregarDadosFormulario(
		FormularioAutomovelViewModel? dadosPrevios = null)
	{
		var resultadoGrupos = servicoGrupos.SelecionarTodos(EmpresaId.GetValueOrDefault());

		if (resultadoGrupos.IsFailed)
		{
			ApresentarMensagemFalha(resultadoGrupos.ToResult());

			return null;
		}

		var gruposDisponiveis = resultadoGrupos.Value;

		if (dadosPrevios is null)
			dadosPrevios = new FormularioAutomovelViewModel();

		dadosPrevios.GrupoAutomoveis = gruposDisponiveis
			.Select(g => new SelectListItem(g.Nome, g.Id.ToString()));

		return dadosPrevios;
	}
}
