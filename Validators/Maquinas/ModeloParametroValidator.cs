using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.Maquinas;
using FluentValidation;

namespace FarmPlannerAPI.Validators.Maquinas
{
    public class ModeloParametroValidator : AbstractValidator<ModeloParametroViewModel>
    {
        private readonly FarmPlannerContext _context;

        public ModeloParametroValidator(FarmPlannerContext context)
        {
            _context = context;
            RuleFor(c => c).Custom((modelopar, validateContext) =>
            {
                var reg = context.modelosparametros.Where(m => m.Id != modelopar.Id && m.IdModeloMaquina == modelopar.IdModeloMaquina && m.IdCultura == modelopar.IdCultura && m.IdOperacao == modelopar.IdOperacao && m.idconta == modelopar.idconta)
                .FirstOrDefault();
                if (reg != null)
                {
                    validateContext.AddFailure("Já existe essa configuração");
                }
            });
        }
    }
}