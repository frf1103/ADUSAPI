using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.Conta;
using FarmPlannerAPICore.Models.Fazenda;
using FluentValidation;

namespace FarmPlannerAPI.Validators.Fazenda
{
    public class FazendaValidator : AbstractValidator<EditarFazendaViewModel>
    {
        private readonly FarmPlannerContext _context;
        public FazendaValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("É necessário informar a Descrição")
                .MaximumLength(100).WithMessage("O nome da Fazenda deve ter no máximo 100 caracteres");

        }

    }
}