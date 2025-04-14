using ADUSAPI.Context;
using ADUSAPICore.Models.Moeda;
using FluentValidation;

namespace ADUSAPI.Validators.Moeda
{
    public class MoedaValidator : AbstractValidator<MoedaViewModel>
    {
        private readonly ADUSContext _context;

        public MoedaValidator(ADUSContext context)
        {
            _context = context;

            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("É necessário informar a Descricao.")
                .MaximumLength(100).WithMessage("A Descricao deve ter no máximo 100 caracteres");
        }
    }

    public class ExcluirMoedaValidator : AbstractValidator<MoedaViewModel>
    {
        private readonly ADUSContext _context;

        public ExcluirMoedaValidator(ADUSContext context)
        {
            RuleFor(c => c).Custom((tipooper, validateContext) =>
            {
                var princ = context.cotacoesmoedas.FirstOrDefault(c => c.IdMoeda == tipooper.Id);
                if (princ != null)
                {
                    validateContext.AddFailure("Existem cotações dessa moeda");
                }
            });
        }
    }
}