using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.Cultura;
using FluentValidation;

namespace FarmPlannerAPI.Validators.Cultura
{
    public class CulturaValidator : AbstractValidator<CulturaViewModel>
    {
        private readonly FarmPlannerContext _context;

        public CulturaValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("Necessario informar a Descricao.")
                .MaximumLength(100).WithMessage("A Descriçao deve ter no máximo 100 caracteres");

            RuleFor(c => c.UnidadeProdutiva)
                .NotEmpty().WithMessage("Necessario informar a unidade produtiva");
            RuleFor(c => c.NomeProduto)
                .NotEmpty().WithMessage("Necessario informar o nome do produto");
            RuleFor(c => c.DiasEstimadosEmergencia)
                .GreaterThan(0).WithMessage("Dias maior ou igual a 1")
                .NotEmpty().WithMessage("Necessario informar os Dias de emergencia");
        }
    }
}