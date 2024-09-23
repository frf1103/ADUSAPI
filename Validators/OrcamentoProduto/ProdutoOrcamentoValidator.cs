using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.OrcamentoProduto;
using FluentValidation;

namespace FarmPlannerAPI.Validators.OrcamentoProduto
{
    public class ProdutoOrcamentoValidator : AbstractValidator<ProdutoOrcamentoViewModel>
    {
        private readonly FarmPlannerContext _context;

        public ProdutoOrcamentoValidator(FarmPlannerContext context)
        {
            _context = context;
            /*
            RuleFor(c => c.IdProduto)
                .NotEmpty().WithMessage("É necessário informar o produto.");
            RuleFor(c => c.IdPrincipioAtivo)
                .NotEmpty().WithMessage("É necessário informar o principio ativo.");
            */
            RuleFor(c => c).Custom((orc, validateContext) =>
            {
                var conf = _context.produtos.Where(c => c.Id == orc.IdProduto).FirstOrDefault();
                /*                if (conf != null && conf.IdPrincipioAtivo != orc.IdPrincipioAtivo)
                                {
                                    validateContext.AddFailure("Produto e principio ativo incompatíveis");
                                } */
            });
        }
    }

    public class ExcluirProdutoOrcamentoValidator : AbstractValidator<ProdutoOrcamentoViewModel>
    {
        private readonly FarmPlannerContext _context;

        public ExcluirProdutoOrcamentoValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c).Custom((orc, validateContext) =>
            {
            });
        }
    }
}