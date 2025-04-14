using FluentValidation;
using ADUSAPICore.Models.Banco;
using ADUSAPI.Context;

namespace ADUSAPI.Validators.Banco
{
    public class BancoValidator : AbstractValidator<BancoViewModel>
    {
        public BancoValidator()
        {
            RuleFor(b => b.Descricao)
                .NotEmpty().WithMessage("A descrição é obrigatória.")
                .MaximumLength(50).WithMessage("A descrição deve ter no máximo 50 caracteres.");

            RuleFor(b => b.Codigo)
                .NotEmpty().WithMessage("O código é obrigatório.")
                .MaximumLength(4).WithMessage("O código deve ter no máximo 4 caracteres.");
        }
    }

    public class ExcluirBancoValidator : AbstractValidator<BancoViewModel>
    {
        private readonly ADUSContext _context;

        public ExcluirBancoValidator(ADUSContext context)
        {
            _context = context;

            RuleFor(b => b.Id)
                .GreaterThan(0).WithMessage("ID inválido para exclusão.")
                .Must(NaoPossuiContasCorrentes).WithMessage("Não é possível excluir um banco com contas correntes vinculadas.");
        }

        private bool NaoPossuiContasCorrentes(int bancoId)
        {
            return !_context.contascorrentes.Any(c => c.BancoId == bancoId);
        }
    }
}