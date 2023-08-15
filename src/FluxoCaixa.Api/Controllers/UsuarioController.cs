using FluxoCaixa.Domain.Interfaces.Services;
using FluxoCaixa.Domain.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FluxoCaixa.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;
        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult ObterPorEmail([FromQuery] string email)
        {
            return Ok(_service.ObterPorEmail(email));
        }

        [HttpPost]
        public IActionResult Cadastro([FromBody] UsuarioViewModel model)
        {
            _service.Adicionar(model);
            return Ok("Usuário registrado com sucesso.");
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Atualizar([FromBody] UsuarioViewModel model)
        {
            _service.Atualizar(model);
            return Ok("Usuário atualizado com sucesso.");
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Remover([FromQuery] long id)
        {
            _service.Remover(id);
            return Ok("Usuário removido com sucesso.");
        }
    }
}
