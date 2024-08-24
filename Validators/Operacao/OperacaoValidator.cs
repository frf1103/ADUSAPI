using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.Operacao;
using FluentValidation;

namespace FarmPlannerAPI.Validators.Operacao
{
    public class OperacaoValidator : AbstractValidator<OperacaoViewModel>
    {

        private readonly FarmPlannerContext _context;
        public OperacaoValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("É necessário informar a Descricao.")
                .MaximumLength(100).WithMessage("A Descricao deve ter no máximo 100 caracteres");

        }

    }

    public class ExcluirOperacaoValidator : AbstractValidator<OperacaoViewModel>
    {

        private readonly FarmPlannerContext _context;
        public ExcluirOperacaoValidator(FarmPlannerContext context)
        {
            RuleFor(c => c).Custom((tipooper, validateContext) =>
            {

                var princ = context.planejoperacoes.FirstOrDefault(c => c.IdOperacao == tipooper.Id);
                if (princ != null)
                {
                    validateContext.AddFailure("Existem planejamento para essa operação");
                }

            });
        }

    }
}
