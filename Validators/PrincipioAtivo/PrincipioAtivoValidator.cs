using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.Conta;
using FarmPlannerAPICore.Models.PrincipioAtivo;
using FluentValidation;

namespace FarmPlannerAPI.Validators.PrincipioAtivo
{
    public class PrincipioAtivoValidator : AbstractValidator<PrincipioAtivoViewModel>
    {
        private readonly FarmPlannerContext _context;

        public PrincipioAtivoValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("E necessario informar a Descricao.")
                .MaximumLength(100).WithMessage("A Descricao deve ter no máximo 100 caracteres");
        }
    }

    public class ExcluirPrincipioAtivoValidator : AbstractValidator<PrincipioAtivoViewModel>
    {
        private readonly FarmPlannerContext _context;

        public ExcluirPrincipioAtivoValidator(FarmPlannerContext context)
        {
            RuleFor(c => c).Custom((principio, validateContext) =>
            {
                var princ = context.produtosprincipio.FirstOrDefault(c => c.idprincipio == principio.Id);
                if (princ != null)
                {
                    validateContext.AddFailure("Principio ativo com produtos");
                }
                var princ1 = context.produtosorcamento.FirstOrDefault(c => c.IdPrincipioAtivo == principio.Id);
                if (princ1 != null)
                {
                    validateContext.AddFailure("Principio ativo orcamento");
                }
                var princ2 = context.produtoplanejados.FirstOrDefault(c => c.IdPrincipioAtivo == principio.Id);
                if (princ2 != null)
                {
                    validateContext.AddFailure("Principio ativo planejamento");
                }
            });
        }
    }
}