using System;

namespace FluxoCaixa.Domain.ViewModels
{
    public class UsuarioLogadoViewModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime? ExpiraEm { get; set; }
    }
}
