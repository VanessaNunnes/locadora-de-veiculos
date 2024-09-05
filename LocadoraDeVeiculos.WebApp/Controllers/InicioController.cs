using AutoMapper;
using LocadoraDeVeiculos.Aplicacao.Servicos;
using LocadoraDeVeiculos.Dominio.ModuloUsuario;
using LocadoraDeVeiculos.WebApp.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LocadoraDeVeiculos.WebApp.Controllers;

    public class InicioController : Controller
    {
	public IActionResult Index()
	{
		return View();
	}

}

