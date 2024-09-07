using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.Unidade;
using FluentValidation;

namespace FarmPlannerAPI.Validators.Unidade
{
    public class ExcluirUnidadeValidator : AbstractValidator<UnidadeViewModel>
    {
        private readonly FarmPlannerContext _context;

        public ExcluirUnidadeValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c).Custom((Unidade, validateContext) =>
            {
                var fazendas = context.produtos.FirstOrDefault(c => c.idunidade == Unidade.id);
                if (fazendas != null)
                {
                    validateContext.AddFailure("Existem produtos pra essa unidade");
                }
            });
        }
    }
}