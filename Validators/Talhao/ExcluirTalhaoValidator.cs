using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.Talhao;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FarmPlannerAPI.Validators.Talhao
{
    
    public class ExcluirTalhaoValidator : AbstractValidator<EditarTalhaoViewModel>
    {
        private readonly FarmPlannerContext _context;
        public ExcluirTalhaoValidator(FarmPlannerContext context)
        {
            _context = context;


            RuleFor(c => c).Custom((talhao, validateContext) =>
            {
                var conf = _context.configareas.Where(c => c.IdTalhao == talhao.Id).FirstOrDefault();
                if (conf != null)
                {
                    validateContext.AddFailure("Existem configuraçoes para esse talhão");
                }
            });
        }
    }
}
