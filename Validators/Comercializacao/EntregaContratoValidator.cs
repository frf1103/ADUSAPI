using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.Comercializacao;
using FluentValidation;

namespace FarmPlannerAPI.Validators.Comercializacao
{
    public class EntregaContratoValidator : AbstractValidator<EntregaContratoViewModel>
    {
        private readonly FarmPlannerContext _context;

        public EntregaContratoValidator(FarmPlannerContext context)
        {
            _context = context;
            RuleFor(c => c.Documento)
             .NotEmpty().WithMessage("É necessário informar um documento.");
            RuleFor(c => c.Quantidade)
             .NotEmpty().GreaterThan(0).WithMessage("É necessário informar valor maior que zero.");
            RuleFor(c => c.DataEntrega)
             .NotEmpty().WithMessage("É necessário informar uma data de entrega.");
        }
    }

    public class ExcluirEntregaContratoValidator : AbstractValidator<EntregaContratoViewModel>
    {
        private readonly FarmPlannerContext _context;

        public ExcluirEntregaContratoValidator(FarmPlannerContext context)
        {
            RuleFor(c => c).Custom((com, validateContext) =>
            {
            });
        }
    }
}