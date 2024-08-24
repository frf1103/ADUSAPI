using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.Comercializacao;
using FluentValidation;

namespace FarmPlannerAPI.Validators.Comercializacao
{
    public class ComercializacaoValidator : AbstractValidator<ComercializacaoViewModel>
    {
        private readonly FarmPlannerContext _context;

        public ComercializacaoValidator(FarmPlannerContext context)
        {
            _context = context;
        }
    }

    public class ExcluirComercializacaoValidator : AbstractValidator<ComercializacaoViewModel>
    {
        private readonly FarmPlannerContext _context;

        public ExcluirComercializacaoValidator(FarmPlannerContext context)
        {
            RuleFor(c => c).Custom((com, validateContext) =>
            {
                var ent = _context.entregaContratos.Where(c => c.IdComercializacao == com.Id).FirstOrDefault();
                if (ent != null)
                {
                    validateContext.AddFailure("Contrato com entregas efetuadas");
                }
            });
        }
    }
}