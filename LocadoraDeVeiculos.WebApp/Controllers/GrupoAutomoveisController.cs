using AutoMapper;
using LocadoraDeVeiculos.Aplicacao.Servicos;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomoveis;
using LocadoraDeVeiculos.WebApp.Extensions;
using LocadoraDeVeiculos.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace LocadoraDeVeiculos.WebApp.Controllers;
    public class GrupoAutomoveisController : WebControllerBase
    {
        private readonly GrupoAutomoveisService servicoGrupoAutomoveis;
        private readonly IMapper mapeador;

        public GrupoAutomoveisController(
            GrupoAutomoveisService servicoGrupoAutomoveis,
            IMapper mapeador
        )
        {
            this.servicoGrupoAutomoveis = servicoGrupoAutomoveis;
            this.mapeador = mapeador;
        }

        public IActionResult Listar()
        {
            var resultado = servicoGrupoAutomoveis.SelecionarTodos();

            if (resultado.IsFailed)
            {
                ApresentarMensagemFalha(resultado.ToResult());

                return RedirectToAction("Index", "Inicio");
            }

            var grupoAutomoveis = resultado.Value;

            var listarGrupoAutomoveissVm = mapeador.Map<IEnumerable<ListarGrupoAutomoveisViewModel>>(grupoAutomoveis);

            ViewBag.Mensagem = TempData.DesserializarMensagemViewModel();

            return View(listarGrupoAutomoveissVm);
        }

        public IActionResult Inserir()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Inserir(InserirGrupoAutomoveisViewModel inserirGrupoAutomoveisVm)
        {
            if (!ModelState.IsValid)
                return View(inserirGrupoAutomoveisVm);

            var grupoAutomoveis = mapeador.Map<GrupoAutomoveis>(inserirGrupoAutomoveisVm);

            var resultado = servicoGrupoAutomoveis.Inserir(grupoAutomoveis);

            if (resultado.IsFailed)
            {
                ApresentarMensagemFalha(resultado.ToResult());

                return RedirectToAction(nameof(Listar));
            }

            ApresentarMensagemSucesso($"O registro ID [{grupoAutomoveis.Id}] foi inserido com sucesso!");

            return RedirectToAction(nameof(Listar));
        }

        public IActionResult Editar(int id)
        {
            var resultado = servicoGrupoAutomoveis.SelecionarPorId(id);

            if (resultado.IsFailed)
            {
                ApresentarMensagemFalha(resultado.ToResult());

                return RedirectToAction(nameof(Listar));
            }

            var grupoAutomoveis = resultado.Value;

            var editarGrupoAutomoveisVm = mapeador.Map<EditarGrupoAutomoveisViewModel>(grupoAutomoveis);

            return View(editarGrupoAutomoveisVm);
        }

        [HttpPost]
        public IActionResult Editar(EditarGrupoAutomoveisViewModel editarGrupoAutomoveisVm)
        {
            if (!ModelState.IsValid)
                return View(editarGrupoAutomoveisVm);

            var grupoAutomoveis = mapeador.Map<GrupoAutomoveis>(editarGrupoAutomoveisVm);

            var resultado = servicoGrupoAutomoveis.Editar(grupoAutomoveis);

            if (resultado.IsFailed)
            {
                ApresentarMensagemFalha(resultado.ToResult());

                return RedirectToAction(nameof(Listar));
            }

            ApresentarMensagemSucesso($"O registro ID [{editarGrupoAutomoveisVm.Id}] foi editado com sucesso!");

            return RedirectToAction(nameof(Listar));
        }

        public IActionResult Excluir(int id)
        {
            var resultado = servicoGrupoAutomoveis.SelecionarPorId(id);

            if (resultado.IsFailed)
            {
                ApresentarMensagemFalha(resultado.ToResult());

                return RedirectToAction(nameof(Listar));
            }

            var grupoAutomoveis = resultado.Value;

            var detalhesGrupoAutomoveisViewModel = mapeador.Map<DetalhesGrupoAutomoveisViewModel>(grupoAutomoveis);

            return View(detalhesGrupoAutomoveisViewModel);
        }

        [HttpPost]
        public IActionResult Excluir(DetalhesGrupoAutomoveisViewModel detalhesGrupoAutomoveisViewModel)
        {
            var resultado = servicoGrupoAutomoveis.Excluir(detalhesGrupoAutomoveisViewModel.Id);

            if (resultado.IsFailed)
            {
                ApresentarMensagemFalha(resultado);

                return RedirectToAction(nameof(Listar));
            }

            ApresentarMensagemSucesso($"O registro ID [{detalhesGrupoAutomoveisViewModel.Id}] foi excluído com sucesso!");

            return RedirectToAction(nameof(Listar));
        }

        public IActionResult Detalhes(int id)
        {
            var resultado = servicoGrupoAutomoveis.SelecionarPorId(id);

            if (resultado.IsFailed)
            {
                ApresentarMensagemFalha(resultado.ToResult());

                return RedirectToAction(nameof(Listar));
            }

            var grupoAutomoveis = resultado.Value;

            var detalhesGrupoAutomoveisViewModel = mapeador.Map<DetalhesGrupoAutomoveisViewModel>(grupoAutomoveis);

            return View(detalhesGrupoAutomoveisViewModel);
        }
    }
