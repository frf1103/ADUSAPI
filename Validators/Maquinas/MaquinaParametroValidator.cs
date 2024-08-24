using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.Maquinas;
using FluentValidation;

namespace FarmPlannerAPI.Validators.Maquinas
{
    public class MaquinaParametroValidator : AbstractValidator<MaquinaParametroViewModel>
    {

        private readonly FarmPlannerContext _context;
        public MaquinaParametroValidator(FarmPlannerContext context)
        {
            _context = context;
            RuleFor(c => c).Custom((modelopar, validateContext) =>
            {
                var reg = context.maquinasparametro.Where(m => m.Id != modelopar.Id && m.IdMaquina == modelopar.IdMaquina && m.IdCultura == modelopar.IdCultura && m.IdOperacao == modelopar.IdOperacao)
                .FirstOrDefault();
                if (reg != null)
                {
                    validateContext.AddFailure("Já existe essa configuração");
                }

            });

        }

    }
}
