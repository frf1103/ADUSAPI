using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.Localidades;
using FluentValidation;

namespace FarmPlannerAPI.Validators.Localidade
{
    public class RegiaoValidator : AbstractValidator<RegiaoViewModel>
    {
        private readonly FarmPlannerContext _context;
        public RegiaoValidator(FarmPlannerContext context)
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
