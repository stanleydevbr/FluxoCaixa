using FluentValidation;
using FluxoCaixa.Domain.Enums;
using System;

namespace FluxoCaixa.Domain.Entities
{
    public class Caixa : Entity<Caixa>
    {
        public DateTime Lancamento { get; set; } = DateTime.Now;
        public string Descricao { get; set; }
        public EnLancamento Tipo { get; set; }
        public decimal Valor { get; set; }
        public override bool EhValido()
        {
            RuleFor(x => x.Descricao)
                .NotEmpty()
                .WithMessage("A descrição precisa ser informada")
                .Length(5, 60)
                .WithMessage("A descrição precisa ter entre 5 a 60 caracteres");

            RuleFor(x => x.Valor)
                .GreaterThan(0)
                .WithMessage("O valor precisa ser maior que '0'");

            RuleFor(x => x.Lancamento)
                .NotEmpty()
                .WithMessage("A data de lançamento precisa ser informada")
                .LessThan(p => DateTime.Now.AddDays(1))
                .WithMessage("Não e possível fazer lançamento futuro");

            //TODO: Validar o enumerador valores permitidos (1-Credito, 2-Debito)
            //RuleFor(x => x.Tipo)
            //    .

            ValidationResult = Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
