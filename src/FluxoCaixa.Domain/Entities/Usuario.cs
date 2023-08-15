using FluentValidation;

namespace FluxoCaixa.Domain.Entities
{
    public class Usuario: Entity<Usuario>
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ConfirmarSenha { get; set; }

        public override bool EhValido()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("O nome precisa ser informado.")
                .Length(6, 60)
                .WithMessage("O nome precisa ter entre 6 a 10 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("O e-mail precisa ser informado.")
                .EmailAddress()
                .WithMessage("E-mail inválido.");

            RuleFor(x => x.Senha)
                .NotEmpty()
                .WithMessage("A senha precisa ser informada");

            RuleFor(x => x.ConfirmarSenha)
                .Must(x => (x == Senha))
                .WithMessage("A confirmação da senha não confere, verifique. ");

            ValidationResult = Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
