using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.AnoAgricola;
using FarmPlannerAPICore.Models.PrincipioAtivo;
using FluentValidation;

namespace FarmPlannerAPI.Validators.AnoAgricola
{
    public class ExcluirAnoAgricolaValidator : AbstractValidator<AdicionarAnoAgricolaViewModel>
    {

        private readonly FarmPlannerContext _context;
        public ExcluirAnoAgricolaValidator(FarmPlannerContext context)
        {
            RuleFor(c => c).Custom((ano, validateContext) =>
            {

                var tl = context.talhoes.FirstOrDefault(c => c.IdAnoAgricola == ano.Id);
                if (tl != null)
                {
                    validateContext.AddFailure("Existem talhões para esse ano agricola");
                }
                var sf = context.safras.FirstOrDefault(c => c.IdAnoAgricola == ano.Id);
                if (tl != null)
                {
                    validateContext.AddFailure("Existem safras para esse ano agricola");
                }


            });
        }

    }
}
