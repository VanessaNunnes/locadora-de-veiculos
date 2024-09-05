using AutoMapper;
using LocadoraDeVeiculos.Aplicacao.Servicos;
using LocadoraDeVeiculos.Dominio.ModuloCombustivel;
using LocadoraDeVeiculos.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocadoraDeVeiculos.WebApp.Controllers;

[Authorize(Roles = "Empresa,Funcionario")]
public class CombustivelController : WebControllerBase
{
	private readonly ConfiguracaoCombustivelService servicoCombustivel;
	private readonly IMapper mapeador;

	public CombustivelController(
		AutenticacaoService servicoAuth,
		ConfiguracaoCombustivelService servicoCombustivel,
		IMapper mapeador
	) : base(servicoAuth)
	{
		this.servicoCombustivel = servicoCombustivel;
		this.mapeador = mapeador;
	}

	public IActionResult Configurar()
	{
		var resultado = servicoCombustivel
			.ObterConfiguracao(EmpresaId.GetValueOrDefault());

		if (resultado.IsFailed)
			return RedirectToAction("Index", "Inicio");

		var configuracaoCombustivel = resultado.Value;

		var formularioVm = mapeador.Map<FormularioConfiguracaoCombustivelViewModel>(configuracaoCombustivel);

		return View(formularioVm);
	}

	[HttpPost]
	public IActionResult Configurar(FormularioConfiguracaoCombustivelViewModel formularioVm)
	{
		var config = mapeador.Map<ConfiguracaoCombustivel>(formularioVm);

		var resultado = servicoCombustivel.SalvarConfiguracao(config);

		if (resultado.IsFailed)
			return RedirectToAction("Index", "Inicio");

		ApresentarMensagemSucesso("A configuração foi salva com sucesso!");

		return RedirectToAction("Index", "Inicio");
	}
}
