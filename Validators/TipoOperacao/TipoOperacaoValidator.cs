using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.TipoOperacao;
using FluentValidation;

namespace FarmPlannerAPI.Validators.TipoOperacao
{
    public class TipoOperacaoValidator : AbstractValidator<TipoOperacaoViewModel>
    {
        private readonly FarmPlannerContext _context;

        public TipoOperacaoValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("E necessario informar a Descricao.")
                .MaximumLength(100).WithMessage("A Descricao deve ter no máximo 100 caracteres");
        }
    }

    public class ExcluirTipoOperacaoValidator : AbstractValidator<TipoOperacaoViewModel>
    {
        private readonly FarmPlannerContext _context;

        public ExcluirTipoOperacaoValidator(FarmPlannerContext context)
        {
            RuleFor(c => c).Custom((tipooper, validateContext) =>
            {
                var princ = context.operacoes.FirstOrDefault(c => c.IdTipoOperacao == tipooper.Id);
                if (princ != null)
                {
                    validateContext.AddFailure("Existem operaçoes vinculadas a esse tipo");
                }
            });
        }
    }
}