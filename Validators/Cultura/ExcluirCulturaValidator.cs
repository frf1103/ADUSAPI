using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.Cultura;
using FluentValidation;

namespace FarmPlannerAPI.Validators.Cultura
{
    public class ExcluirCulturaValidator : AbstractValidator<CulturaViewModel>
    {
        private readonly FarmPlannerContext _context;
        public ExcluirCulturaValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c).Custom((conta, validateContext) =>
            {

                var variedade = context.variedades.FirstOrDefault(c => c.IdCultura == conta.Id);
                if (variedade != null)
                {
                    validateContext.AddFailure("Cultura com variedades definidas");
                }

            });


        }

    }

}
