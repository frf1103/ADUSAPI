using ADUSAPI.Context;
using ADUSAPICore.Models.Localidades;
using FluentValidation;

namespace ADUSAPI.Validators.Localidade
{
    public class RegiaoValidator : AbstractValidator<RegiaoViewModel>
    {
        private readonly ADUSContext _context;
        public RegiaoValidator(ADUSContext context)
        {
            _context = context;

            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("É necessário informar a Descricao.")
                .MaximumLength(100).WithMessage("A Descriçao deve ter no máximo 100 caracteres");

            RuleFor(c => c.Mascara)
                .NotEmpty().WithMessage("É necessário informar a máscara");



        }

    }

}
