using AutoMapper;
using LocadoraDeVeiculos.Aplicacao.Servicos;
using LocadoraDeVeiculos.Dominio.ModuloUsuario;
using LocadoraDeVeiculos.WebApp.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LocadoraDeVeiculos.WebApp.Controllers;

    public class InicioController : WebControllerBase
    {
    private readonly GrupoAutomoveisService servicoGrupoAutomoveis;
    private readonly IMapper mapeador;

    public InicioController(
        GrupoAutomoveisService servicoGrupoAutomoveis,
        IMapper mapeador
    )
    {
        this.servicoGrupoAutomoveis = servicoGrupoAutomoveis;
        this.mapeador = mapeador;
    }

    public ViewResult Index()
    {
        
        if (UsuarioId.HasValue)
        {
            ViewBag.QuantidadeGrupoAutomoveis = servicoGrupoAutomoveis.SelecionarTodos(UsuarioId.Value).Value.Count;
        }

        ViewBag.Mensagem = TempData.DesserializarMensagemViewModel();

        return View();
    }

}

