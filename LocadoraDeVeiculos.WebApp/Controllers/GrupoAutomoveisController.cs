using AutoMapper;
using LocadoraDeVeiculos.Aplicacao.Servicos;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomoveis;
using LocadoraDeVeiculos.WebApp.Extensions;
using LocadoraDeVeiculos.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocadoraDeVeiculos.WebApp.Controllers;

[Authorize(Roles = "Empresa,Funcionario")]
public class GrupoAutomoveisController : WebControllerBase
{
	private readonly GrupoAutomoveisService servico;
	private readonly IMapper mapeador;

	public GrupoAutomoveisController(
		AutenticacaoService servicoAuth,
		GrupoAutomoveisService servico,
		IMapper mapeador
	) : base(servicoAuth)
	{
		this.servico = servico;
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

		var grupos = resultado.Value;

		var listarGruposVm =
			mapeador.Map<IEnumerable<ListarGrupoAutomoveisViewModel>>(grupos);

		return View(listarGruposVm);
	}

	public IActionResult Inserir()
	{
		return View();
	}

	[HttpPost]
	public IActionResult Inserir(InserirGrupoAutomoveisViewModel inserirVm)
	{
		if (!ModelState.IsValid)
			return View(inserirVm);

		var grupo = mapeador.Map<GrupoAutomoveis>(inserirVm);

		var resultado = servico.Inserir(grupo);

		if (resultado.IsFailed)
		{
			ApresentarMensagemFalha(resultado.ToResult());

			return RedirectToAction(nameof(Listar));
		}

		ApresentarMensagemSucesso($"O registro ID [{grupo.Id}] foi inserido com sucesso!");

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

		var grupo = resultado.Value;

		var editarVm = mapeador.Map<EditarGrupoAutomoveisViewModel>(grupo);

		return View(editarVm);
	}

	[HttpPost]
	public IActionResult Editar(EditarGrupoAutomoveisViewModel editarVM)
	{
		if (!ModelState.IsValid)
			return View(editarVM);

		var grupo = mapeador.Map<GrupoAutomoveis>(editarVM);

		var resultado = servico.Editar(grupo);

		if (resultado.IsFailed)
		{
			ApresentarMensagemFalha(resultado.ToResult());

			return RedirectToAction(nameof(Listar));
		}

		ApresentarMensagemSucesso($"O registro ID [{grupo.Id}] foi editado com sucesso!");

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

		var grupo = resultado.Value;

		var detalhesVm = mapeador.Map<DetalhesGrupoAutomoveisViewModel>(grupo);

		return View(detalhesVm);
	}

	[HttpPost]
	public IActionResult Excluir(DetalhesGrupoAutomoveisViewModel detalhesVm)
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

		var grupo = resultado.Value;

		var detalhesVm = mapeador.Map<DetalhesGrupoAutomoveisViewModel>(grupo);

		return View(detalhesVm);
	}
}
