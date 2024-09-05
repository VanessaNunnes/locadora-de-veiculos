using AutoMapper;
using LocadoraDeVeiculos.Aplicacao.Servicos;
using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using LocadoraDeVeiculos.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocadoraDeVeiculos.WebApp.Controllers;

[Authorize(Roles = "Empresa")]
public class FuncionarioController : WebControllerBase
{
    private readonly FuncionarioService servicoFuncionario;
    private readonly IMapper mapeador;

    public FuncionarioController(
        FuncionarioService servicoFuncionario,
        AutenticacaoService servicoAutenticacao,
        IMapper mapeador
    ) : base(servicoAutenticacao)
    {
        this.servicoFuncionario = servicoFuncionario;
        this.mapeador = mapeador;
    }

    public async Task<IActionResult> Listar()
    {
        var resultado = servicoFuncionario
            .SelecionarFuncionariosDaEmpresa(EmpresaId.GetValueOrDefault());

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction("Index", "Inicio");
        }

        var funcionarios = resultado.Value;

        var listarFuncionariosVm = mapeador.Map<IEnumerable<ListarFuncionarioViewModel>>(funcionarios);

        return View(listarFuncionariosVm);
    }

    public IActionResult Inserir()
    {
        return View(new InserirFuncionarioViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Inserir(InserirFuncionarioViewModel inserirVm)
    {
        if (!ModelState.IsValid)
            return View(inserirVm);

        var funcionario = mapeador.Map<Funcionario>(inserirVm);

        var resultadoFuncionario = await servicoFuncionario.Inserir(
            funcionario,
            inserirVm.NomeUsuario,
            inserirVm.Senha
        );

        if (resultadoFuncionario.IsFailed)
        {
            ApresentarMensagemFalha(resultadoFuncionario.ToResult());

            return RedirectToAction(nameof(Listar));
        }

        ApresentarMensagemSucesso($"O funcionário ID [{funcionario.Id}] foi inserido com sucesso!");

        return RedirectToAction(nameof(Listar));
    }
}
