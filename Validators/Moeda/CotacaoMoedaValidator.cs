using FarmPlannerAPI.Context;

using FarmPlannerAPICore.Models.Moeda;
using FluentValidation;

namespace FarmPlannerAPI.Validators.Moeda
{
    public class CotacaoMoedaValidator : AbstractValidator<CotacaoMoedaViewModel>
    {

        private readonly FarmPlannerContext _context;
        public CotacaoMoedaValidator(FarmPlannerContext context)
        {
            _context = context;


        }

    }

    public class ExcluirCotacaoMoedaValidator : AbstractValidator<CotacaoMoedaViewModel>
    {

        private readonly FarmPlannerContext _context;
        public ExcluirCotacaoMoedaValidator(FarmPlannerContext context)
        {
            RuleFor(c => c).Custom((tipooper, validateContext) =>
            {


            });
        }

    }
}
