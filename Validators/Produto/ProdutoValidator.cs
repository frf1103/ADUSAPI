using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.Produto;
using FluentValidation;

namespace FarmPlannerAPI.Validators.Produto
{
    public class ProdutoValidator : AbstractValidator<ProdutoViewModel>
    {
        private readonly FarmPlannerContext _context;
        public ProdutoValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("É necessário informar a Descricao.")
                .MaximumLength(100).WithMessage("A Descricao deve ter no máximo 100 caracteres");

        }

    }

    public class ExcluirProdutoValidator : AbstractValidator<ProdutoViewModel>
    {
        private readonly FarmPlannerContext _context;
        public ExcluirProdutoValidator(FarmPlannerContext context)
        {
            
            RuleFor(c => c).Custom((grupo, validateContext) =>
            {

                var reg = context.produtoplanejados.FirstOrDefault(c => c.IdProduto == grupo.Id);
                if (reg != null)
                {
                    validateContext.AddFailure("Produto com planejamento");
                }
                var reg1 = context.produtosorcamento.FirstOrDefault(c => c.IdProduto == grupo.Id);
                if (reg1 != null)
                {
                    validateContext.AddFailure("Produto com orçamento");
                }
                var reg2 = context.produtosorcamento.FirstOrDefault(c => c.IdProduto == grupo.Id);
                if (reg2 != null)
                {
                    validateContext.AddFailure("Produto com orçamento");
                }
                var reg3 = context.planejamentoCompras.FirstOrDefault(c => c.IdProduto == grupo.Id);
                if (reg3 != null)
                {
                    validateContext.AddFailure("Produto com planejamento de compra");
                }

            }); 

        }

    }
}
