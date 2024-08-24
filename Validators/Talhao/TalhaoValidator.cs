using FarmPlannerAPI.Context;

using FarmPlannerAPICore.Models.Talhao;
using FluentValidation;

namespace FarmPlannerAPI.Validators.Talhao
{
    public class TalhaoValidator : AbstractValidator<EditarTalhaoViewModel>
    {
        private readonly FarmPlannerContext _context;
        public TalhaoValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("É necessário informar a Descrição")
                .MaximumLength(20).WithMessage("O nome do Talhão deve ter no máximo 20 caracteres");
            RuleFor(c => c.AreaProdutiva)
                .NotEmpty().WithMessage("É necessário informar a Area Produtiva")
                .GreaterThan(0).WithMessage("Informe uma area maior que zero");
                

        }

    }

}
