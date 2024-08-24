using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.PlanejamentoOperacao;

using FluentValidation;

namespace FarmPlannerAPI.Validators.PlanejamentoOperacao
{
    public class PlanejamentoOperacaoValidator : AbstractValidator<PlanejamentoOperacaoViewModel>
    {
        private readonly FarmPlannerContext _context;
        public PlanejamentoOperacaoValidator(FarmPlannerContext context)
        {
            _context = context;

            /*            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("É necessário informar a Descricao.")
                .MaximumLength(100).WithMessage("A Descriçao deve ter no máximo 100 caracteres");
            */
        }
    }

    public class ExcluirPlanejamentoOperacaoValidator : AbstractValidator<PlanejamentoOperacaoViewModel>
    {
        private readonly FarmPlannerContext _context;
        public ExcluirPlanejamentoOperacaoValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c).Custom((orc, validateContext) =>
            {
/*                var conf = _context.produtosorcamento.Where(c => c.IdOrcamento == orc.Id).FirstOrDefault();
                if (conf != null)
                {
                    validateContext.AddFailure("Existem produtos para esse orçamento");
                }

                var conf1 = _context.planejoperacoes.Where(c => c.IdOrcamento == orc.Id).FirstOrDefault();
                if (conf1 != null)
                {
                    validateContext.AddFailure("Orçamento com planejamento");
                }
*/
            });

        }


    }

}
