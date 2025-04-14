using FluentValidation;

using ADUSAPICore.Models.Banco;

namespace ADUSAPI.Validators.Banco
{
    public class ContaCorrenteValidator : AbstractValidator<ContaCorrenteViewModel>
    {
        public ContaCorrenteValidator()
        {
            RuleFor(c => c.Id).NotEmpty().MaximumLength(256);
            RuleFor(c => c.Descricao).NotEmpty().MaximumLength(50);
            RuleFor(c => c.Agencia).NotEmpty().MaximumLength(4);
            RuleFor(c => c.ContaBanco).NotEmpty().MaximumLength(15);
            RuleFor(c => c.Titular).NotEmpty().MaximumLength(100);
            RuleFor(c => c.BancoId).GreaterThan(0);
        }
    }
}