using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.Organizacao;
using FluentValidation;
using Microsoft.EntityFrameworkCore;


namespace FarmPlannerAPI.Validators.Organizacao
{
    public class ExcluirOrganizacaoValidator:AbstractValidator<EditarOrganizacaoViewModel>
    {
        private readonly FarmPlannerContext _context;
        public ExcluirOrganizacaoValidator(FarmPlannerContext context)
        {
            _context = context;
            RuleFor(c => c).Custom((organizacao, validateContext) =>
            {

                var faz = context.fazendas.Where(f => f.IdOrganizacao == organizacao.Id).FirstOrDefault();
                if (faz != null)
                {
                    validateContext.AddFailure("Organização tem fazenda vinculada");
                }
                var reg = context.gruposcontas.Where(f => f.IdOrganizacao == organizacao.Id).FirstOrDefault();
                if (reg != null)
                {
                    validateContext.AddFailure("Organização tem Contas vinculadas");
                }

            });
                
        }
        
    }
}
