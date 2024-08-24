using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.GrupoProduto;
using FluentValidation;

namespace FarmPlannerAPI.Validators.GrupoProduto
{
    public class GrupoProdutoValidator : AbstractValidator<GrupoProdutoViewModel>
    {
        private readonly FarmPlannerContext _context;

        public GrupoProdutoValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("E necessario informar a Descricao.")
                .MaximumLength(100).WithMessage("A Descricao deve ter no máximo 100 caracteres");
        }
    }

    public class ExcluirGrupoProdutoValidator : AbstractValidator<GrupoProdutoViewModel>
    {
        private readonly FarmPlannerContext _context;

        public ExcluirGrupoProdutoValidator(FarmPlannerContext context)
        {
            RuleFor(c => c).Custom((grupo, validateContext) =>
            {
                var princ = context.produtos.FirstOrDefault(c => c.IdGrupoProduto == grupo.Id);
                if (princ != null)
                {
                    validateContext.AddFailure("Grupo com produtos");
                }
            });
        }
    }
}