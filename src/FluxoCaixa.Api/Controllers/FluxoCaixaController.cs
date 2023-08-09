using FluxoCaixa.Api.Adapters;
using FluxoCaixa.Domain.Interfaces.Services;
using FluxoCaixa.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FluxoCaixa.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public IActionResult ObterTodos()
        {
            var result = _service.ObterTodosLancamentos();
            return CustomResponse(result);
        }

        [HttpGet("ObterPorId")]
        public IActionResult ObterPorId([FromQuery] long id)
        {
            var result = _service.ObterLancamentoPorId(id);
            return CustomResponse(result);
        }


        [HttpGet("Consolidado")]
        public IActionResult RelatorioConsolidadoDia([FromQuery] DateTime data)
        {
            var resultado = _service.ObterLancamentosPorPeriodo(data, data);
            var response = resultado.ToConsolidadoAdapter(data);
            return CustomResponse(response);
        }
    }
}
