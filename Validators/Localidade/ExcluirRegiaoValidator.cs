using ADUSAPI.Context;

using ADUSAPICore.Models.Localidades;
using FluentValidation;

namespace ADUSAPI.Validators.Localidade
{
    public class ExcluirRegiaoValidator : AbstractValidator<RegiaoViewModel>
    {
        private readonly ADUSContext _context;

        public ExcluirRegiaoValidator(ADUSContext context)
        {
            _context = context;

            RuleFor(c => c).Custom((regiao, validateContext) =>
            {
            });
        }
    }
}