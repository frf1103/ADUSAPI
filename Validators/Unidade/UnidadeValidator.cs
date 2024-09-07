using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.Unidade;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FarmPlannerAPI.Validators.Unidade
{
    public class UnidadeValidator : AbstractValidator<UnidadeViewModel>
    {
        private readonly FarmPlannerContext _context;

        public UnidadeValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c.descricao)
                .NotEmpty().WithMessage("É necessário informar a Descricao.")
                .MaximumLength(100).WithMessage("A Descriçao deve ter no máximo 20 caracteres");

            RuleFor(c => c.multiplo)
                .NotEmpty().WithMessage("É necessário informar o multiplo");
        }
    }
}