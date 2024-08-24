using FarmPlannerAPI.Context;

using FarmPlannerAPICore.Models.Localidades;
using FluentValidation;

namespace FarmPlannerAPI.Validators.Localidade
{
    public class ExcluirRegiaoValidator : AbstractValidator<RegiaoViewModel>
    {
        private readonly FarmPlannerContext _context;
        public ExcluirRegiaoValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c).Custom((regiao, validateContext) =>
            {

                var fazendas = context.fazendas.FirstOrDefault(c => c.IdRegiao == regiao.Id);
                if (fazendas != null)
                {
                    validateContext.AddFailure("Existem fazendas pra essa região");
                }

            });

        }

    }

}
