using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.Conta;
using FarmPlannerAPICore.Models.Fazenda;
using FluentValidation;

namespace FarmPlannerAPI.Validators.Fazenda
{
    public class ExcluirFazendaValidator : AbstractValidator<EditarFazendaViewModel>
    {
        private readonly FarmPlannerContext _context;
        public ExcluirFazendaValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c).Custom((fazenda, validateContext) =>
            {

                var talhoes = context.talhoes.FirstOrDefault(c => c.IdFazenda == fazenda.Id);
                if (talhoes != null)
                {
                    validateContext.AddFailure("Fazenda tem talhões definidos");
                }

            });
            RuleFor(c => c).Custom((fazenda, validateContext) =>
            {

                var talhoes = context.orcamentosproduto.FirstOrDefault(c => c.IdFazenda == fazenda.Id);
                if (talhoes != null)
                {
                    validateContext.AddFailure("Fazenda com orcamentos");
                }

            });

        }

    }

}
