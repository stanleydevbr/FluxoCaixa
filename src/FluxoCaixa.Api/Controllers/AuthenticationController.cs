using FluxoCaixa.Domain.Interfaces.Services;
using FluxoCaixa.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FluxoCaixa.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioService _service;

        public AuthenticationController(IConfiguration configuration, IUsuarioService service)
        {
            _configuration = configuration;
            _service = service;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginViewModel login)
        {
            if (login == null)
                return BadRequest("Request inválido.");

            var usuarioLogado = _service.Login(login);
            return Ok(usuarioLogado);
        }

    }
}
