using FluxoCaixa.Api.Adapters;
using FluxoCaixa.Domain.Interfaces.Services;
using FluxoCaixa.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace FluxoCaixa.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class FluxoCaixaController : BaseController
    {
        private readonly ICaixaService _service;

        public FluxoCaixaController(ICaixaService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Adicionar([FromBody] CaixaViewModel caixa)
        {
            _service.Gravar(caixa);
            return CustomResponse("Operação realizada com sucesso!");
        }

        [HttpGet]
        public ActionResult<List<CaixaViewModel>> ObterTodos()
        {
            var result = _service.ObterTodosLancamentos();
            return CustomResponse(result);
        }

        [HttpGet("ObterPorId")]
        public ActionResult<CaixaViewModel> ObterPorId([FromQuery] long id)
        {
            var result = _service.ObterLancamentoPorId(id);
            return CustomResponse(result);
        }


        [HttpGet("Consolidado")]
        public ActionResult<ConsolidadoViewModel> RelatorioConsolidadoDia([FromQuery] DateTime data)
        {
            var resultado = _service.ObterLancamentosPorPeriodo(data, data);
            var response = resultado.ToConsolidadoAdapter(data);
            return CustomResponse(response);
        }
    }
}
