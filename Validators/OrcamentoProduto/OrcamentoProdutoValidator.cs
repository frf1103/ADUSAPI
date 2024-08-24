using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.OrcamentoProduto;

using FluentValidation;

namespace FarmPlannerAPI.Validators.OrcamentoProduto
{
    public class OrcamentoProdutoValidator : AbstractValidator<OrcamentoProdutoViewModel>
    {
        private readonly FarmPlannerContext _context;

        public OrcamentoProdutoValidator(FarmPlannerContext context)
        {
            _context = context;
            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("É necessário informar a Descricao.")
                .MaximumLength(100).WithMessage("A Descriçao deve ter no máximo 100 caracteres");
        }
    }

    public class ExcluirOrcamentoProdutoValidator : AbstractValidator<OrcamentoProdutoViewModel>
    {
        private readonly FarmPlannerContext _context;

        public ExcluirOrcamentoProdutoValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c).Custom((orc, validateContext) =>
            {
                var conf = _context.produtosorcamento.Where(c => c.IdOrcamento == orc.Id).FirstOrDefault();
                if (conf != null)
                {
                    validateContext.AddFailure("Existem produtos para esse orçamento");
                }
            });
        }
    }
}