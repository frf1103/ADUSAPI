using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.PedidoCompra;

using FluentValidation;

namespace FarmPlannerAPI.Validators.PedidoCompra
{
    public class PedidoCompraValidator : AbstractValidator<PedidoCompraViewModel>
    {
        private readonly FarmPlannerContext _context;

        public PedidoCompraValidator(FarmPlannerContext context)
        {
            _context = context;

            /*            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("É necessário informar a Descricao.")
                .MaximumLength(100).WithMessage("A Descriçao deve ter no máximo 100 caracteres");
            */
        }
    }

    public class ExcluirPedidoCompraValidator : AbstractValidator<PedidoCompraViewModel>
    {
        private readonly FarmPlannerContext _context;

        public ExcluirPedidoCompraValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c).Custom((ped, validateContext) =>
            {
                var conf = _context.produtoscompra.Where(c => c.idpedido == ped.id).FirstOrDefault();
                if (conf != null)
                {
                    validateContext.AddFailure("Existem produtos para esse pedido");
                }
            });
        }
    }
}