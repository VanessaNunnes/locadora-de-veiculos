using AutoMapper;
using LocadoraDeVeiculos.Aplicacao.Servicos;
using LocadoraDeVeiculos.Dominio.ModuloAutomoveis;
using LocadoraDeVeiculos.Dominio.ModuloUsuario;
using LocadoraDeVeiculos.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LocadoraDeVeiculos.WebApp.Controllers;
    public class AutomovelController : WebControllerBase
    {
        private readonly AutomovelService servico;
        private readonly GrupoAutomoveisService servicoGrupos;
        private readonly IMapper mapeador;

        public AutomovelController(
            AutomovelService servico,
            GrupoAutomoveisService servicoGrupos,
            IMapper mapeador
        )
        {
            this.servico = servico;
            this.servicoGrupos = servicoGrupos;
            this.mapeador = mapeador;
        }

        public IActionResult Listar()
        {
            var resultado = servico.SelecionarTodos(UsuarioId.GetValueOrDefault());

            if (resultado.IsFailed)
            {
                ApresentarMensagemFalha(resultado.ToResult());

                return RedirectToAction("Index", "Inicio");
            }

            var automoveis = resultado.Value;

            var listarAutomoveisVm = mapeador.Map<IEnumerable<ListarAutomovelViewModel>>(automoveis);

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

            var automovel = mapeador.Map<Automovel>(inserirVm);

            automovel.UsuarioId = UsuarioId.GetValueOrDefault();

		var resultado = servico.Inserir(automovel);


            if (resultado.IsFailed)
            {
                ApresentarMensagemFalha(resultado.ToResult());

                return RedirectToAction(nameof(Listar));
            }

            ApresentarMensagemSucesso($"O registro ID [{automovel.Id}] foi inserido com sucesso!");

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

            var automovel = resultado.Value;

            var editarAutomovelVm = mapeador.Map<EditarAutomovelViewModel>(automovel);

            return View(editarAutomovelVm);
        }

        [HttpPost]
        public IActionResult Editar(EditarAutomovelViewModel editarVm)
        {
            if (!ModelState.IsValid)
                return View(CarregarDadosFormulario(editarVm));

            var automovel = mapeador.Map<Automovel>(editarVm);

            var resultado = servico.Editar(automovel);

            if (resultado.IsFailed)
            {
                ApresentarMensagemFalha(resultado.ToResult());

                return RedirectToAction(nameof(Listar));
            }

            ApresentarMensagemSucesso($"O registro ID [{automovel.Id}] foi editado com sucesso!");

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

            var automovel = resultado.Value;

            var detalhesVm = mapeador.Map<DetalhesAutomovelViewModel>(automovel);

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

            var automovel = resultado.Value;

            var detalhesVm = mapeador.Map<DetalhesAutomovelViewModel>(automovel);

            return View(detalhesVm);
        }

        private FormularioAutomovelViewModel? CarregarDadosFormulario(
            FormularioAutomovelViewModel? dadosPrevios = null)
    {
            var resultadoGrupos = servicoGrupos.SelecionarTodos(UsuarioId.GetValueOrDefault());

            if (resultadoGrupos.IsFailed)
            {
                ApresentarMensagemFalha(resultadoGrupos.ToResult());

                return null;
            }

            var gruposDisponiveis = resultadoGrupos.Value;

            if (dadosPrevios is null)
            {
                var formularioVm = new FormularioAutomovelViewModel
                {
                    GrupoAutomoveis = gruposDisponiveis
                        .Select(g => new SelectListItem(g.Nome, g.Id.ToString()))
                };

                return formularioVm;
            }

            dadosPrevios.GrupoAutomoveis = gruposDisponiveis
                .Select(g => new SelectListItem(g.Nome, g.Id.ToString()));

            return dadosPrevios;
        }
    }
