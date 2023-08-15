using FluxoCaixa.Api.Adapters;
using FluxoCaixa.Domain.Interfaces.Services;
using FluxoCaixa.Domain.Notifications;
using FluxoCaixa.Domain.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace FluxoCaixa.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FluxoCaixaController : BaseController
    {
        private readonly ICaixaService _service;
        protected NotificationContext _notifications;
        public FluxoCaixaController(ICaixaService service, NotificationContext notifications) : base(notifications)
        {
            _service = service;
            _notifications = notifications;
        }

        #region GET
        [HttpGet]
        public ActionResult<List<CaixaViewModel>> ObterTodos()
        {
            var result = _service.ObterTodosLancamentos();
            return Ok(result);
        }

        [HttpGet("ObterPorId")]
        public ActionResult<CaixaViewModel> ObterPorId([FromQuery] long id)
        {
            var result = _service.ObterLancamentoPorId(id);
            return Ok(result);
        }

        [HttpGet("Consolidado")]
        public ActionResult<ConsolidadoViewModel> RelatorioConsolidadoDia([FromQuery] DateTime data)
        {
            var resultado = _service.ObterLancamentosPorPeriodo(data, data);
            var response = resultado?.ToConsolidadoAdapter(data);
            return Ok(response);
        }
        #endregion

        #region POST
        [HttpPost]
        public IActionResult Adicionar([FromBody] CaixaViewModel caixa)
        {
            _service.Gravar(caixa);
            return Ok("Lançamento adicionando com sucesso!");
        }
        #endregion

        #region PUT
        [HttpPut]
        public IActionResult Atualizar([FromBody] CaixaViewModel caixa)
        {
            _service.Atualizar(caixa);
            return Ok("Lançamento atualizado com sucesso!");
        }
        #endregion

        #region DELETE
        [HttpDelete]
        public IActionResult Excluir([FromQuery] long id)
        {
            _service.Remover(id);
            return Ok("Exclusão realizada com sucesso!");
        }
        #endregion

    }
}
