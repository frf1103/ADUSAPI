using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.Produto;
using FarmPlannerAPICore.Models.ProdutoPrincipio;
using FluentValidation;

namespace FarmPlannerAPI.Validators.Produto
{
    public class ProdutoPrincipioValidator : AbstractValidator<ProdutoPrincipioViewModel>
    {
        private readonly FarmPlannerContext _context;

        public ProdutoPrincipioValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c.quantidade)
                .GreaterThan(0).WithMessage("Quantidade precisa ser maior que zero.");

            RuleFor(c => c).Custom((pp, validateContext) =>
            {
                var reg = context.produtosprincipio.FirstOrDefault(x => x.idproduto == pp.idproduto && x.idprincipio == pp.idprincipio);
                if (reg != null)
                {
                    validateContext.AddFailure("Principio ativo já configurado para esse produto");
                }
            });
        }
    }
}