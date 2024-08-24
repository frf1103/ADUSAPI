using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.Conta;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FarmPlannerAPI.Validators.Conta
{
    public class ExcluirContaValidator : AbstractValidator<EditarContaViewModel>
    {
        private readonly FarmPlannerContext _context;

        public ExcluirContaValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c).Custom((conta, validateContext) =>
            {
                var organiza = context.organizacoes.FirstOrDefault(c => c.idconta == conta.Id);
                if (organiza != null)
                {
                    validateContext.AddFailure("Conta tem organização vinculada");
                }

                var oper = context.operacoes.FirstOrDefault(c => c.idconta == conta.Id);
                if (organiza != null)
                {
                    validateContext.AddFailure("Conta tem operações vinculadas");
                }
            });
        }
    }
}