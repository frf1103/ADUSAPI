using ADUSAPI.Context;

using ADUSAPICore.Models.Moeda;
using FluentValidation;

namespace ADUSAPI.Validators.Moeda
{
    public class CotacaoMoedaValidator : AbstractValidator<CotacaoMoedaViewModel>
    {

        private readonly ADUSContext _context;
        public CotacaoMoedaValidator(ADUSContext context)
        {
            _context = context;


        }

    }

    public class ExcluirCotacaoMoedaValidator : AbstractValidator<CotacaoMoedaViewModel>
    {

        private readonly ADUSContext _context;
        public ExcluirCotacaoMoedaValidator(ADUSContext context)
        {
            RuleFor(c => c).Custom((tipooper, validateContext) =>
            {


            });
        }

    }
}
