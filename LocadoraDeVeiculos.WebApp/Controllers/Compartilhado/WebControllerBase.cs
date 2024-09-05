using LocadoraDeVeiculos.WebApp.Extensions;
using LocadoraDeVeiculos.WebApp.Models;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using LocadoraDeVeiculos.Aplicacao.Servicos;

namespace LocadoraDeVeiculos.WebApp.Controllers;

    public class WebControllerBase : Controller
    {
	protected readonly AutenticacaoService servicoAuth;

	protected int? EmpresaId
	{
		get
		{
			var empresaId = servicoAuth.ObterIdEmpresaAsync(User).Result;

			return empresaId;
		}
	}

	protected WebControllerBase(AutenticacaoService servicoAuth)
	{
		this.servicoAuth = servicoAuth;
	}

	protected IActionResult MensagemRegistroNaoEncontrado(int idRegistro)
	{
		TempData.SerializarMensagemViewModel(new MensagemViewModel
		{
			Titulo = "Erro",
			Mensagem = $"Não foi possível encontrar o registro ID [{idRegistro}]!"
		});

		return RedirectToAction("Index", "Inicio");
	}

	protected void ApresentarMensagemFalha(Result resultado)
	{
		ViewBag.Mensagem = new MensagemViewModel
		{
			Titulo = "Falha",
			Mensagem = resultado.Errors[0].Message
		};
	}

	protected void ApresentarMensagemSucesso(string mensagem)
	{
		TempData.SerializarMensagemViewModel(new MensagemViewModel
		{
			Titulo = "Sucesso",
			Mensagem = mensagem
		});
	}
}


