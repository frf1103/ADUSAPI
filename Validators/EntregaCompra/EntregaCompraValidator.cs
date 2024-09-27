using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.PedidoCompra;
using FluentValidation;

namespace FarmPlannerAPI.Validators.EntregaCompra
{
    public class EntregaCompraValidator
    {
        public class AddEntregaCompraValidator : AbstractValidator<EntregaCompraViewModel>
        {
            private readonly FarmPlannerContext _context;

            public AddEntregaCompraValidator(FarmPlannerContext context)
            {
                _context = context;

                /*            RuleFor(c => c.Descricao)
                    .NotEmpty().WithMessage("É necessário informar a Descricao.")
                    .MaximumLength(100).WithMessage("A Descriçao deve ter no máximo 100 caracteres");
                */
            }
        }

        public class ExcluirEntregaCompraValidator : AbstractValidator<EntregaCompraViewModel>
        {
            private readonly FarmPlannerContext _context;

            public ExcluirEntregaCompraValidator(FarmPlannerContext context)
            {
                _context = context;

                RuleFor(c => c).Custom((ped, validateContext) =>
                {
                });
            }
        }
    }
}