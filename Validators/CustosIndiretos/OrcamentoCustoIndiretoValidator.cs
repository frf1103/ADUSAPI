using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.CustosIndiretos;
using FluentValidation;

namespace FarmPlannerAPI.Validators.CustosIndiretos
{
    public class OrcamentoCustoIndiretoValidator : AbstractValidator<OrcamentoCustoIndiretoViewModel>
    {
        private readonly FarmPlannerContext _context;

        public OrcamentoCustoIndiretoValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c.valor)
                .NotEmpty().NotEqual(0).WithMessage("É necessário informar o Valor.");
        }
    }

    public class ExcluirOrcamentoCustoIndiretoValidator : AbstractValidator<OrcamentoCustoIndiretoViewModel>
    {
        private readonly FarmPlannerContext _context;

        public ExcluirOrcamentoCustoIndiretoValidator(FarmPlannerContext context)
        {
            RuleFor(c => c).Custom((tipooper, validateContext) =>
            {
                /*   var princ = context.gruposcontas.FirstOrDefault(c => c.IdOrcamentoCustoIndireto == tipooper.Id);
                   if (princ != null)
                   {
                       validateContext.AddFailure("Existem grupos de conta vinculadas a essa classe");
                   } */
            });
        }
    }
}