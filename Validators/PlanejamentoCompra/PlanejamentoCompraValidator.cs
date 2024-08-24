using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.PlanejamentoCompra;
using FarmPlannerAPICore.Models.TipoOperacao;
using FluentValidation;

namespace FarmPlannerAPI.Validators.PlanejamentoCompra
{
    public class PlanejamentoCompraValidator: AbstractValidator<PlanejamentoCompraViewModel>
    {

        private readonly FarmPlannerContext _context;
        public PlanejamentoCompraValidator(FarmPlannerContext context)
        {
            _context = context;

/*            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("É necessário informar a Descricao.")
                .MaximumLength(100).WithMessage("A Descricao deve ter no máximo 100 caracteres");
*/
        }

    }

    public class ExcluirPlanejamentoCompraValidator : AbstractValidator<PlanejamentoCompraViewModel>
    {

        private readonly FarmPlannerContext _context;
        public ExcluirPlanejamentoCompraValidator(FarmPlannerContext context)
        {
            RuleFor(c => c).Custom((tipooper, validateContext) =>
            {

  
            });
        }

    }
}
