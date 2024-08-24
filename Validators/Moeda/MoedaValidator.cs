using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.Moeda;
using FluentValidation;

namespace FarmPlannerAPI.Validators.Moeda
{
    public class MoedaValidator : AbstractValidator<MoedaViewModel>
    {

        private readonly FarmPlannerContext _context;
        public MoedaValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("É necessário informar a Descricao.")
                .MaximumLength(100).WithMessage("A Descricao deve ter no máximo 100 caracteres");

        }

    }

    public class ExcluirMoedaValidator : AbstractValidator<MoedaViewModel>
    {

        private readonly FarmPlannerContext _context;
        public ExcluirMoedaValidator(FarmPlannerContext context)
        {
            RuleFor(c => c).Custom((tipooper, validateContext) =>
            {

                var princ = context.cotacoesmoedas.FirstOrDefault(c => c.IdMoeda == tipooper.Id);
                if (princ != null)
                {
                    validateContext.AddFailure("Existem cotações dessa moeda");
                }
                var princ1 = context.comercializacoes.FirstOrDefault(c => c.IdMoeda == tipooper.Id);
                if (princ1 != null)
                {
                    validateContext.AddFailure("Existem comercializações nessa moeda");
                }

            });
        }

    }
}
