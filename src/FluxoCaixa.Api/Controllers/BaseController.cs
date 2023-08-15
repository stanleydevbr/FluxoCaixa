using FluxoCaixa.Domain.Notifications;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FluxoCaixa.Api.Controllers
{
    public abstract class BaseController : Controller
    {
        protected ICollection<string> Erros = new List<string>();
        protected NotificationContext _notifications;

        protected BaseController(NotificationContext notifications)
        {
            _notifications = notifications;
        }

        protected ActionResult CustomResponse(object result = null)
        {

            if (OperacaoValida())
            {
                return Ok(result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Mensagens", Erros.ToArray() }
            }));
        }

        protected bool OperacaoValida()
        {
            return !_notifications.HasNotifications;
        }
    }
}
