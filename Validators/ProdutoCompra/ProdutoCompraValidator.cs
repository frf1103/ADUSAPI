using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.PedidoCompra;

using FluentValidation;

namespace FarmPlannerAPI.Validators.ProdutoCompra
{
    public class ProdutoCompraValidator : AbstractValidator<ProdutoCompraViewModel>
    {
        private readonly FarmPlannerContext _context;

        public ProdutoCompraValidator(FarmPlannerContext context)
        {
            _context = context;

            /*            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("É necessário informar a Descricao.")
                .MaximumLength(100).WithMessage("A Descriçao deve ter no máximo 100 caracteres");
            */
        }
    }

    public class ExcluirProdutoCompraValidator : AbstractValidator<ProdutoCompraViewModel>
    {
        private readonly FarmPlannerContext _context;

        public ExcluirProdutoCompraValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c).Custom((ped, validateContext) =>
            {
            });
        }
    }
}